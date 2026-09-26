using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UI
{
    public static class PasswordHelper
    {
        public static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            if (password.Length < 8) return false;
            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(c => !char.IsLetterOrDigit(c));
            return hasUpper && hasLower && hasDigit && hasSpecial;
        }

        public static string GenerateStrongPassword(int length = 12)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()-_+=<>?";
            var all = upper + lower + digits + special;
            var rnd = RandomNumberGenerator.Create();
            var bytes = new byte[length];
            rnd.GetBytes(bytes);
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = all[bytes[i] % all.Length];
            }
            // ensure each category present
            var pwd = new string(chars).ToList();
            pwd[0] = upper[bytes[0] % upper.Length];
            pwd[1] = lower[bytes[1] % lower.Length];
            pwd[2] = digits[bytes[2] % digits.Length];
            pwd[3] = special[bytes[3] % special.Length];
            return new string(pwd.ToArray());
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
