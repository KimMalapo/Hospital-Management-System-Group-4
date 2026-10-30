using System.Data;
using Microsoft.Data.SqlClient;

namespace BusinessLogic
{
    public class DbExecutor
    {
        private readonly string _connectionString;

        public DbExecutor(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public DataTable Query(string sql, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public DataTable QuerySP(string storedProc, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = storedProc;
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
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
            cmd.CommandType = CommandType.Text;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int ExecuteSP(string storedProc, params SqlParameter[] parameters)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = storedProc;
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public object? ExecuteScalarSP(string storedProc, params SqlParameter[] parameters)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = storedProc;
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }
}