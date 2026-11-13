using System.Data;
using Model;

namespace BusinessLogic;

/// <summary>Generates and reads bills. Totals are calculated by the database from the room stay and the charges.</summary>
public class BillingService : ServiceBase
{
    public BillingService(string connectionString) : base(connectionString) { }

    /// <summary>Creates the bill for an admission (or replaces it while nothing has been paid).</summary>
    public Bill Generate(int admissionId)
    {
        if (admissionId <= 0)
            throw new ValidationException("Select an admission first.", "Admission");
        Permissions.RequireLogin();

        return Run("BillingService.Generate", () =>
        {
            var admission = Db.QuerySP("usp_tblAdmissions_GetById", P("@AdmissionID", admissionId));
            if (admission.Rows.Count == 0)
                throw new ValidationException("Admission not found.", "Admission");
            if (RowReader.Str(admission.Rows[0], "Status") == AdmissionStatus.Cancelled)
                throw new ValidationException("A cancelled admission cannot be billed.", "Admission");

            var existing = GetByAdmission(admissionId);
            if (existing != null && existing.AmountPaid > 0)
                throw new ValidationException(
                    "This bill already has payments, so it cannot be regenerated. Void the payments first.", "Bill");

            var id = Db.ExecuteScalarSP("usp_tblBills_Generate",
                P("@AdmissionID", admissionId),
                P("@GeneratedBy", CurrentUser.EmailOrSystem));

            var billId = id == null ? 0 : Convert.ToInt32(id);
            return GetById(billId) ?? throw new ValidationException("The bill was created but could not be loaded.");
        });
    }

    /// <summary>The current (non-voided) bill of an admission, or null when none exists.</summary>
    public Bill? GetByAdmission(int admissionId)
    {
        if (admissionId <= 0) return null;
        return Run("BillingService.GetByAdmission", () =>
        {
            var dt = Db.QuerySP("usp_tblBills_GetByAdmission", P("@AdmissionID", admissionId));
            return dt.Rows.Count == 0 ? null : Mappers.ToBill(dt.Rows[0]);
        });
    }

    public Bill? GetById(int billId)
    {
        if (billId <= 0) return null;
        return Run("BillingService.GetById", () =>
        {
            var dt = Db.QuerySP("usp_tblBills_GetById", P("@BillID", billId));
            return dt.Rows.Count == 0 ? null : Mappers.ToBill(dt.Rows[0]);
        });
    }

    public DataTable GetItems(int billId) =>
        Run("BillingService.GetItems", () => Db.QuerySP("usp_tblBills_GetItems", P("@BillID", billId)));

    /// <summary>Sets the discount of a bill (replaces any earlier discount). A reason is required.</summary>
    public void ApplyDiscount(int billId, decimal amount, string? reason)
    {
        if (billId <= 0)
            throw new ValidationException("Select a bill first.", "Bill");
        RequireMoney(amount, "Discount");
        var why = RequireText(reason, "Discount reason", 300);
        Permissions.RequireLogin();

        RunVoid("BillingService.ApplyDiscount", () =>
        {
            var bill = GetById(billId) ?? throw new ValidationException("Bill not found.", "Bill");
            if (bill.Status == BillStatus.Paid || bill.Status == BillStatus.Voided)
                throw new ValidationException($"A {bill.Status.ToLowerInvariant()} bill cannot be changed.", "Bill");
            if (amount > bill.Subtotal)
                throw new ValidationException(
                    $"The discount cannot be more than the bill subtotal of {bill.Subtotal:N2}.", "Discount");
            if (amount > bill.Subtotal - bill.AmountPaid)
                throw new ValidationException(
                    $"The discount would drop the total below what was already paid ({bill.AmountPaid:N2}).", "Discount");

            Db.ExecuteSP("usp_tblBills_ApplyDiscount",
                P("@BillID", billId),
                P("@Amount", amount),
                P("@Reason", why),
                P("@AppliedBy", CurrentUser.EmailOrSystem));
        });
    }

    /// <summary>Voids a bill. Admin only; only possible while nothing is paid.</summary>
    public void Void(int billId, string? reason)
    {
        if (billId <= 0)
            throw new ValidationException("Select a bill first.", "Bill");
        var why = RequireText(reason, "Reason", 300);
        Permissions.Require(AppAction.VoidBill);

        RunVoid("BillingService.Void", () =>
        {
            var bill = GetById(billId) ?? throw new ValidationException("Bill not found.", "Bill");
            if (bill.AmountPaid > 0)
                throw new ValidationException("Void the payments of this bill first.", "Bill");

            Db.ExecuteSP("usp_tblBills_Void",
                P("@BillID", billId), P("@Reason", why), P("@VoidedBy", CurrentUser.EmailOrSystem));
        });
    }
}
