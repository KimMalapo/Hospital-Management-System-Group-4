using System.Data;
using Microsoft.Data.SqlClient;

namespace UI
{
    // Minimal DB helper for forms: parameterized query and execute helpers
    public static class DbHelper
    {
        public static DataTable Query(string sql, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            var cs = Config.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs)) return dt;

            using var conn = new SqlConnection(cs);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                foreach (var p in parameters) cmd.Parameters.Add(p);
            }

            conn.Open();
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public static int Execute(string sql, params SqlParameter[] parameters)
        {
            var cs = Config.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs)) return 0;

            using var conn = new SqlConnection(cs);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                foreach (var p in parameters) cmd.Parameters.Add(p);
            }

            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}
