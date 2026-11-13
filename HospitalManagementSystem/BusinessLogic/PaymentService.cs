using System.Data;
using Model;

namespace BusinessLogic;

/// <summary>Records payments against a bill, voids them, and loads payment history and receipts.</summary>
public class PaymentService : ServiceBase
{
    private readonly BillingService _billing;

    public PaymentService(string connectionString) : base(connectionString)
    {
        _billing = new BillingService(connectionString);
    }

    /// <summary>Posts a payment. Returns the saved payment, including its ReceiptNo.</summary>
    public Payment Post(int billId, decimal amount, string? method, string? referenceNo)
    {
        if (billId <= 0)
            throw new ValidationException("Select a bill first.", "Bill");
        RequireMoney(amount, "Amount");

        var chosen = PaymentMethod.All.FirstOrDefault(m =>
            m.Equals((method ?? "").Trim(), StringComparison.OrdinalIgnoreCase));
        if (chosen == null)
            throw new ValidationException(
                "Choose a payment method: " + string.Join(", ", PaymentMethod.All) + ".", "Method");

        var reference = OptionalText(referenceNo, "Reference number", 100);
        if (reference == null && PaymentMethod.RequiresReference(chosen))
            throw new ValidationException($"A reference number is required for {chosen} payments.", "Reference number");

        Permissions.RequireLogin();

        return Run("PaymentService.Post", () =>
        {
            var bill = _billing.GetById(billId) ?? throw new ValidationException("Bill not found.", "Bill");
            if (bill.Status == BillStatus.Voided)
                throw new ValidationException("This bill was voided. Payments cannot be recorded.", "Bill");
            if (bill.Status == BillStatus.Paid || bill.Balance <= 0)
                throw new ValidationException("This bill is already fully paid.", "Bill");
            if (amount > bill.Balance)
                throw new ValidationException(
                    $"The payment of {amount:N2} is more than the balance of {bill.Balance:N2}.", "Amount");

            var dt = Db.QuerySP("usp_tblPayments_Post",
                P("@BillID", billId),
                P("@Amount", amount),
                P("@Method", chosen),
                P("@ReferenceNo", reference),
                P("@ReceivedBy", CurrentUser.EmailOrSystem));

            if (dt.Rows.Count == 0)
                throw new ValidationException("The payment could not be saved.");
            return Mappers.ToPayment(dt.Rows[0]);
        });
    }

    /// <summary>Voids a payment and restores the bill balance. Admin only; a reason is required.</summary>
    public void Void(int paymentId, string? reason)
    {
        if (paymentId <= 0)
            throw new ValidationException("Select a payment to void.", "Payment");
        var why = RequireText(reason, "Reason", 300);
        Permissions.Require(AppAction.VoidPayment);

        RunVoid("PaymentService.Void", () =>
            Db.ExecuteSP("usp_tblPayments_Void",
                P("@PaymentID", paymentId), P("@Reason", why), P("@VoidedBy", CurrentUser.EmailOrSystem)));
    }

    public DataTable GetByBill(int billId) =>
        Run("PaymentService.GetByBill", () => Db.QuerySP("usp_tblPayments_GetByBill", P("@BillID", billId)));

    public DataTable GetHistory(DateTime from, DateTime to, int? patientId = null)
    {
        if (from.Date > to.Date)
            throw new ValidationException("The 'From' date cannot be later than the 'To' date.", "From");

        return Run("PaymentService.GetHistory", () =>
            Db.QuerySP("usp_tblPayments_GetHistory",
                P("@From", from.Date), P("@To", to.Date), P("@PatientID", patientId)));
    }

    /// <summary>Data for the receipt print layout, loaded from the database.</summary>
    public ReceiptData? GetReceipt(int paymentId)
    {
        if (paymentId <= 0) return null;
        return Run("PaymentService.GetReceipt", () =>
        {
            var dt = Db.QuerySP("usp_Receipt_GetByPayment", P("@PaymentID", paymentId));
            return dt.Rows.Count == 0 ? null : Mappers.ToReceipt(dt.Rows[0]);
        });
    }
}
