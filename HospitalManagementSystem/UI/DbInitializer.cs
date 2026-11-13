using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;
using Microsoft.Data.SqlClient;

namespace UI
{
    /// <summary>
    /// Start-up database check. It never creates a partial schema: if the database or any required table or
    /// procedure is missing, it shows a clear message and returns false so the application can stop cleanly.
    /// It also makes sure the demo accounts exist and that every stored password is a BCrypt hash.
    /// </summary>
    internal static class DbInitializer
    {
        /// <summary>The last problem found, or null when the database is ready.</summary>
        public static string? StartupError { get; private set; }

        // Objects the application cannot run without
        private static readonly string[] RequiredTables =
        {
            "tblUsers", "tblPatients", "tblRooms", "tblRoomAssignments", "tblRoomTypes", "tblDepartmentTypes",
            "tblDiagnosisCodes", "tblDrugCategories", "tblAuditLog", "tblAdmissions", "tblServices",
            "tblAdmissionCharges", "tblBills", "tblBillItems", "tblPayments"
        };

        private static readonly string[] RequiredProcedures =
        {
            "usp_tblUsers_GetByEmail", "usp_tblPatients_GetAll", "usp_tblRooms_GetAll",
            "usp_tblAdmissions_Admit", "usp_tblServices_GetAll", "usp_tblAdmissionCharges_Add",
            "usp_tblBills_Generate", "usp_tblPayments_Post", "usp_Report_BillingSummary", "usp_Dashboard_Counts"
        };

        /// <summary>Returns true when the application can start. Shows a message box when it cannot.</summary>
        public static bool EnsureDatabase(bool showMessage = true)
        {
            StartupError = null;
            try
            {
                string connection;
                try { connection = Config.GetConnectionString(); }
                catch (Exception ex)
                {
                    return Fail(ex.Message, showMessage);
                }

                if (!TryOpen(connection, out var openError))
                    return Fail("The application cannot connect to the database.\n\n" + openError +
                                "\n\nCheck HMS_CONNECTION_STRING or appsettings.json, and that SQL Server / LocalDB is running. " +
                                "See docs/DATABASE_SETUP.md.", showMessage);

                var missing = FindMissingObjects(connection);
                if (missing.Count > 0)
                    return Fail("The database is not fully set up. Missing: " + string.Join(", ", missing.Take(8)) +
                                (missing.Count > 8 ? $" and {missing.Count - 8} more" : "") +
                                ".\n\nPublish the DB project to your database (see docs/DATABASE_SETUP.md), then start the app again.",
                                showMessage);

                EnsureDemoUsers(connection);
                MigratePlaintextPasswords(connection);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("DbInitializer.EnsureDatabase failed", ex);
                return Fail("Unexpected problem while checking the database: " + ex.Message, showMessage);
            }
        }

        private static bool Fail(string message, bool show)
        {
            StartupError = message;
            Logger.Error("DbInitializer: " + message, new InvalidOperationException(message));
            if (show)
                MessageBox.Show(message, "Database not ready", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private static bool TryOpen(string connectionString, out string error)
        {
            try
            {
                using var c = new SqlConnection(connectionString);
                c.Open();
                error = "";
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("DbInitializer.TryOpen failed", ex);
                error = ex.Message;
                return false;
            }
        }

        private static List<string> FindMissingObjects(string connectionString)
        {
            var missing = new List<string>();
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            foreach (var t in RequiredTables)
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT OBJECT_ID(@n, N'U')";
                cmd.Parameters.AddWithValue("@n", "dbo." + t);
                if (cmd.ExecuteScalar() is DBNull or null) missing.Add(t);
            }
            foreach (var p in RequiredProcedures)
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT OBJECT_ID(@n, N'P')";
                cmd.Parameters.AddWithValue("@n", "dbo." + p);
                if (cmd.ExecuteScalar() is DBNull or null) missing.Add(p);
            }
            return missing;
        }

        /// <summary>
        /// Creates the demo Admin and Staff accounts when no user with that e-mail exists. Passwords are hashed here,
        /// never stored as plain text in SQL. Change them before any real use.
        /// </summary>
        private static void EnsureDemoUsers(string connectionString)
        {
            var demo = new[]
            {
                (Email: "admin@example.com", Password: "admin123", Role: "Admin"),
                (Email: "staff@example.com", Password: "staff123", Role: "Staff")
            };

            using var conn = new SqlConnection(connectionString);
            conn.Open();
            foreach (var u in demo)
            {
                using var exists = conn.CreateCommand();
                exists.CommandText = "SELECT COUNT(1) FROM dbo.tblUsers WHERE Email = @e";
                exists.Parameters.AddWithValue("@e", u.Email);
                if (Convert.ToInt32(exists.ExecuteScalar() ?? 0) > 0) continue;

                using var insert = conn.CreateCommand();
                insert.CommandText = "INSERT INTO dbo.tblUsers (Email, Password, Role) VALUES (@e, @p, @r);";
                insert.Parameters.AddWithValue("@e", u.Email);
                insert.Parameters.AddWithValue("@p", BCrypt.Net.BCrypt.HashPassword(u.Password));
                insert.Parameters.AddWithValue("@r", u.Role);
                insert.ExecuteNonQuery();
            }
        }

        private static void MigratePlaintextPasswords(string connectionString)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var select = conn.CreateCommand();
                select.CommandText = "SELECT UserID, Password FROM dbo.tblUsers";
                using var reader = select.ExecuteReader();
                var updates = new List<(int id, string hash)>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var pwd = reader.IsDBNull(1) ? null : reader.GetString(1);
                    if (string.IsNullOrEmpty(pwd)) continue;
                    if (pwd.StartsWith("$2")) continue; // already BCrypt
                    updates.Add((id, BCrypt.Net.BCrypt.HashPassword(pwd)));
                }
                reader.Close();

                foreach (var u in updates)
                {
                    using var upd = conn.CreateCommand();
                    upd.CommandText = "UPDATE dbo.tblUsers SET Password = @p WHERE UserID = @id";
                    upd.Parameters.AddWithValue("@p", u.hash);
                    upd.Parameters.AddWithValue("@id", u.id);
                    upd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("DbInitializer.MigratePlaintextPasswords failed", ex);
            }
        }
    }
}
