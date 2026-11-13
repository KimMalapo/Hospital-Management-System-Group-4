namespace BusinessLogic;

/// <summary>
/// Raised when input or a business rule fails. The Message is written for the end user,
/// so the UI can show it directly (for example in an ErrorProvider or a message box).
/// </summary>
public class ValidationException : Exception
{
    /// <summary>The field the message is about (for example "Quantity"), or null.</summary>
    public string? Field { get; }

    public ValidationException(string message, string? field = null, Exception? inner = null)
        : base(message, inner)
    {
        Field = field;
    }
}
