using System.Security.Claims;
using BusinessLogic;

namespace UI
{
    // REPLACES the existing UI/SessionManager.cs.
    // Changes: keeps BusinessLogic.CurrentUser in sync (so services know who is logged in),
    // clears everything on Logout, and raises SessionExpired when the token has expired.
    public static class SessionManager
    {
        private static string? _currentToken;
        private static ClaimsPrincipal? _currentUser;

        /// <summary>Raised when IsAuthenticated finds an expired token. The landing page should show
        /// "Your session expired" and return to LoginForm.</summary>
        public static event Action? SessionExpired;

        public static void SetToken(string token)
        {
            _currentToken = token;
            _currentUser = new AuthService().ValidateToken(token);
            SyncCurrentUser();
        }

        public static void Logout()
        {
            _currentToken = null;
            _currentUser = null;
            CurrentUser.Clear();
        }

        public static bool IsAuthenticated
        {
            get
            {
                if (string.IsNullOrEmpty(_currentToken)) return false;

                _currentUser = new AuthService().ValidateToken(_currentToken, out var expired);
                if (_currentUser == null)
                {
                    Logout();
                    if (expired) SessionExpired?.Invoke();
                    return false;
                }

                SyncCurrentUser();
                return true;
            }
        }

        public static string? CurrentEmail =>
            _currentUser?.FindFirst(ClaimTypes.Email)?.Value;

        public static string? CurrentRole =>
            _currentUser?.FindFirst(ClaimTypes.Role)?.Value;

        public static bool IsAdmin =>
            string.Equals(CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase);

        private static void SyncCurrentUser()
        {
            if (CurrentEmail != null && CurrentRole != null)
                CurrentUser.Set(CurrentEmail, CurrentRole);
            else
                CurrentUser.Clear();
        }
    }
}
