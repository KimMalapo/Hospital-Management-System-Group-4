namespace Model;

public class Charge
{
    public int ChargeID { get; set; }
    public int AdmissionID { get; set; }
    public int ServiceID { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
    public DateTime? ChargedAt { get; set; }
    public string? ChargedBy { get; set; }
    public bool IsVoided { get; set; }
    public string? VoidReason { get; set; }
}
