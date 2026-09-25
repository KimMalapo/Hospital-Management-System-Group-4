using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace UI
{
    public partial class LoginForm : Form
    {
        private static readonly string LocalDbConnectionString = new SqlConnectionStringBuilder
        {
            DataSource = "(localdb)\\MSSQLLocalDB",
            IntegratedSecurity = true,
            InitialCatalog = "HospitalDB",
            TrustServerCertificate = true
        }.ConnectionString;

        public LoginForm()
        {
            InitializeComponent();
        }

        private static string GenerateJwtToken(string userId, string email)
        {
            // secret from env or fallback to a local value
            var secret = Environment.GetEnvironmentVariable("HMS_JWT_SECRET") ?? "ReplaceThisWithASecureSecretInEnv";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: "HMS",
                audience: "HMSClients",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Designer wires btnLogin to btnLogin_Click; forward to shared handler
        private void btnLogin_Click(object? sender, EventArgs e)
        {
            BtnLogin_Click(sender, e);
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var connStr = GetConnectionString();
            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("Database connection string is not configured. Set environment variable 'HMS_CONNECTION_STRING' or provide UI/appsettings.json.", "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using var conn = new SqlConnection(connStr);
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT UserID, Email, Password FROM dbo.tblUsers WHERE Email = @email OR Email = @username";
                cmd.Parameters.AddWithValue("@email", username);
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();
                using var reader = cmd.ExecuteReader();
                int userId = -1;
                string? storedHash = null;
                string? email = null;
                if (reader.Read())
                {
                    userId = reader.GetInt32(0);
                    email = reader.IsDBNull(1) ? null : reader.GetString(1);
                    storedHash = reader.IsDBNull(2) ? null : reader.GetString(2);
                }

                if (userId != -1 && storedHash != null && BCrypt.Net.BCrypt.Verify(password, storedHash))
                {
                    // generate JWT token
                    var token = GenerateJwtToken(userId.ToString(), email ?? username);
                    MessageBox.Show("Login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // store token in-memory for this session (simple approach)
                    try
                    {
                        Application.UserAppDataRegistry.SetValue("AuthToken", token);
                        AuthService.SetToken(token);
                    }
                    catch
                    {
                        // ignore registry write failures
                    }

                    var main = new RoomManagement();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid email/username or password.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string? GetConnectionString()
        {
            // 1) environment variable takes precedence
            var env = Environment.GetEnvironmentVariable("HMS_CONNECTION_STRING");
            if (!string.IsNullOrEmpty(env))
            {
                return ApplyTrustServerCertificate(env);
            }

            // 2) appsettings.json in UI project directory
            var cs = LoadConnectionStringFromConfig();
            if (!string.IsNullOrEmpty(cs))
            {
                return ValidateAndReturnConnectionString(cs);
            }

            return null;
        }

        private static string ApplyTrustServerCertificate(string connectionString)
        {
            try
            {
                var b = new SqlConnectionStringBuilder(connectionString)
                {
                    TrustServerCertificate = true
                };
                return b.ConnectionString;
            }
            catch
            {
                return connectionString;
            }
        }

        private static string? LoadConnectionStringFromConfig()
        {
            try
            {
                var basePath = AppContext.BaseDirectory;
                var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables();
                var config = configBuilder.Build();
                return config.GetConnectionString("DefaultConnection");
            }
            catch
            {
                return null;
            }
        }

        private static string ValidateAndReturnConnectionString(string connectionString)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString)
                {
                    TrustServerCertificate = true
                };

                if (CanConnectToServer(builder))
                {
                    return builder.ConnectionString;
                }
            }
            catch
            {
                // fall through to LocalDB
            }

            return LocalDbConnectionString;
        }

        private static bool CanConnectToServer(SqlConnectionStringBuilder builder)
        {
            // Test server-level connectivity using master DB
            var masterBuilder = new SqlConnectionStringBuilder(builder.ConnectionString)
            {
                InitialCatalog = "master",
                TrustServerCertificate = true
            };

            try
            {
                using var masterConn = new SqlConnection(masterBuilder.ConnectionString);
                masterConn.Open();

                var targetDb = string.IsNullOrEmpty(builder.InitialCatalog) ? "master" : builder.InitialCatalog;
                if (!DatabaseExists(masterConn, targetDb))
                {
                    return false;
                }

                // Try opening the target DB to ensure this login can access it
                using var testConn = new SqlConnection(builder.ConnectionString);
                testConn.Open();
                testConn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool DatabaseExists(SqlConnection masterConn, string databaseName)
        {
            using var cmd = masterConn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(1) FROM sys.databases WHERE name = @db";
            cmd.Parameters.AddWithValue("@db", databaseName);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }
}
