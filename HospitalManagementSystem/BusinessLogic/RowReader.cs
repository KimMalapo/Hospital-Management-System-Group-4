using System.Data;

namespace BusinessLogic;

/// <summary>
/// Null-safe reads from a DataRow. A missing column returns a default value, so a stored procedure
/// can add or omit columns without crashing the mapping code.
/// </summary>
public static class RowReader
{
    private static bool Has(DataRow r, string c) => r.Table.Columns.Contains(c) && r[c] != DBNull.Value;

    public static string? Str(DataRow r, string c) => Has(r, c) ? Convert.ToString(r[c]) : null;

    public static int Int(DataRow r, string c) => Has(r, c) ? Convert.ToInt32(r[c]) : 0;

    public static int? IntOrNull(DataRow r, string c) => Has(r, c) ? Convert.ToInt32(r[c]) : null;

    public static decimal Dec(DataRow r, string c) => Has(r, c) ? Convert.ToDecimal(r[c]) : 0m;

    public static DateTime? Date(DataRow r, string c) => Has(r, c) ? Convert.ToDateTime(r[c]) : null;

    public static bool Bool(DataRow r, string c) => Has(r, c) && Convert.ToBoolean(r[c]);
}
