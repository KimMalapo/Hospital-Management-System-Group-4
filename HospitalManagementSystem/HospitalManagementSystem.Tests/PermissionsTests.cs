using BusinessLogic;

namespace HospitalManagementSystem.Tests;

// CurrentUser is static, so these tests reset it before and after each test.
public class PermissionsTests : IDisposable
{
    public PermissionsTests() => CurrentUser.Clear();
    public void Dispose() => CurrentUser.Clear();

    [Fact]
    public void Nobody_logged_in_is_refused()
    {
        var ex = Assert.Throws<UnauthorizedAccessException>(() => Permissions.RequireLogin());
        Assert.Contains("log in", ex.Message);
        Assert.False(Permissions.IsAllowed(AppAction.ViewReports));
    }

    [Theory]
    [InlineData(AppAction.VoidCharge)]
    [InlineData(AppAction.VoidPayment)]
    [InlineData(AppAction.VoidBill)]
    [InlineData(AppAction.ManageServices)]
    [InlineData(AppAction.ManageMasterData)]
    [InlineData(AppAction.ManageUsers)]
    [InlineData(AppAction.ViewReports)]
    public void Staff_cannot_do_admin_only_actions(AppAction action)
    {
        CurrentUser.Set("staff@hms.test", "Staff");
        Assert.False(Permissions.IsAllowed(action));
        var ex = Assert.Throws<UnauthorizedAccessException>(() => Permissions.Require(action));
        Assert.Contains("Admin", ex.Message);
    }

    [Theory]
    [InlineData(AppAction.VoidCharge)]
    [InlineData(AppAction.VoidPayment)]
    [InlineData(AppAction.ManageServices)]
    [InlineData(AppAction.ViewReports)]
    public void Admin_can_do_everything(AppAction action)
    {
        CurrentUser.Set("admin@hms.test", "Admin");
        Assert.True(Permissions.IsAllowed(action));
        Permissions.Require(action);
    }

    [Fact]
    public void Role_check_ignores_letter_case()
    {
        CurrentUser.Set("admin@hms.test", "admin");
        Assert.True(CurrentUser.IsAdmin);
    }

    [Fact]
    public void Staff_can_log_in_and_do_everyday_work()
    {
        CurrentUser.Set("staff@hms.test", "Staff");
        Permissions.RequireLogin();
        Assert.Equal("staff@hms.test", CurrentUser.EmailOrSystem);
    }

    [Fact]
    public void Clear_logs_out()
    {
        CurrentUser.Set("a@b.c", "Admin");
        CurrentUser.Clear();
        Assert.False(CurrentUser.IsAuthenticated);
        Assert.Equal("system", CurrentUser.EmailOrSystem);
    }
}

public class AuthServiceTests : IDisposable
{
    private readonly string? _saved = Environment.GetEnvironmentVariable(AuthService.SecretVariable);

    public void Dispose() => Environment.SetEnvironmentVariable(AuthService.SecretVariable, _saved);

    [Fact]
    public void Missing_secret_gives_a_clear_message()
    {
        Environment.SetEnvironmentVariable(AuthService.SecretVariable, null);
        Assert.False(AuthService.IsConfigured);
        var ex = Assert.Throws<InvalidOperationException>(() => new AuthService());
        Assert.Contains("HMS_JWT_SECRET", ex.Message);
    }

    [Fact]
    public void Short_secret_is_refused()
    {
        Environment.SetEnvironmentVariable(AuthService.SecretVariable, "short");
        Assert.False(AuthService.IsConfigured);
    }

    [Fact]
    public void Token_round_trip_keeps_email_and_role()
    {
        Environment.SetEnvironmentVariable(AuthService.SecretVariable, "a-test-secret-with-more-than-32-characters");
        var auth = new AuthService();
        var token = auth.CreateToken("admin@hms.test", "Admin");
        var principal = auth.ValidateToken(token, out var expired);
        Assert.False(expired);
        Assert.NotNull(principal);
        Assert.Equal("admin@hms.test", principal!.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value);
        Assert.Equal("Admin", principal.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value);
    }

    [Fact]
    public void Tampered_token_is_rejected()
    {
        Environment.SetEnvironmentVariable(AuthService.SecretVariable, "a-test-secret-with-more-than-32-characters");
        var auth = new AuthService();
        var token = auth.CreateToken("a@b.c", "Staff");
        Assert.Null(auth.ValidateToken(token + "x"));
    }
}
