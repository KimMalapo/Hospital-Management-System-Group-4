using BusinessLogic;

namespace HospitalManagementSystem.Tests;

// The database stores UTC; the app shows and accepts local time. These tests hold for any computer time zone.
public class TimeConversionTests
{
    [Fact]
    public void Local_time_is_sent_to_the_database_as_utc()
    {
        var local = new DateTime(2026, 11, 24, 9, 0, 0, DateTimeKind.Local);

        var utc = TimeConversion.ToUtc(local);

        Assert.Equal(DateTimeKind.Utc, utc.Kind);
        Assert.Equal(local.ToUniversalTime(), utc);
    }

    [Fact]
    public void Picker_value_without_a_kind_is_treated_as_local_time()
    {
        // DateTimePicker.Value has Kind Unspecified.
        var picked = new DateTime(2026, 11, 24, 9, 0, 0, DateTimeKind.Unspecified);

        var utc = TimeConversion.ToUtc(picked);

        Assert.Equal(TimeZoneInfo.ConvertTimeToUtc(picked, TimeZoneInfo.Local), utc);
    }

    [Fact]
    public void A_value_that_is_already_utc_is_not_shifted_again()
    {
        var alreadyUtc = new DateTime(2026, 11, 24, 1, 0, 0, DateTimeKind.Utc);

        Assert.Equal(alreadyUtc, TimeConversion.ToUtc(alreadyUtc));
    }

    [Fact]
    public void A_missing_date_stays_missing()
    {
        Assert.Null(TimeConversion.ToUtc((DateTime?)null));
    }

    [Fact]
    public void Utc_value_from_the_database_is_shown_as_local_time()
    {
        // The SQL driver returns Kind Unspecified for datetime2.
        var fromDatabase = new DateTime(2026, 11, 24, 1, 0, 0, DateTimeKind.Unspecified);

        var shown = TimeConversion.ToLocal(fromDatabase);

        Assert.Equal(DateTimeKind.Local, shown.Kind);
        Assert.Equal(TimeZoneInfo.ConvertTimeFromUtc(fromDatabase, TimeZoneInfo.Local), shown);
    }

    [Fact]
    public void Saving_and_reading_back_gives_the_same_moment()
    {
        var original = new DateTime(2026, 11, 24, 1, 0, 0, DateTimeKind.Utc);

        var roundTrip = TimeConversion.ToUtc(TimeConversion.ToLocal(original));

        Assert.Equal(original, roundTrip);
    }

    [Fact]
    public void An_admission_typed_as_now_is_not_in_the_future_for_the_database()
    {
        // Before the fix, local time (UTC+8 in Manila) was stored as if it were UTC, 8 hours ahead of the database clock.
        var typedNow = DateTime.Now;

        var stored = TimeConversion.ToUtc(typedNow);

        Assert.True(stored <= DateTime.UtcNow.AddSeconds(5));
    }
}
