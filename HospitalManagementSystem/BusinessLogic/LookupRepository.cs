using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Model;

namespace BusinessLogic
{
    // Minimal repository: calls stored procedures like "usp_tblRoomTypes_Get" and update/check procs
    public class LookupRepository
    {
        private readonly string _connectionString;

        public LookupRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        // spBaseName should be the table name used in stored procs, e.g. "tblRoomTypes"
        public List<LookupItem> GetList(string spBaseName)
        {
            if (string.IsNullOrWhiteSpace(spBaseName))
                throw new ArgumentException("spBaseName required", nameof(spBaseName));

            var result = new List<LookupItem>();
            var spName = $"usp_{spBaseName}_Get";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new LookupItem
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Code = SafeGetString(reader, "Code"),
                    Name = SafeGetString(reader, "Name"),
                    Status = SafeGetString(reader, "Status"),
                    Notes = SafeGetString(reader, "Notes")
                };

                result.Add(item);
            }

            return result;
        }

        public LookupItem? GetById(string spBaseName, int id)
        {
            if (string.IsNullOrWhiteSpace(spBaseName))
                throw new ArgumentException("spBaseName required", nameof(spBaseName));

            var spName = $"usp_{spBaseName}_Get";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new LookupItem
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Code = SafeGetString(reader, "Code"),
                    Name = SafeGetString(reader, "Name"),
                    Status = SafeGetString(reader, "Status"),
                    Notes = SafeGetString(reader, "Notes")
                };
            }

            return null;
        }

        // Checks if a code exists in the table, excluding an optional id (used for uniqueness validation)
        public bool CheckCodeExists(string spBaseName, string code, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(spBaseName))
                throw new ArgumentException("spBaseName required", nameof(spBaseName));
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("code required", nameof(code));

            var spName = $"usp_{spBaseName}_CheckCodeExists";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;
            cmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.NVarChar, 50) { Value = code });

            if (excludeId.HasValue)
                cmd.Parameters.Add(new SqlParameter("@ExcludeId", SqlDbType.Int) { Value = excludeId.Value });
            else
                cmd.Parameters.Add(new SqlParameter("@ExcludeId", SqlDbType.Int) { Value = DBNull.Value });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var count = reader.GetInt32(reader.GetOrdinal("CodeExists"));
                return count > 0;
            }

            return false;
        }

        // Update item using the table-specific update stored procedure. Returns affected rows.
        public int Update(string spBaseName, LookupItem item)
        {
            if (string.IsNullOrWhiteSpace(spBaseName))
                throw new ArgumentException("spBaseName required", nameof(spBaseName));
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var spName = $"usp_{spBaseName}_Update";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;

            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = item.Id });

            // DiagnosisCodes uses parameter name @Description instead of @Name
            if (string.Equals(spBaseName, "tblDiagnosisCodes", StringComparison.OrdinalIgnoreCase))
            {
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 400) { Value = (object?)item.Name ?? DBNull.Value });
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 200) { Value = (object?)item.Name ?? DBNull.Value });
            }

            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.NVarChar, 50) { Value = (object?)item.Status ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.NVarChar, 400) { Value = (object?)item.Notes ?? DBNull.Value });

            conn.Open();
            // Stored procs return SELECT @@ROWCOUNT AS AffectedRows; use ExecuteScalar to get the value
            var result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value)
                return 0;

            if (int.TryParse(result.ToString(), out var rows))
                return rows;

            return 0;
        }

        // Search for lookup items by keyword (Code or Name)
        public List<LookupItem> Search(string spBaseName, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(spBaseName))
                throw new ArgumentException("spBaseName required", nameof(spBaseName));
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("searchTerm required", nameof(searchTerm));

            var result = new List<LookupItem>();
            var spName = $"usp_{spBaseName}_Search";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = spName;
            cmd.Parameters.Add(new SqlParameter("@SearchTerm", SqlDbType.NVarChar, 200) { Value = searchTerm });

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new LookupItem
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Code = SafeGetString(reader, "Code"),
                    Name = SafeGetString(reader, "Name"),
                    Status = SafeGetString(reader, "Status"),
                    Notes = SafeGetString(reader, "Notes")
                };

                result.Add(item);
            }

            return result;
        }

        private static string? SafeGetString(SqlDataReader reader, string name)
        {
            var idx = reader.GetOrdinal(name);
            if (reader.IsDBNull(idx))
                return null;
            return reader.GetString(idx);
        }
    }
}
