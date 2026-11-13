using System.Data;

namespace Model;

/// <summary>Everything the receipt print layout needs, loaded from the database.</summary>
public class ReceiptData
{
    public int PaymentID { get; set; }
    public string? ReceiptNo { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PatientName { get; set; }
    public string? BillNo { get; set; }
    public string? AdmissionNo { get; set; }
    public decimal Amount { get; set; }
    public string? Method { get; set; }
    public string? ReferenceNo { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? ReceivedBy { get; set; }
}

/// <summary>Everything the billing statement print layout needs.</summary>
public class StatementData
{
    public Bill Bill { get; set; } = new();
    public Admission Admission { get; set; } = new();
    public Patient? Patient { get; set; }
    public DataTable Items { get; set; } = new();
    public DataTable Payments { get; set; } = new();
}

/// <summary>The final patient record shown after discharge.</summary>
public class DischargeRecord
{
    public Admission Admission { get; set; } = new();
    public Bill? Bill { get; set; }
    public DataTable Charges { get; set; } = new();
    public DataTable Payments { get; set; } = new();
}

/// <summary>Result of checking whether an admission can be discharged now.</summary>
public record DischargeCheck(bool CanDischarge, string Message);

public class DashboardCounts
{
    public int ActiveAdmissions { get; set; }
    public int AvailableRooms { get; set; }
    public int UnpaidBills { get; set; }
    public decimal TodaysCollections { get; set; }
}
