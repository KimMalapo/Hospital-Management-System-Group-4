using System.Data;
using BusinessLogic;
using Model;

namespace HospitalManagementSystem.Tests;

public class ServiceCatalogValidationTests : IDisposable
{
    private readonly ServiceCatalogService _svc = new("Server=(localdb)\\none;Database=none;");

    public ServiceCatalogValidationTests() => CurrentUser.Clear();
    public void Dispose() => CurrentUser.Clear();

    private static ServiceItem Good() => new()
    {
        Code = "cbc", Name = "Complete Blood Count", Category = "laboratory", UnitPrice = 350m
    };

    [Fact]
    public void Valid_service_is_cleaned_up()
    {
        var clean = ServiceCatalogService.Validate(Good());
        Assert.Equal("CBC", clean.Code);
        Assert.Equal("Laboratory", clean.Category);
    }

    [Theory]
    [InlineData("", "Code")]
    [InlineData("has space", "Code")]
    [InlineData("this-code-is-way-too-long-for-the-limit", "Code")]
    public void Bad_codes_are_rejected(string code, string field)
    {
        var s = Good(); s.Code = code;
        var ex = Assert.Throws<ValidationException>(() => ServiceCatalogService.Validate(s));
        Assert.Equal(field, ex.Field);
    }

    [Fact]
    public void Name_category_and_price_are_checked()
    {
        var s = Good(); s.Name = " ";
        Assert.Equal("Name", Assert.Throws<ValidationException>(() => ServiceCatalogService.Validate(s)).Field);

        s = Good(); s.Category = "Pizza";
        Assert.Equal("Category", Assert.Throws<ValidationException>(() => ServiceCatalogService.Validate(s)).Field);

        s = Good(); s.UnitPrice = -1m;
        Assert.Equal("Unit price", Assert.Throws<ValidationException>(() => ServiceCatalogService.Validate(s)).Field);

        s = Good(); s.UnitPrice = 10.123m;
        Assert.Throws<ValidationException>(() => ServiceCatalogService.Validate(s));
    }

    [Fact]
    public void Staff_cannot_change_the_price_list()
    {
        CurrentUser.Set("staff@hms.test", "Staff");
        Assert.Throws<UnauthorizedAccessException>(() => _svc.Create(Good()));
        Assert.Throws<UnauthorizedAccessException>(() => _svc.Deactivate(3));
    }

    [Fact]
    public void Update_and_deactivate_need_a_selected_service()
    {
        Assert.Throws<ValidationException>(() => _svc.Update(Good()));   // ServiceID is 0
        Assert.Throws<ValidationException>(() => _svc.Deactivate(0));
    }
}

public class ChargeValidationTests : IDisposable
{
    private readonly ChargeService _svc = new("Server=(localdb)\\none;Database=none;");

    public ChargeValidationTests() => CurrentUser.Clear();
    public void Dispose() => CurrentUser.Clear();

    [Theory]
    [InlineData(0, 1, 1, "Admission")]
    [InlineData(1, 0, 1, "Service")]
    [InlineData(1, 1, 0, "Quantity")]
    [InlineData(1, 1, -2, "Quantity")]
    [InlineData(1, 1, 1000, "Quantity")]
    public void Add_rejects_bad_input(int admission, int service, int qty, string field)
    {
        var ex = Assert.Throws<ValidationException>(() => _svc.Add(admission, service, qty));
        Assert.Equal(field, ex.Field);
    }

    [Fact]
    public void Add_needs_a_logged_in_user()
    {
        Assert.Throws<UnauthorizedAccessException>(() => _svc.Add(1, 1, 2));
    }

    [Fact]
    public void Void_needs_a_reason()
    {
        Assert.Throws<ValidationException>(() => _svc.Void(1, ""));
        Assert.Throws<ValidationException>(() => _svc.Void(1, "ab"));
        Assert.Throws<ValidationException>(() => _svc.Void(0, "Entered by mistake"));
    }

    [Fact]
    public void Staff_cannot_void_a_charge()
    {
        CurrentUser.Set("staff@hms.test", "Staff");
        var ex = Assert.Throws<UnauthorizedAccessException>(() => _svc.Void(1, "Entered by mistake"));
        Assert.Contains("Admin", ex.Message);
    }
}
