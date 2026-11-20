using BusinessLogic;

namespace HospitalManagementSystem.Tests;

public class PasswordHelperTests
{
    [Fact]
    public void Hash_then_Verify_with_the_same_password_succeeds()
    {
        var hash = PasswordHelper.Hash("Secret123");
        Assert.True(PasswordHelper.Verify("Secret123", hash));
    }

    [Fact]
    public void Verify_with_a_wrong_password_fails()
    {
        var hash = PasswordHelper.Hash("Secret123");
        Assert.False(PasswordHelper.Verify("secret123", hash));
        Assert.False(PasswordHelper.Verify("Secret124", hash));
    }

    [Fact]
    public void Hash_is_not_the_plain_password_and_is_salted()
    {
        var a = PasswordHelper.Hash("Secret123");
        var b = PasswordHelper.Hash("Secret123");
        Assert.NotEqual("Secret123", a);
        Assert.NotEqual(a, b);                       // different salt each time
        Assert.True(PasswordHelper.Verify("Secret123", b));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Hash_rejects_empty_passwords(string? password)
    {
        Assert.Throws<ArgumentException>(() => PasswordHelper.Hash(password!));
    }

    [Theory]
    [InlineData(null, "somehash")]
    [InlineData("", "somehash")]
    [InlineData("Secret123", null)]
    [InlineData("Secret123", "")]
    public void Verify_returns_false_for_missing_input(string? password, string? hash)
    {
        Assert.False(PasswordHelper.Verify(password!, hash!));
    }

    [Fact]
    public void Verify_returns_false_for_a_hash_that_is_not_bcrypt()
    {
        Assert.False(PasswordHelper.Verify("Secret123", "this-is-not-a-bcrypt-hash"));
    }

    [Fact]
    public void Old_method_names_still_work()
    {
        var hash = PasswordHelper.HashPassword("Secret123");
        Assert.True(PasswordHelper.Verify("Secret123", hash));
    }

    [Theory]
    [InlineData("Abcdef1!", true)]
    [InlineData("LongPassword9!", true)]
    [InlineData("Short1A", false)]          // under 8 characters
    [InlineData("alllowercase1", false)]    // no uppercase
    [InlineData("ALLUPPERCASE1", false)]    // no lowercase
    [InlineData("NoDigitsHere", false)]     // no digit
    [InlineData("Password123", false)]      // no special character
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsStrongPassword_applies_the_rules(string? password, bool expected)
    {
        Assert.Equal(expected, PasswordHelper.IsStrongPassword(password!));
    }
}
