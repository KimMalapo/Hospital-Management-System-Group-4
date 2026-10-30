using System;
using System.Collections.Generic;
using Model;

namespace BusinessLogic
{
    public class LookupService
    {
        private readonly LookupRepository _repo;
        private readonly DbExecutor _db;

        public LookupService(string connectionString)
        {
            _repo = new LookupRepository(connectionString);
            _db = new DbExecutor(connectionString);
        }

        private string T(string key) =>
            key.StartsWith("tbl", StringComparison.OrdinalIgnoreCase) ? key : "tbl" + key;

        public List<LookupItem> GetAll(string key) => _repo.GetList(T(key));
        public LookupItem? GetById(string key, int id) => _repo.GetById(T(key), id);
        public List<LookupItem> Search(string key, string term) => _repo.Search(T(key), term);

        public int Create(string key, LookupItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Code))
                throw new ArgumentException("Code is required");
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required");

            if (_repo.CodeExists(T(key), item.Code!))
                throw new InvalidOperationException($"Code '{item.Code}' already exists");

            return _repo.Create(T(key), item);
        }

        public int Update(string key, LookupItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(item.Status))
                throw new ArgumentException("Status is required");

            var existing = _repo.GetById(T(key), item.Id)
                ?? throw new ArgumentException("Record not found");

            if (!string.IsNullOrWhiteSpace(item.Code) && item.Code != existing.Code)
                throw new InvalidOperationException("Code cannot be changed");

            return _repo.Update(T(key), item);
        }

        public int SoftDelete(string key, int id)
        {
            var item = _repo.GetById(T(key), id)
                ?? throw new ArgumentException("Record not found");

            if (IsCodeInUse(key, item.Code))
            {
                throw new InvalidOperationException(
                    $"Cannot deactivate '{item.Code}' because it is still in use.");
            }

            return _repo.SoftDelete(T(key), id);
        }

        private bool IsCodeInUse(string lookupKey, string? code)
        {
            if (string.IsNullOrWhiteSpace(code)) return false;

            try
            {
                if (lookupKey.Contains("RoomType", StringComparison.OrdinalIgnoreCase))
                {
                    var dt = _db.Query(
                        "SELECT COUNT(*) FROM dbo.tblRooms WHERE RoomType = @code",
                        new Microsoft.Data.SqlClient.SqlParameter("@code", code));
                    return Convert.ToInt32(dt.Rows[0][0]) > 0;
                }
            }
            catch { }

            return false;
        }
    }
}