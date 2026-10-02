using System.Data;
using Microsoft.Data.SqlClient;

namespace BusinessLogic
{
    // Lightweight DB executor used by service classes
    public class DbExecutor
    {
        private readonly string _connectionString;

        public DbExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string sql, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using var conn = new SqlConnection(_connectionString);
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

        public int Execute(string sql, params SqlParameter[] parameters)
        {
            using var conn = new SqlConnection(_connectionString);
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
