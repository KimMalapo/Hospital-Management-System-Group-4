using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Model;

namespace BusinessLogic
{
    // Minimal repository: calls stored procedures like "usp_tblRoomTypes_Get"
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
            if (string.IsNullOrWhiteSpace(spBaseName)) throw new ArgumentException("spBaseName required", nameof(spBaseName));

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
            if (string.IsNullOrWhiteSpace(spBaseName)) throw new ArgumentException("spBaseName required", nameof(spBaseName));

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

        private static string? SafeGetString(SqlDataReader r, string name)
        {
            var idx = r.GetOrdinal(name);
            if (r.IsDBNull(idx)) return null;
            return r.GetString(idx);
        }
    }
}
