using BusinessLogic;
using Model;

namespace HospitalManagementSystem.Tests;

public class ErrorHandlingTests
{
    // Exposes the protected helpers of ServiceBase for testing.
    private class Probe : ServiceBase
    {
        public Probe() : base("Server=(localdb)\\none;Database=none;") { }
        public string Text(string? v, string label, int max) => RequireText(v, label, max);
        public string? Opt(string? v, string label, int max) => OptionalText(v, label, max);
        public decimal Money(decimal v, bool allowZero = false) => RequireMoney(v, "Amount", allowZero);
    }

    [Theory]
    [InlineData(2627)]
    [InlineData(2601)]
    public void Duplicate_errors_use_the_hint(int number)
    {
        var ex = SqlErrorMapper.FromNumber(number, "raw sql text", "A patient with the same name and date of birth already exists.");
        Assert.Equal("A patient with the same name and date of birth already exists.", ex.Message);
    }

    [Fact]
    public void Duplicate_without_hint_has_a_generic_message()
    {
        var ex = SqlErrorMapper.FromNumber(2627, "Violation of UNIQUE KEY constraint");
        Assert.DoesNotContain("UNIQUE KEY", ex.Message);
    }

    [Fact]
    public void Foreign_key_error_is_friendly()
    {
        var ex = SqlErrorMapper.FromNumber(547, "The INSERT statement conflicted with the FOREIGN KEY constraint");
        Assert.DoesNotContain("FOREIGN KEY", ex.Message);
    }

    [Fact]
    public void Procedure_messages_pass_through()
    {
        var ex = SqlErrorMapper.FromNumber(50000, "Room '101' is not available.");
        Assert.Equal("Room '101' is not available.", ex.Message);
    }

    [Fact]
    public void Unknown_errors_never_show_raw_text()
    {
        var ex = SqlErrorMapper.FromNumber(9999, "Msg 9999, Level 16: internal detail");
        Assert.DoesNotContain("internal detail", ex.Message);
    }

    [Fact]
    public void Timeout_and_connection_errors_have_their_own_messages()
    {
        Assert.Contains("too long", SqlErrorMapper.FromNumber(-2, "x").Message);
        Assert.Contains("connect", SqlErrorMapper.FromNumber(53, "x").Message);
    }

    [Fact]
    public void RequireText_rejects_empty_and_too_long()
    {
        var p = new Probe();
        var empty = Assert.Throws<ValidationException>(() => p.Text("  ", "Room", 20));
        Assert.Equal("Room", empty.Field);
        Assert.Throws<ValidationException>(() => p.Text(new string('x', 21), "Room", 20));
        Assert.Equal("101", p.Text(" 101 ", "Room", 20));
    }

    [Fact]
    public void OptionalText_turns_blank_into_null()
    {
        Assert.Null(new Probe().Opt("   ", "Reason", 50));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(10.555)]
    public void RequireMoney_rejects_bad_amounts(double value)
    {
        Assert.Throws<ValidationException>(() => new Probe().Money((decimal)value));
    }

    [Fact]
    public void RequireMoney_accepts_valid_amounts_and_optional_zero()
    {
        var p = new Probe();
        Assert.Equal(1250.50m, p.Money(1250.50m));
        Assert.Equal(0m, p.Money(0m, allowZero: true));
    }

    [Fact]
    public void PatientValidator_accepts_a_valid_patient()
    {
        PatientValidator.Validate(new Patient
        {
            FirstName = "Ana", LastName = "Reyes", DateOfBirth = new DateTime(1995, 4, 2),
            ContactNumber = "+63 912 345 6789", Email = "ana@example.com"
        });
    }

    [Theory]
    [InlineData("", "Reyes", "First name")]
    [InlineData("Ana", " ", "Last name")]
    public void PatientValidator_requires_names(string first, string last, string field)
    {
        var ex = Assert.Throws<ValidationException>(() =>
            PatientValidator.Validate(new Patient { FirstName = first, LastName = last }));
        Assert.Equal(field, ex.Field);
    }

    [Fact]
    public void PatientValidator_rejects_future_birth_date_bad_email_and_bad_phone()
    {
        Assert.Throws<ValidationException>(() => PatientValidator.Validate(
            new Patient { FirstName = "A", LastName = "B", DateOfBirth = DateTime.Today.AddDays(1) }));
        Assert.Throws<ValidationException>(() => PatientValidator.Validate(
            new Patient { FirstName = "A", LastName = "B", Email = "not-an-email" }));
        Assert.Throws<ValidationException>(() => PatientValidator.Validate(
            new Patient { FirstName = "A", LastName = "B", ContactNumber = "abc" }));
    }
}
