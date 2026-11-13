using System.Data;
using Microsoft.Data.SqlClient;

namespace BusinessLogic
{
    // REPLACES the existing BusinessLogic/DbExecutor.cs.
    // Change: every connection tells SQL Server who the app user is (SESSION_CONTEXT 'AppUser'),
    // so audit triggers can record the real person instead of the SQL login (needs the DB-06 trigger change).
    public class DbExecutor
    {
        private readonly string _connectionString;

        public DbExecutor(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private SqlConnection OpenConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();

            var user = CurrentUser.Email;
            if (!string.IsNullOrWhiteSpace(user))
            {
                try
                {
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "EXEC sp_set_session_context @key = N'AppUser', @value = @v;";
                    cmd.Parameters.Add(new SqlParameter("@v", SqlDbType.NVarChar, 200) { Value = user });
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Auditing detail must never stop the real work.
                    Logger.Error("DbExecutor: could not set AppUser session context", ex);
                }
            }
            return conn;
        }

        public DataTable Query(string sql, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using var conn = OpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public DataTable QuerySP(string storedProc, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using var conn = OpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = storedProc;
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public int Execute(string sql, params SqlParameter[] parameters)
        {
            using var conn = OpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
            return cmd.ExecuteNonQuery();
        }

        public int ExecuteSP(string storedProc, params SqlParameter[] parameters)
        {
            using var conn = OpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = storedProc;
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
            return cmd.ExecuteNonQuery();
        }

        public object? ExecuteScalarSP(string storedProc, params SqlParameter[] parameters)
        {
            using var conn = OpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = storedProc;
            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                foreach (var p in parameters) cmd.Parameters.Add(p);
            return cmd.ExecuteScalar();
        }
    }
}
