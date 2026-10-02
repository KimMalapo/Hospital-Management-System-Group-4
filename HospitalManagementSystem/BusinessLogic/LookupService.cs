using System;
using System.Collections.Generic;
using Model;

namespace BusinessLogic
{
    // Small service wrapping the repository. Keeps calling code simple.
    public class LookupService
    {
        private readonly LookupRepository _repo;

        public LookupService(string connectionString)
        {
            _repo = new LookupRepository(connectionString ?? throw new ArgumentNullException(nameof(connectionString)));
        }

        // Public method expects a simple lookup key like "RoomTypes", "DrugCategories", "DiagnosisCodes", "DepartmentTypes"
        // It maps to stored procs named like "usp_tblRoomTypes_Get" by prefixing with "tbl" when needed.
        public List<LookupItem> GetAll(string lookupKey)
        {
            if (string.IsNullOrWhiteSpace(lookupKey)) throw new ArgumentException("lookupKey required", nameof(lookupKey));

            // Normalize key to the table name used in stored procs
            // Accept keys like "RoomTypes" or "tblRoomTypes"
            var table = lookupKey.StartsWith("tbl", StringComparison.OrdinalIgnoreCase) ? lookupKey : "tbl" + lookupKey;
            return _repo.GetList(table);
        }

        public LookupItem? GetById(string lookupKey, int id)
        {
            if (string.IsNullOrWhiteSpace(lookupKey)) throw new ArgumentException("lookupKey required", nameof(lookupKey));
            var table = lookupKey.StartsWith("tbl", StringComparison.OrdinalIgnoreCase) ? lookupKey : "tbl" + lookupKey;
            return _repo.GetById(table, id);
        }

        // Update an existing lookup/master data item.
        // Validation rules applied: required fields (Name/Status) and code-change protection.
        // Returns number of rows affected.
        public int Update(string lookupKey, LookupItem item)
        {
            if (string.IsNullOrWhiteSpace(lookupKey)) throw new ArgumentException("lookupKey required", nameof(lookupKey));
            if (item == null) throw new ArgumentNullException(nameof(item));

            var table = lookupKey.StartsWith("tbl", StringComparison.OrdinalIgnoreCase) ? lookupKey : "tbl" + lookupKey;

            // Fetch existing record to validate existence and prevent changing Code
            var existing = _repo.GetById(table, item.Id);
            if (existing == null) throw new ArgumentException($"No record with Id={item.Id} in {table}");

            // Code change is not allowed by DB update procedures; enforce here.
            if (!string.IsNullOrWhiteSpace(item.Code) && !string.Equals(item.Code, existing.Code, StringComparison.Ordinal))
                throw new InvalidOperationException("Changing the Code is not allowed. To change Code safely, create a new record and retire the old one.");

            // Required fields: Name/Description and Status
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name/Description is required", nameof(item.Name));
            if (string.IsNullOrWhiteSpace(item.Status))
                throw new ArgumentException("Status is required", nameof(item.Status));

            // Optionally check uniqueness if caller attempted to change Code (we disallow), but keep check available
            // Call repository update
            var rows = _repo.Update(table, item);
            return rows;
        }
    }
}
