using System;
using System.Collections.Generic;
using Model;

namespace BusinessLogic
{
    public class LookupService
    {
        private readonly LookupRepository _repo;

        public LookupService(string connectionString)
        {
            _repo = new LookupRepository(connectionString ?? throw new ArgumentNullException(nameof(connectionString)));
        }

        private string NormalizeTable(string key) => 
            key.StartsWith("tbl", StringComparison.OrdinalIgnoreCase) ? key : "tbl" + key;

        public List<LookupItem> GetAll(string lookupKey)
        {
            if (string.IsNullOrWhiteSpace(lookupKey)) 
                throw new ArgumentException("lookupKey required", nameof(lookupKey));
            return _repo.GetList(NormalizeTable(lookupKey));
        }

        public LookupItem? GetById(string lookupKey, int id)
        {
            if (string.IsNullOrWhiteSpace(lookupKey)) 
                throw new ArgumentException("lookupKey required", nameof(lookupKey));
            return _repo.GetById(NormalizeTable(lookupKey), id);
        }

        public int Update(string lookupKey, LookupItem item)
        {
            if (string.IsNullOrWhiteSpace(lookupKey)) 
                throw new ArgumentException("lookupKey required", nameof(lookupKey));
            if (item == null) 
                throw new ArgumentNullException(nameof(item));

            var table = NormalizeTable(lookupKey);
            var existing = _repo.GetById(table, item.Id);
            if (existing == null) 
                throw new ArgumentException($"No record with Id={item.Id}");

            if (!string.IsNullOrWhiteSpace(item.Code) && item.Code != existing.Code)
                throw new InvalidOperationException("Cannot change Code");

            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required", nameof(item.Name));
            if (string.IsNullOrWhiteSpace(item.Status))
                throw new ArgumentException("Status is required", nameof(item.Status));

            return _repo.Update(table, item);
        }

        public List<LookupItem> Search(string lookupKey, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(lookupKey)) 
                throw new ArgumentException("lookupKey required", nameof(lookupKey));
            if (string.IsNullOrWhiteSpace(searchTerm)) 
                throw new ArgumentException("searchTerm required", nameof(searchTerm));

            return _repo.Search(NormalizeTable(lookupKey), searchTerm);
        }
    }
}
