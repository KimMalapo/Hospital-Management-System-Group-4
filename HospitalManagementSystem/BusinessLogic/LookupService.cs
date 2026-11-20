using System;
using System.Collections.Generic;
using Model;

namespace BusinessLogic
{
    public class LookupService
    {
        private readonly ILookupRepository _repo;

        public LookupService(string connectionString) : this(new LookupRepository(connectionString)) { }

        public LookupService(ILookupRepository repository)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        private static string NormalizeTable(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Lookup type is required.", nameof(key));

            var table = key.Trim();
            if (!table.StartsWith("tbl", StringComparison.OrdinalIgnoreCase))
                table = "tbl" + table;

            return table.ToLowerInvariant() switch
            {
                "tbldiagnosiscodes" => "tblDiagnosisCodes",
                "tbldrugcategories" => "tblDrugCategories",
                "tblroomtypes" => "tblRoomTypes",
                "tbldepartmenttypes" => "tblDepartmentTypes",
                _ => throw new ArgumentException($"Unsupported lookup type '{key}'.", nameof(key))
            };
        }

        public List<LookupItem> GetAll(string key) => _repo.GetList(NormalizeTable(key));
        public LookupItem? GetById(string key, int id) => _repo.GetById(NormalizeTable(key), id);
        public List<LookupItem> Search(string key, string term) =>
            _repo.Search(NormalizeTable(key), string.IsNullOrWhiteSpace(term) ? string.Empty : term.Trim());

        public int Create(string key, LookupItem item)
        {
            var table = NormalizeTable(key);
            if (string.IsNullOrWhiteSpace(item.Code))
                throw new ArgumentException("Code is required");
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required");

            if (_repo.CodeExists(table, item.Code!))
                throw new InvalidOperationException($"Code '{item.Code}' already exists");

            return _repo.Create(table, item);
        }

        public int Update(string key, LookupItem item)
        {
            var table = NormalizeTable(key);
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(item.Status))
                throw new ArgumentException("Status is required");

            var existing = _repo.GetById(table, item.Id)
                ?? throw new ArgumentException("Record not found");

            if (!string.IsNullOrWhiteSpace(item.Code) && item.Code != existing.Code)
                throw new InvalidOperationException("Code cannot be changed");

            return _repo.Update(table, item);
        }

        public int SoftDelete(string key, int id)
        {
            var table = NormalizeTable(key);
            var item = _repo.GetById(table, id)
                ?? throw new ArgumentException("Record not found");

            if (_repo.IsCodeInUse(table, item.Code))
            {
                throw new InvalidOperationException(
                    $"Cannot deactivate '{item.Code}' because it is still in use.");
            }

            return _repo.SoftDelete(table, id);
        }
    }
}