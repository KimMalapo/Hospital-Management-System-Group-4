using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UI
{
    public static class AuthService
    {
        private static string? _token;

        public static void SetToken(string token)
        {
            _token = token;
        }

        public static string? GetToken()
        {
            return _token;
        }

        public static bool IsAuthenticated()
        {
            if (string.IsNullOrEmpty(_token)) return false;
            try
            {
                var secret = Environment.GetEnvironmentVariable("HMS_JWT_SECRET") ?? "ReplaceThisWithASecureSecretInEnv";
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(secret);
                tokenHandler.ValidateToken(_token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromMinutes(5)
                }, out var validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
