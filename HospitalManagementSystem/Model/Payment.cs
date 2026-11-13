namespace Model;

public class Payment
{
    public int PaymentID { get; set; }
    public string? ReceiptNo { get; set; }
    public int BillID { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string? Method { get; set; }
    public string? ReferenceNo { get; set; }
    public string? ReceivedBy { get; set; }
    public string Status { get; set; } = "Posted";
    public string? VoidReason { get; set; }
}
