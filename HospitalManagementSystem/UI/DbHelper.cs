using System.Data;
using Microsoft.Data.SqlClient;

namespace UI
{
    // Minimal DB helper for forms: parameterized query and execute helpers
    public static class DbHelper
    {
        public static DataTable Query(string sql, params SqlParameter[] parameters) =>
            Run(sql, parameters, cmd =>
            {
                var dt = new DataTable();
                using var reader = cmd.ExecuteReader();
                dt.Load(reader);
                return dt;
            });

        public static int Execute(string sql, params SqlParameter[] parameters) =>
            Run(sql, parameters, cmd => cmd.ExecuteNonQuery());

        public static object? ExecuteScalar(string sql, params SqlParameter[] parameters) =>
            Run<object?>(sql, parameters, cmd => cmd.ExecuteScalar());

        private static T Run<T>(string sql, SqlParameter[] parameters, Func<SqlCommand, T> execute)
        {
            using var conn = new SqlConnection(Config.GetConnectionString());
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            if (parameters != null)
            {
                foreach (var p in parameters) cmd.Parameters.Add(p);
            }

            conn.Open();
            return execute(cmd);
        }
    }
}
