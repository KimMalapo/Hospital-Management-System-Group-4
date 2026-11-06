using System;
using BusinessLogic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace UI
{
    /// <summary>
    /// The single place the app reads configuration from.
    /// Order of precedence for the connection string:
    ///   1. HMS_CONNECTION_STRING environment variable
    ///   2. appsettings.Local.json  (per-machine, git-ignored)
    ///   3. appsettings.json        (shared defaults, LocalDB)
    /// </summary>
    public static class Config
    {
        private static IConfiguration? _config;

        private static IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    _config = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
                }
                return _config;
            }
        }

        public static string GetConnectionString()
        {
            var cs = Environment.GetEnvironmentVariable("HMS_CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(cs))
                cs = Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(cs))
            {
                var ex = new InvalidOperationException(
                    "Connection string not found. Set HMS_CONNECTION_STRING, or DefaultConnection in appsettings.json / appsettings.Local.json");
                Logger.Error("Config.GetConnectionString", ex);
                throw ex;
            }

            return WithTrustedCertificate(cs);
        }

        // Microsoft.Data.SqlClient encrypts by default, which fails against LocalDB/dev servers
        // that only have a self-signed certificate.
        private static string WithTrustedCertificate(string connectionString)
        {
            try
            {
                return new SqlConnectionStringBuilder(connectionString) { TrustServerCertificate = true }.ConnectionString;
            }
            catch (Exception ex)
            {
                Logger.Error("Config: connection string could not be parsed, using it as given", ex);
                return connectionString;
            }
        }

        public static string GetJwtSecret()
        {
            var secret = Environment.GetEnvironmentVariable("HMS_JWT_SECRET");
            if (string.IsNullOrWhiteSpace(secret))
                throw new InvalidOperationException("HMS_JWT_SECRET environment variable is not set");
            return secret;
        }
    }
}
