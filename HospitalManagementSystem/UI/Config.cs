using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace UI
{
    public static class Config
    {
        // Returns a usable connection string, preferring environment variable, then appsettings.json.
        public static string? GetConnectionString()
        {
            var env = Environment.GetEnvironmentVariable("HMS_CONNECTION_STRING");
            if (!string.IsNullOrEmpty(env))
            {
                try
                {
                    var b = new SqlConnectionStringBuilder(env)
                    {
                        TrustServerCertificate = true
                    };
                    return b.ConnectionString;
                }
                catch
                {
                    return env;
                }
            }

            try
            {
                var basePath = AppContext.BaseDirectory;
                var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables();
                var config = configBuilder.Build();
                var cs = config.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrEmpty(cs))
                {
                    try
                    {
                        var b = new SqlConnectionStringBuilder(cs)
                        {
                            TrustServerCertificate = true
                        };

                        // Test server-level connectivity using master
                        try
                        {
                            var masterBuilder = new SqlConnectionStringBuilder(b.ConnectionString)
                            {
                                InitialCatalog = "master",
                                TrustServerCertificate = true
                            };
                            using var masterConn = new SqlConnection(masterBuilder.ConnectionString);
                            masterConn.Open();
                            // Check if target DB exists
                            using var cmd = masterConn.CreateCommand();
                            var targetDb = string.IsNullOrEmpty(b.InitialCatalog) ? "master" : b.InitialCatalog;
                            cmd.CommandText = "SELECT COUNT(1) FROM sys.databases WHERE name = @db";
                            cmd.Parameters.AddWithValue("@db", targetDb);
                            var dbExists = Convert.ToInt32(cmd.ExecuteScalar() ?? 0) > 0;
                            if (!dbExists)
                            {
                                return null;
                            }

                            // Try opening the configured DB
                            using var testConn = new SqlConnection(b.ConnectionString);
                            testConn.Open();
                            testConn.Close();
                            return b.ConnectionString;
                        }
                        catch
                        {
                            return null;
                        }
                    }
                    catch
                    {
                        return cs;
                    }
                }
            }
            catch
            {
                // ignore
            }

            return null;
        }
    }
}
