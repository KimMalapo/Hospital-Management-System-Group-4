using System;
using Microsoft.Extensions.Configuration;

namespace UI
{
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
                        .AddEnvironmentVariables()
                        .Build();
                }
                return _config;
            }
        }

        public static string GetConnectionString()
        {
            var env = Environment.GetEnvironmentVariable("HMS_CONNECTION_STRING");
            if (!string.IsNullOrWhiteSpace(env))
                return env;

            var cs = Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException(
                    "Connection string not found. Set HMS_CONNECTION_STRING or DefaultConnection in appsettings.json");

            return cs;
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