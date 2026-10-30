using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Model;

namespace BusinessLogic
{
    public class LookupRepository
    {
        private readonly string _cs;

        public LookupRepository(string connectionString)
        {
            _cs = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<LookupItem> GetList(string table)
        {
            var list = new List<LookupItem>();
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand($"usp_{table}_Get", conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
                list.Add(Map(r));
            return list;
        }

        public LookupItem? GetById(string table, int id)
        {
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand($"usp_{table}_GetById", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            using var r = cmd.ExecuteReader();
            return r.Read() ? Map(r) : null;
        }

        public bool CodeExists(string table, string code)
        {
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand($"usp_{table}_CheckCodeExists", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Code", code);
            conn.Open();
            var result = cmd.ExecuteScalar();
            return result != null && Convert.ToInt32(result) > 0;
        }

        public int Create(string table, LookupItem item)
        {
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand($"usp_{table}_Create", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Code", (object?)item.Code ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Name", (object?)item.Name ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", (object?)item.Status ?? "Active");
            cmd.Parameters.AddWithValue("@Notes", (object?)item.Notes ?? DBNull.Value);
            conn.Open();
            var id = cmd.ExecuteScalar();
            return id == null ? 0 : Convert.ToInt32(id);
        }

        public int Update(string table, LookupItem item)
        {
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand($"usp_{table}_Update", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", item.Id);
            cmd.Parameters.AddWithValue("@Name", (object?)item.Name ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", (object?)item.Status ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", (object?)item.Notes ?? DBNull.Value);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int SoftDelete(string table, int id)
        {
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand($"usp_{table}_SoftDelete", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public List<LookupItem> Search(string table, string term)
        {
            var list = new List<LookupItem>();
            using var conn = new SqlConnection(_cs);
            using var cmd = new SqlCommand($"usp_{table}_Search", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@SearchTerm", term);
            conn.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(Map(r));
            return list;
        }

        private static LookupItem Map(SqlDataReader r)
        {
            return new LookupItem
            {
                Id = r.GetInt32(r.GetOrdinal("Id")),
                Code = r["Code"] as string,
                Name = r["Name"] as string,
                Status = r["Status"] as string,
                Notes = r["Notes"] as string,
                IsDeleted = r.GetSchemaTable()?.Columns.Contains("IsDeleted") == true &&
                            r["IsDeleted"] != DBNull.Value && Convert.ToBoolean(r["IsDeleted"])
            };
        }
    }
}