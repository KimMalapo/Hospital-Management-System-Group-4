namespace BusinessLogic;

/// <summary>
/// The database stores every timestamp in UTC (SYSUTCDATETIME). The app works in the user's local time.
/// These two methods are the only place the two are converted, so a stay measured by the database
/// matches what the user typed and what the screens show.
/// </summary>
public static class TimeConversion
{
    /// <summary>
    /// Local time to UTC, for values sent to the database.
    /// A value with Kind Utc is returned as is; Local and Unspecified (what DateTimePicker gives) are treated as local.
    /// </summary>
    public static DateTime ToUtc(DateTime value) =>
        value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();

    public static DateTime? ToUtc(DateTime? value) =>
        value.HasValue ? ToUtc(value.Value) : null;

    /// <summary>
    /// UTC to local time, for values read from the database.
    /// The database driver returns Kind Unspecified, which is always UTC here.
    /// </summary>
    public static DateTime ToLocal(DateTime utcValue) =>
        DateTime.SpecifyKind(utcValue, DateTimeKind.Utc).ToLocalTime();
}
