namespace Model
{
    /// <summary>One row of a lookup list (department, diagnosis code...).</summary>
    public class LookupItem
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}