namespace Model;

/// <summary>One entry of the hospital service price list (tblServices).</summary>
public class ServiceItem
{
    public int ServiceID { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? DrugCategoryCode { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; } = true;
}
