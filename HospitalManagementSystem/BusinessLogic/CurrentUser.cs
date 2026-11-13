namespace BusinessLogic;

/// <summary>
/// The logged-in user, visible to every service. The UI sets it at login (SessionManager does this)
/// and clears it at logout. Services use it for CreatedBy / ChargedBy / ReceivedBy and permission checks.
/// </summary>
public static class CurrentUser
{
    private static readonly object Sync = new();
    private static string? _email;
    private static string? _role;

    public static void Set(string email, string role)
    {
        lock (Sync) { _email = email; _role = role; }
    }

    public static void Clear()
    {
        lock (Sync) { _email = null; _role = null; }
    }

    public static string? Email { get { lock (Sync) return _email; } }

    public static string? Role { get { lock (Sync) return _role; } }

    public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(Email);

    public static bool IsAdmin =>
        string.Equals(Role, "Admin", StringComparison.OrdinalIgnoreCase);

    /// <summary>The value to store in CreatedBy-style columns.</summary>
    public static string EmailOrSystem => Email ?? "system";
}
