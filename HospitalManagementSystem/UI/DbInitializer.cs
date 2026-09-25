using System;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace UI
{
    internal static class DbInitializer
    {
        // Patch: no-op update to keep file up-to-date.
        // Ensure a working DB exists. Prefer configured DB; otherwise create a localdb fallback.
        public static void EnsureDatabase()
        {
            try
            {
                // prefer the shared Config helper for connection string resolution
                var configured = Config.GetConnectionString();
                if (!string.IsNullOrEmpty(configured))
                {
                    // if configured DB is reachable, ensure passwords are hashed there
                    if (TryOpen(configured))
                    {
                        MigratePlaintextPasswords(configured);
                        return;
                    }
                }

                // fallback: create localdb HospitalDB and ensure tblUsers exists
                var masterConn = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=master;";
                using (var m = new SqlConnection(masterConn))
                {
                    m.Open();
                    using var c = m.CreateCommand();
                    c.CommandText = "IF DB_ID('HospitalDB') IS NULL CREATE DATABASE [HospitalDB];";
                    c.ExecuteNonQuery();
                }

                var localConn = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=HospitalDB;";
                using (var conn = new SqlConnection(localConn))
                {
                    conn.Open();
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = @"IF OBJECT_ID(N'dbo.tblUsers', N'U') IS NULL
CREATE TABLE dbo.tblUsers (UserID INT IDENTITY(1,1) PRIMARY KEY, Email NVARCHAR(200) NOT NULL, Password NVARCHAR(200) NOT NULL);";
                    cmd.ExecuteNonQuery();

                    using var countCmd = conn.CreateCommand();
                    countCmd.CommandText = "SELECT COUNT(1) FROM dbo.tblUsers;";
                    var cnt = Convert.ToInt32(countCmd.ExecuteScalar() ?? 0);
                    if (cnt == 0)
                    {
                        var hashed = BCrypt.Net.BCrypt.HashPassword("admin123");
                        using var insert = conn.CreateCommand();
                        insert.CommandText = "INSERT INTO dbo.tblUsers (Email, Password) VALUES (@e, @p);";
                        insert.Parameters.AddWithValue("@e", "admin@example.com");
                        insert.Parameters.AddWithValue("@p", hashed);
                        insert.ExecuteNonQuery();
                    }

                    MigratePlaintextPasswords(localConn);
                }

                try { File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "localdb_initialized.txt"), DateTime.UtcNow.ToString("o")); } catch { }
            }
            catch
            {
                // best-effort only
            }
        }

        private static bool TryOpen(string connectionString)
        {
            try
            {
                using var c = new SqlConnection(connectionString);
                c.Open();
                c.Close();
                return true;
            }
            catch
            {
                return false;
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
                var updates = new System.Collections.Generic.List<(int id, string hash)>();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var pwd = reader.IsDBNull(1) ? null : reader.GetString(1);
                    if (string.IsNullOrEmpty(pwd)) continue;
                    if (pwd.StartsWith("$2")) continue; // already BCrypt
                    var hashed = BCrypt.Net.BCrypt.HashPassword(pwd);
                    updates.Add((id, hashed));
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
            catch
            {
                // ignore migration failures
            }
        }
    }
}
