using System.Text.RegularExpressions;
using Model;

namespace BusinessLogic;

/// <summary>
/// Input rules for patient records, shared by PatientService.Create and Update
/// (required fields, invalid input). Duplicate detection is done by the database.
/// </summary>
public static class PatientValidator
{
    private static readonly Regex EmailPattern =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    private static readonly Regex PhonePattern =
        new(@"^[0-9+\-\s()]{7,30}$", RegexOptions.Compiled);

    public static void Validate(Patient p)
    {
        if (p == null) throw new ValidationException("Patient details are required.");

        var first = p.FirstName?.Trim();
        var last = p.LastName?.Trim();

        if (string.IsNullOrEmpty(first))
            throw new ValidationException("First name is required.", "First name");
        if (string.IsNullOrEmpty(last))
            throw new ValidationException("Last name is required.", "Last name");
        if (first.Length > 100)
            throw new ValidationException("First name must be 100 characters or fewer.", "First name");
        if (last.Length > 100)
            throw new ValidationException("Last name must be 100 characters or fewer.", "Last name");

        if (p.DateOfBirth.HasValue)
        {
            if (p.DateOfBirth.Value.Date > DateTime.Today)
                throw new ValidationException("Date of birth cannot be in the future.", "Date of birth");
            if (p.DateOfBirth.Value.Year < 1900)
                throw new ValidationException("Date of birth is not valid.", "Date of birth");
        }

        if (!string.IsNullOrWhiteSpace(p.Gender) && p.Gender.Trim().Length > 20)
            throw new ValidationException("Gender must be 20 characters or fewer.", "Gender");

        if (!string.IsNullOrWhiteSpace(p.ContactNumber) && !PhonePattern.IsMatch(p.ContactNumber.Trim()))
            throw new ValidationException("Contact number can only contain digits, spaces, + - ( ) and must be 7 to 30 characters.", "Contact number");

        if (!string.IsNullOrWhiteSpace(p.Email))
        {
            var e = p.Email.Trim();
            if (e.Length > 200 || !EmailPattern.IsMatch(e))
                throw new ValidationException("Email address is not valid.", "Email");
        }

        if (!string.IsNullOrWhiteSpace(p.Address) && p.Address.Trim().Length > 400)
            throw new ValidationException("Address must be 400 characters or fewer.", "Address");
    }
}
