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
    }
}
