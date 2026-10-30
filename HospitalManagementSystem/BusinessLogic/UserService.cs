using System;
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

        public (bool Success, string Role, string Message) Login(string email, string password)
        {
            var dt = _db.QuerySP("usp_tblUsers_GetByEmail",
                new SqlParameter("@Email", email));

            if (dt.Rows.Count == 0)
                return (false, "", "Invalid email or password");

            var row = dt.Rows[0];
            var hash = row["Password"] as string;
            var role = row["Role"] as string ?? "Staff";

            if (!PasswordHelper.Verify(password, hash ?? ""))
                return (false, "", "Invalid email or password");

            return (true, role, "Login successful");
        }

        public int Register(string email, string password, string role = "Staff")
        {
            if (role != "Admin" && role != "Staff")
                throw new ArgumentException("Role must be Admin or Staff");

            var hash = PasswordHelper.Hash(password);

            try
            {
                return _db.ExecuteSP("usp_tblUsers_Create",
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Password", hash),
                    new SqlParameter("@Role", role));
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new InvalidOperationException("Email already registered");
            }
        }

        // Old name the UI is calling
        public int CreateUser(string email, string passwordHash)
        {
            return _db.Execute(
                "INSERT INTO dbo.tblUsers (Email, Password, Role) VALUES (@e, @p, 'Staff')",
                new SqlParameter("@e", email),
                new SqlParameter("@p", passwordHash));
        }
    }
}