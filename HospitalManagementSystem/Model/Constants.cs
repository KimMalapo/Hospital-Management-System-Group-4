namespace Model;

/// <summary>Allowed admission statuses.</summary>
public static class AdmissionStatus
{
    public const string Admitted = "Admitted";
    public const string Discharged = "Discharged";
    public const string Cancelled = "Cancelled";
    public static readonly string[] All = { Admitted, Discharged, Cancelled };
}

/// <summary>Allowed bill statuses.</summary>
public static class BillStatus
{
    public const string Open = "Open";
    public const string PartiallyPaid = "PartiallyPaid";
    public const string Paid = "Paid";
    public const string Voided = "Voided";
}

/// <summary>Ways to pay. Anything but cash needs a reference number.</summary>
public static class PaymentMethod
{
    public const string Cash = "Cash";
    public const string Card = "Card";
    public const string GCash = "GCash";
    public const string BankTransfer = "Bank Transfer";
    public const string Insurance = "Insurance";

    public static readonly string[] All = { Cash, Card, GCash, BankTransfer, Insurance };

    /// <summary>Every method except cash needs a reference number (card slip, transaction id, claim number).</summary>
    public static bool RequiresReference(string method) =>
        !string.Equals(method, Cash, StringComparison.OrdinalIgnoreCase);
}

/// <summary>Groups a service can belong to.</summary>
public static class ServiceCategory
{
    public static readonly string[] All =
        { "Consultation", "Laboratory", "Imaging", "Procedure", "Medication", "Supply", "Other" };
}
