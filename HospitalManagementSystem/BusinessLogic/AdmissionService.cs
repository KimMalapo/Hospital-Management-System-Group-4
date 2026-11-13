using System.Data;
using Model;

namespace BusinessLogic;

/// <summary>
/// Admit, search, cancel and discharge hospital admissions.
/// Every method validates input first, then calls the usp_tblAdmissions_* stored procedures.
/// </summary>
public class AdmissionService : ServiceBase
{
    private readonly LookupService _lookups;

    public AdmissionService(string connectionString) : base(connectionString)
    {
        _lookups = new LookupService(connectionString);
    }

    // ---------- Admit ----------

    /// <summary>Admits a patient into a room. Returns the saved admission (with its AdmissionNo).</summary>
    public Admission Admit(int patientId, string? roomNo, string? departmentCode, string? diagnosisCode,
                           string? attendingPhysician, string? reason, DateTime? admitDate = null)
    {
        if (patientId <= 0)
            throw new ValidationException("Select a patient to admit.", "Patient");
        var room = RequireText(roomNo, "Room", 20);
        var department = RequireText(departmentCode, "Department", 50);
        var diagnosis = RequireText(diagnosisCode, "Diagnosis", 50);
        var physician = RequireText(attendingPhysician, "Attending physician", 200);
        var why = OptionalText(reason, "Reason for admission", 500);
        if (admitDate.HasValue && admitDate.Value > DateTime.Now.AddMinutes(5))
            throw new ValidationException("The admit date cannot be in the future.", "Admit date");

        Permissions.RequireLogin();

        return Run("AdmissionService.Admit", () =>
        {
            RequireActiveLookup("DepartmentTypes", department, "Department");
            RequireActiveLookup("DiagnosisCodes", diagnosis, "Diagnosis");

            var dt = Db.QuerySP("usp_tblAdmissions_Admit",
                P("@PatientID", patientId),
                P("@RoomNo", room),
                P("@DepartmentCode", department),
                P("@DiagnosisCode", diagnosis),
                P("@AttendingPhysician", physician),
                P("@Reason", why),
                P("@AdmitDate", admitDate),
                P("@CreatedBy", CurrentUser.EmailOrSystem));

            if (dt.Rows.Count == 0)
                throw new ValidationException("The admission could not be saved.");

            var id = RowReader.Int(dt.Rows[0], "AdmissionID");
            return GetById(id) ?? throw new ValidationException("The admission was saved but could not be loaded.");
        });
    }

    private void RequireActiveLookup(string key, string code, string label)
    {
        var item = _lookups.GetAll(key).Find(i =>
            i.Code != null && i.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

        if (item == null || item.IsDeleted ||
            !string.Equals(item.Status, "Active", StringComparison.OrdinalIgnoreCase))
            throw new ValidationException($"{label} '{code}' does not exist or is not active.", label);
    }

    // ---------- Read ----------

    public DataTable GetActive() =>
        Run("AdmissionService.GetActive", () => Db.QuerySP("usp_tblAdmissions_GetActive"));

    public DataTable Search(string? term) =>
        Run("AdmissionService.Search", () =>
            Db.QuerySP("usp_tblAdmissions_Search", P("@SearchTerm", term?.Trim() ?? "")));

    public Admission? GetById(int admissionId)
    {
        if (admissionId <= 0) return null;
        return Run("AdmissionService.GetById", () =>
        {
            var dt = Db.QuerySP("usp_tblAdmissions_GetById", P("@AdmissionID", admissionId));
            return dt.Rows.Count == 0 ? null : Mappers.ToAdmission(dt.Rows[0]);
        });
    }

    public DataTable GetByPatient(int patientId) =>
        Run("AdmissionService.GetByPatient", () =>
            Db.QuerySP("usp_tblAdmissions_GetByPatient", P("@PatientID", patientId)));

    // ---------- Cancel ----------

    /// <summary>Cancels an admission that has no charges yet and frees the room.</summary>
    public void Cancel(int admissionId)
    {
        if (admissionId <= 0)
            throw new ValidationException("Select an admission to cancel.", "Admission");
        Permissions.RequireLogin();

        RunVoid("AdmissionService.Cancel", () =>
            Db.ExecuteSP("usp_tblAdmissions_Cancel",
                P("@AdmissionID", admissionId),
                P("@CancelledBy", CurrentUser.EmailOrSystem)));
    }

    // ---------- Discharge ----------

    /// <summary>Explains whether the patient can be discharged now (used to enable the Discharge button).</summary>
    public DischargeCheck CheckDischarge(int admissionId)
    {
        if (admissionId <= 0)
            return new DischargeCheck(false, "Select an admission first.");

        return Run("AdmissionService.CheckDischarge", () =>
        {
            var admission = GetById(admissionId);
            if (admission == null)
                return new DischargeCheck(false, "Admission not found.");
            if (admission.Status != AdmissionStatus.Admitted)
                return new DischargeCheck(false, $"This admission is already {admission.Status.ToLowerInvariant()}.");

            var billTable = Db.QuerySP("usp_tblBills_GetByAdmission", P("@AdmissionID", admissionId));
            if (billTable.Rows.Count == 0)
                return new DischargeCheck(false, "No bill has been generated yet. Generate the bill first.");

            var bill = Mappers.ToBill(billTable.Rows[0]);
            if (bill.Status == BillStatus.Voided)
                return new DischargeCheck(false, "The bill was voided. Generate a new bill first.");
            if (bill.Status != BillStatus.Paid || bill.Balance > 0)
                return new DischargeCheck(false, $"A balance of {bill.Balance:N2} is still unpaid.");

            return new DischargeCheck(true, "Ready for discharge.");
        });
    }

    /// <summary>Discharges a fully paid admission. Throws ValidationException (with the reason) when not allowed.</summary>
    public void Discharge(int admissionId, string? summary)
    {
        var note = OptionalText(summary, "Discharge summary", 1000);
        Permissions.RequireLogin();

        var check = CheckDischarge(admissionId);
        if (!check.CanDischarge)
            throw new ValidationException(check.Message, "Discharge");

        RunVoid("AdmissionService.Discharge", () =>
            Db.ExecuteSP("usp_tblAdmissions_Discharge",
                P("@AdmissionID", admissionId),
                P("@DischargeSummary", note),
                P("@DischargedBy", CurrentUser.EmailOrSystem)));
    }

    /// <summary>The final patient record: admission, bill, charges and payments.</summary>
    public DischargeRecord GetDischargeRecord(int admissionId)
    {
        var admission = GetById(admissionId)
            ?? throw new ValidationException("Admission not found.", "Admission");
        if (admission.Status != AdmissionStatus.Discharged)
            throw new ValidationException("The final record is available after the patient is discharged.", "Admission");

        return Run("AdmissionService.GetDischargeRecord", () =>
        {
            var record = new DischargeRecord { Admission = admission };

            var billTable = Db.QuerySP("usp_tblBills_GetByAdmission", P("@AdmissionID", admissionId));
            if (billTable.Rows.Count > 0)
            {
                record.Bill = Mappers.ToBill(billTable.Rows[0]);
                record.Payments = Db.QuerySP("usp_tblPayments_GetByBill", P("@BillID", record.Bill.BillID));
            }

            record.Charges = Db.QuerySP("usp_tblAdmissionCharges_GetByAdmission",
                P("@AdmissionID", admissionId), P("@IncludeVoided", false));
            return record;
        });
    }
}
