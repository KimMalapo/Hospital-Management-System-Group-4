namespace Model;

public class Bill
{
    public int BillID { get; set; }
    public string? BillNo { get; set; }
    public int AdmissionID { get; set; }
    public decimal RoomCharges { get; set; }
    public decimal ServiceCharges { get; set; }
    public decimal Discount { get; set; }
    public string? DiscountReason { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal Balance { get; set; }
    public string Status { get; set; } = BillStatus.Open;
    public DateTime? GeneratedAt { get; set; }
    public string? GeneratedBy { get; set; }

    /// <summary>Room + services before the discount.</summary>
    public decimal Subtotal => RoomCharges + ServiceCharges;
}

public class BillItem
{
    public int BillItemID { get; set; }
    public int BillID { get; set; }
    public string? ItemType { get; set; }   // "Room" or "Service"
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }
}
