namespace BusinessLogic;

/// <summary>Actions that need a role check. Staff can do everything that is not listed as Admin only.</summary>
public enum AppAction
{
    VoidCharge,
    VoidPayment,
    VoidBill,
    ManageServices,
    ManageMasterData,
    ManageUsers,
    ViewReports
}

/// <summary>
/// The Admin / Staff permission matrix (see docs/ROLES.md). Services call Require before changing data,
/// and the UI calls IsAllowed to hide or disable buttons so both agree.
/// </summary>
public static class Permissions
{
    private static readonly HashSet<AppAction> AdminOnly = new()
    {
        AppAction.VoidCharge,
        AppAction.VoidPayment,
        AppAction.VoidBill,
        AppAction.ManageServices,
        AppAction.ManageMasterData,
        AppAction.ManageUsers,
        AppAction.ViewReports
    };

    public static bool IsAllowed(AppAction action) =>
        CurrentUser.IsAuthenticated && (CurrentUser.IsAdmin || !AdminOnly.Contains(action));

    /// <summary>Any logged-in user passes. Use for everyday actions (admit, add charge, post payment).</summary>
    public static void RequireLogin()
    {
        if (!CurrentUser.IsAuthenticated)
            throw new UnauthorizedAccessException("Please log in first.");
    }

    /// <summary>Throws UnauthorizedAccessException with a friendly message when the user may not do the action.</summary>
    public static void Require(AppAction action)
    {
        RequireLogin();
        if (!IsAllowed(action))
            throw new UnauthorizedAccessException(
                $"Only an Admin can {Describe(action)}. Ask an Admin to do this for you.");
    }

    public static string Describe(AppAction action) => action switch
    {
        AppAction.VoidCharge => "void a charge",
        AppAction.VoidPayment => "void a payment",
        AppAction.VoidBill => "void a bill",
        AppAction.ManageServices => "change the service price list",
        AppAction.ManageMasterData => "change master data",
        AppAction.ManageUsers => "manage user accounts",
        AppAction.ViewReports => "open reports",
        _ => "do this"
    };
}
