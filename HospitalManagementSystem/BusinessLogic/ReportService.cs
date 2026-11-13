using System.Data;
using Model;

namespace BusinessLogic;

/// <summary>
/// One place for all reports, receipts and statements. Each method validates input, checks the role,
/// and calls a stored procedure: nothing here is hardcoded, so every figure comes from real records.
/// Reports are Admin only. Receipts, statements and dashboard numbers are open to any logged-in user.
/// Dates are inclusive: the stored procedures include the whole "To" day.
/// </summary>
public class ReportService : ServiceBase
{
    private readonly BillingService _billing;
    private readonly PaymentService _payments;
    private readonly AdmissionService _admissions;
    private readonly PatientService _patients;

    public ReportService(string connectionString) : base(connectionString)
    {
        _billing = new BillingService(connectionString);
        _payments = new PaymentService(connectionString);
        _admissions = new AdmissionService(connectionString);
        _patients = new PatientService(connectionString);
    }

    private static void ValidateRange(DateTime from, DateTime to)
    {
        if (from.Date > to.Date)
            throw new ValidationException("The 'From' date cannot be later than the 'To' date.", "From");
    }

    // ---------- Reports (Admin) ----------

    public DataTable BillingSummary(DateTime from, DateTime to)
    {
        ValidateRange(from, to);
        Permissions.Require(AppAction.ViewReports);
        return Run("ReportService.BillingSummary", () =>
            Db.QuerySP("usp_Report_BillingSummary", P("@From", from.Date), P("@To", to.Date)));
    }

    public DataTable PaymentHistory(DateTime from, DateTime to, int? patientId = null)
    {
        ValidateRange(from, to);
        Permissions.Require(AppAction.ViewReports);
        return Run("ReportService.PaymentHistory", () =>
            Db.QuerySP("usp_Report_PaymentHistory",
                P("@From", from.Date), P("@To", to.Date), P("@PatientID", patientId)));
    }

    public DataTable DailyCollections(DateTime date)
    {
        Permissions.Require(AppAction.ViewReports);
        return Run("ReportService.DailyCollections", () =>
            Db.QuerySP("usp_Report_DailyCollections", P("@Date", date.Date)));
    }

    public DataTable OutstandingBalances()
    {
        Permissions.Require(AppAction.ViewReports);
        return Run("ReportService.OutstandingBalances", () => Db.QuerySP("usp_Report_OutstandingBalances"));
    }

    /// <param name="status">Null for all, or Admitted / Discharged / Cancelled.</param>
    public DataTable Admissions(DateTime from, DateTime to, string? status = null)
    {
        ValidateRange(from, to);
        string? chosen = null;
        if (!string.IsNullOrWhiteSpace(status))
        {
            chosen = AdmissionStatus.All.FirstOrDefault(s => s.Equals(status.Trim(), StringComparison.OrdinalIgnoreCase));
            if (chosen == null)
                throw new ValidationException(
                    "Status must be one of: " + string.Join(", ", AdmissionStatus.All) + ".", "Status");
        }
        Permissions.Require(AppAction.ViewReports);
        return Run("ReportService.Admissions", () =>
            Db.QuerySP("usp_Report_Admissions", P("@From", from.Date), P("@To", to.Date), P("@Status", chosen)));
    }

    public DataTable RoomOccupancy()
    {
        Permissions.Require(AppAction.ViewReports);
        return Run("ReportService.RoomOccupancy", () => Db.QuerySP("usp_Report_RoomOccupancy"));
    }

    public DataTable DischargeSummary(int admissionId)
    {
        if (admissionId <= 0)
            throw new ValidationException("Select an admission first.", "Admission");
        Permissions.Require(AppAction.ViewReports);
        return Run("ReportService.DischargeSummary", () =>
            Db.QuerySP("usp_Report_DischargeSummary", P("@AdmissionID", admissionId)));
    }

    // ---------- Open to any logged-in user ----------

    public DashboardCounts GetDashboardCounts()
    {
        Permissions.RequireLogin();
        return Run("ReportService.GetDashboardCounts", () =>
        {
            var dt = Db.QuerySP("usp_Dashboard_Counts");
            var counts = new DashboardCounts();
            if (dt.Rows.Count > 0)
            {
                var r = dt.Rows[0];
                counts.ActiveAdmissions = RowReader.Int(r, "ActiveAdmissions");
                counts.AvailableRooms = RowReader.Int(r, "AvailableRooms");
                counts.UnpaidBills = RowReader.Int(r, "UnpaidBills");
                counts.TodaysCollections = RowReader.Dec(r, "TodaysCollections");
            }
            return counts;
        });
    }

    /// <summary>Receipt print data for one payment.</summary>
    public ReceiptData GetReceipt(int paymentId)
    {
        if (paymentId <= 0)
            throw new ValidationException("Select a payment first.", "Payment");
        Permissions.RequireLogin();
        return _payments.GetReceipt(paymentId)
            ?? throw new ValidationException("Payment not found.", "Payment");
    }

    /// <summary>Billing statement print data: bill, patient, itemized lines and payments.</summary>
    public StatementData GetStatement(int billId)
    {
        if (billId <= 0)
            throw new ValidationException("Select a bill first.", "Bill");
        Permissions.RequireLogin();

        var bill = _billing.GetById(billId) ?? throw new ValidationException("Bill not found.", "Bill");
        var admission = _admissions.GetById(bill.AdmissionID)
            ?? throw new ValidationException("The admission of this bill was not found.", "Admission");

        return Run("ReportService.GetStatement", () => new StatementData
        {
            Bill = bill,
            Admission = admission,
            Patient = _patients.GetById(admission.PatientID),
            Items = _billing.GetItems(billId),
            Payments = _payments.GetByBill(billId)
        });
    }
}
