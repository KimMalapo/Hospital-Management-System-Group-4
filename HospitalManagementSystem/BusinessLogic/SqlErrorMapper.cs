using Microsoft.Data.SqlClient;

namespace BusinessLogic;

/// <summary>
/// Turns SQL Server errors into plain messages for the user. Technical details are never shown;
/// callers log the original exception with Logger.Error.
/// </summary>
public static class SqlErrorMapper
{
    public static ValidationException Map(SqlException ex, string? duplicateHint = null) =>
        FromNumber(ex.Number, ex.Message, duplicateHint, ex);

    /// <summary>Separate from Map so it can be unit tested (SqlException cannot be constructed in tests).</summary>
    public static ValidationException FromNumber(int number, string message, string? duplicateHint = null, Exception? inner = null)
    {
        // RAISERROR / THROW with a custom message from our stored procedures: the text is already user-friendly.
        if (number >= 50000)
            return new ValidationException(message, null, inner);

        switch (number)
        {
            case 2627:   // unique constraint
            case 2601:   // unique index
                return new ValidationException(duplicateHint ?? "That record already exists.", null, inner);

            case 547:    // foreign key / check constraint
                return new ValidationException(
                    "This action conflicts with related records. The record may be in use, or a selected item no longer exists.", null, inner);

            case 1205:   // deadlock victim
                return new ValidationException("The system was busy with another change. Please try again.", null, inner);

            case -2:     // timeout
                return new ValidationException("The database took too long to respond. Please try again.", null, inner);

            case 53:
            case 40:
            case -1:
            case 4060:
            case 18456:
                return new ValidationException(
                    "Cannot connect to the database. Check that SQL Server LocalDB is running and the connection string is correct.", null, inner);

            default:
                return new ValidationException("Something went wrong while saving. The details were written to the error log.", null, inner);
        }
    }
}
