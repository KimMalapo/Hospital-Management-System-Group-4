using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic
{
    // REPLACES the existing BusinessLogic/AuthService.cs.
    // Changes: clear start-up message when HMS_JWT_SECRET is missing/short, IsConfigured check for Program.cs,
    // and ValidateToken(token, out expired) so the UI can tell "session expired" from "invalid token".
    public class AuthService
    {
        public const string SecretVariable = "HMS_JWT_SECRET";
        private const int MinSecretLength = 16;

        private readonly string _secret;

        /// <summary>Message to show the user when the secret is missing.</summary>
        public static string ConfigurationMessage =>
            $"The app is not configured. Set the environment variable {SecretVariable} to a secret of at least " +
            $"{MinSecretLength} characters (32 recommended), then restart the app. See docs/DATABASE_SETUP.md.";

        /// <summary>Call from Program.cs before showing the login form.</summary>
        public static bool IsConfigured
        {
            get
            {
                var s = Environment.GetEnvironmentVariable(SecretVariable);
                return !string.IsNullOrWhiteSpace(s) && s.Length >= MinSecretLength;
            }
        }

        public AuthService()
        {
            var secret = Environment.GetEnvironmentVariable(SecretVariable);
            if (string.IsNullOrWhiteSpace(secret) || secret.Length < MinSecretLength)
                throw new InvalidOperationException(ConfigurationMessage);
            _secret = secret;
        }

        public string CreateToken(string email, string role, int expireMinutes = 60)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token) => ValidateToken(token, out _);

        public ClaimsPrincipal? ValidateToken(string token, out bool expired)
        {
            expired = false;
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
                var handler = new JwtSecurityTokenHandler();

                return handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                }, out _);
            }
            catch (SecurityTokenExpiredException)
            {
                expired = true;
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error("AuthService.ValidateToken: token rejected", ex);
                return null;
            }
        }
    }
}
