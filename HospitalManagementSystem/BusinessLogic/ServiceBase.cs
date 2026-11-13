using Microsoft.Data.SqlClient;

namespace BusinessLogic;

/// <summary>
/// Shared plumbing for the new services: one DbExecutor, SQL error translation, and small validation helpers.
/// Every public service method should validate first, then call Run so database errors reach the user as
/// friendly messages and are written to the log.
/// </summary>
public abstract class ServiceBase
{
    protected readonly string ConnectionString;
    protected readonly DbExecutor Db;

    protected ServiceBase(string connectionString)
    {
        ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        Db = new DbExecutor(connectionString);
    }

    /// <summary>Runs a database operation. SqlException becomes a ValidationException with a friendly message.</summary>
    protected T Run<T>(string operation, Func<T> action, string? duplicateHint = null)
    {
        try
        {
            return action();
        }
        catch (ValidationException) { throw; }
        catch (UnauthorizedAccessException) { throw; }
        catch (SqlException ex)
        {
            Logger.Error(operation, ex);
            throw SqlErrorMapper.Map(ex, duplicateHint);
        }
    }

    protected void RunVoid(string operation, Action action, string? duplicateHint = null) =>
        Run<bool>(operation, () => { action(); return true; }, duplicateHint);

    protected static SqlParameter P(string name, object? value) =>
        new SqlParameter(name, value ?? DBNull.Value);

    /// <summary>Required text: trimmed, not empty, not longer than maxLength.</summary>
    protected static string RequireText(string? value, string label, int maxLength)
    {
        var v = value?.Trim();
        if (string.IsNullOrEmpty(v))
            throw new ValidationException($"{label} is required.", label);
        if (v.Length > maxLength)
            throw new ValidationException($"{label} must be {maxLength} characters or fewer.", label);
        return v;
    }

    /// <summary>Optional text: trimmed; empty becomes null; not longer than maxLength.</summary>
    protected static string? OptionalText(string? value, string label, int maxLength)
    {
        var v = value?.Trim();
        if (string.IsNullOrEmpty(v)) return null;
        if (v.Length > maxLength)
            throw new ValidationException($"{label} must be {maxLength} characters or fewer.", label);
        return v;
    }

    /// <summary>Money amounts: must be above zero (or zero allowed), no more than 2 decimals, below a sane limit.</summary>
    protected static decimal RequireMoney(decimal amount, string label, bool allowZero = false, decimal max = 100_000_000m)
    {
        if (amount < 0 || (!allowZero && amount == 0))
            throw new ValidationException(
                allowZero ? $"{label} cannot be negative." : $"{label} must be greater than zero.", label);
        if (decimal.Round(amount, 2) != amount)
            throw new ValidationException($"{label} can have at most 2 decimal places.", label);
        if (amount > max)
            throw new ValidationException($"{label} is too large. The maximum is {max:N2}.", label);
        return amount;
    }
}
