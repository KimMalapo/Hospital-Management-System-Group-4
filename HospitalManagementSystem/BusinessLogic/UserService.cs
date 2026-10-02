using System.Data;
using Microsoft.Data.SqlClient;

namespace BusinessLogic
{
    public class UserService
    {
        private readonly DbExecutor _db;

        public UserService(string connectionString)
        {
            _db = new DbExecutor(connectionString);
        }

        public int CreateUser(string email, string passwordHash)
        {
            var sql = "INSERT INTO dbo.tblUsers (Email, Password) VALUES (@e, @p);";
            var p1 = new SqlParameter("@e", System.Data.SqlDbType.NVarChar) { Value = (object)email ?? System.DBNull.Value };
            var p2 = new SqlParameter("@p", System.Data.SqlDbType.NVarChar) { Value = (object)passwordHash ?? System.DBNull.Value };
            return _db.Execute(sql, p1, p2);
        }

        public DataTable GetUserByEmail(string email)
        {
            var sql = "SELECT UserID, Email, Password FROM dbo.tblUsers WHERE Email = @e";
            var p = new SqlParameter("@e", System.Data.SqlDbType.NVarChar) { Value = (object)email ?? System.DBNull.Value };
            return _db.Query(sql, p);
        }

        public DataTable GetAllUsers()
        {
            return _db.Query("SELECT UserID, Email FROM dbo.tblUsers ORDER BY UserID");
        }
    }
}
