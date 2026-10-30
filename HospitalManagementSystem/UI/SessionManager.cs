using System.Security.Claims;
using BusinessLogic;

namespace UI
{
    public static class SessionManager
    {
        private static string? _currentToken;
        private static ClaimsPrincipal? _currentUser;

        public static void SetToken(string token)
        {
            _currentToken = token;
            var auth = new AuthService();          // now works
            _currentUser = auth.ValidateToken(token);
        }

        public static void Logout()
        {
            _currentToken = null;
            _currentUser = null;
        }

        public static bool IsAuthenticated
        {
            get
            {
                if (string.IsNullOrEmpty(_currentToken)) return false;
                var auth = new AuthService();
                _currentUser = auth.ValidateToken(_currentToken);
                return _currentUser != null;
            }
        }

        public static string? CurrentEmail =>
            _currentUser?.FindFirst(ClaimTypes.Email)?.Value;

        public static string? CurrentRole =>
            _currentUser?.FindFirst(ClaimTypes.Role)?.Value;

        public static bool IsAdmin =>
            string.Equals(CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase);
    }
}