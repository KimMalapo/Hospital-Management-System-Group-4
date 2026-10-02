using System;

namespace Model
{
    // Simple DTO for lookup/master data items
    public class LookupItem
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
