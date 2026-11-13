using System.Data;
using Model;

namespace BusinessLogic;

/// <summary>Adds and voids services given to an admitted patient, and totals them.</summary>
public class ChargeService : ServiceBase
{
    public const int MaxQuantity = 999;

    public ChargeService(string connectionString) : base(connectionString) { }

    /// <summary>Adds a service to an admission. Returns the new ChargeID.</summary>
    public int Add(int admissionId, int serviceId, int quantity)
    {
        if (admissionId <= 0)
            throw new ValidationException("Select an admission first.", "Admission");
        if (serviceId <= 0)
            throw new ValidationException("Select a service.", "Service");
        if (quantity < 1 || quantity > MaxQuantity)
            throw new ValidationException($"Quantity must be a whole number from 1 to {MaxQuantity}.", "Quantity");

        Permissions.RequireLogin();

        return Run("ChargeService.Add", () =>
        {
            var admission = LoadAdmission(admissionId);
            if (admission.Status != AdmissionStatus.Admitted)
                throw new ValidationException(
                    $"This admission is {admission.Status.ToLowerInvariant()}. Charges can only be added while the patient is admitted.",
                    "Admission");

            var id = Db.ExecuteScalarSP("usp_tblAdmissionCharges_Add",
                P("@AdmissionID", admissionId),
                P("@ServiceID", serviceId),
                P("@Quantity", quantity),
                P("@ChargedBy", CurrentUser.EmailOrSystem));
            return id == null ? 0 : Convert.ToInt32(id);
        });
    }

    /// <summary>Voids a charge made by mistake. Admin only; a reason is required.</summary>
    public void Void(int chargeId, string? reason)
    {
        if (chargeId <= 0)
            throw new ValidationException("Select a charge to void.", "Charge");
        var why = RequireText(reason, "Reason", 300);
        if (why.Length < 3)
            throw new ValidationException("Reason must be at least 3 characters.", "Reason");

        Permissions.Require(AppAction.VoidCharge);

        RunVoid("ChargeService.Void", () =>
            Db.ExecuteSP("usp_tblAdmissionCharges_Void",
                P("@ChargeID", chargeId),
                P("@Reason", why),
                P("@VoidedBy", CurrentUser.EmailOrSystem)));
    }

    public DataTable GetByAdmission(int admissionId, bool includeVoided = false) =>
        Run("ChargeService.GetByAdmission", () =>
            Db.QuerySP("usp_tblAdmissionCharges_GetByAdmission",
                P("@AdmissionID", admissionId), P("@IncludeVoided", includeVoided)));

    /// <summary>Sum of all charge lines that are not voided.</summary>
    public decimal GetSubtotal(int admissionId)
    {
        var table = GetByAdmission(admissionId, includeVoided: false);
        decimal total = 0m;
        foreach (DataRow row in table.Rows)
        {
            if (RowReader.Bool(row, "IsVoided")) continue;
            total += RowReader.Dec(row, "LineTotal");
        }
        return total;
    }

    private Admission LoadAdmission(int admissionId)
    {
        var dt = Db.QuerySP("usp_tblAdmissions_GetById", P("@AdmissionID", admissionId));
        if (dt.Rows.Count == 0)
            throw new ValidationException("Admission not found.", "Admission");
        return Mappers.ToAdmission(dt.Rows[0]);
    }
}
