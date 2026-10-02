using System;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace UI
{
    public static class Config
    {
        // Returns a usable connection string, preferring (in order):
        // 1. HMS_CONNECTION_STRING env var
        // 2. HMS_DB_FILE env var or appsettings.json DatabaseFile (auto-build LocalDB attach string)
        // 3. DefaultConnection from appsettings.json (tested)
        public static string? GetConnectionString()
        {
            // 1) direct connection string from env
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

            // 2) explicit DB file (LocalDB .mdf) from env or appsettings
            var dbFileEnv = Environment.GetEnvironmentVariable("HMS_DB_FILE");
            try
            {
                var basePath = AppContext.BaseDirectory;
                var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables();
                var config = configBuilder.Build();

                var dbFileFromSettings = config["DatabaseFile"] ?? config["Database:File"];
                var dbFile = !string.IsNullOrWhiteSpace(dbFileEnv) ? dbFileEnv : dbFileFromSettings;

                if (!string.IsNullOrWhiteSpace(dbFile))
                {
                    // make path absolute relative to app base if needed
                    if (!Path.IsPathRooted(dbFile))
                    {
                        dbFile = Path.Combine(basePath, dbFile);
                    }

                    if (File.Exists(dbFile))
                    {
                        try
                        {
                            var b = new SqlConnectionStringBuilder
                            {
                                DataSource = @"(LocalDB)\\MSSQLLocalDB",
                                AttachDBFilename = dbFile,
                                IntegratedSecurity = true,
                                ConnectTimeout = 30,
                                TrustServerCertificate = true
                            };
                            return b.ConnectionString;
                        }
                        catch
                        {
                            // fall through to default resolution
                        }
                    }
                }

                // 3) fallback to DefaultConnection from appsettings.json
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
