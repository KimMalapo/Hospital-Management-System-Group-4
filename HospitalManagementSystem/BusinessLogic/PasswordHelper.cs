using System;

namespace BusinessLogic
{
    public static class PasswordHelper
    {
        public static string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Old name the UI is calling
        public static string HashPassword(string password) => Hash(password);

        public static bool Verify(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;
            try { return BCrypt.Net.BCrypt.Verify(password, hash); }
            catch { return false; }
        }

        // Old name the UI is calling
        public static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;
            bool hasUpper = false, hasLower = false, hasDigit = false;
            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                if (char.IsLower(c)) hasLower = true;
                if (char.IsDigit(c)) hasDigit = true;
            }
            return hasUpper && hasLower && hasDigit;
        }
    }
}