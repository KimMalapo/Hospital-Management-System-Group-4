using System.Data;
using System.Text.RegularExpressions;
using Model;

namespace BusinessLogic;

/// <summary>Manages the hospital service price list. Changes are Admin only; reading is open to everyone.</summary>
public class ServiceCatalogService : ServiceBase
{
    private static readonly Regex CodePattern = new(@"^[A-Za-z0-9_\-]+$", RegexOptions.Compiled);
    private const string DuplicateMessage = "A service with that code already exists.";

    public ServiceCatalogService(string connectionString) : base(connectionString) { }

    public DataTable GetAll(bool activeOnly = true) =>
        Run("ServiceCatalogService.GetAll", () =>
            Db.QuerySP("usp_tblServices_GetAll", P("@ActiveOnly", activeOnly)));

    public DataTable Search(string? term, bool activeOnly = true) =>
        Run("ServiceCatalogService.Search", () =>
            Db.QuerySP("usp_tblServices_Search",
                P("@SearchTerm", term?.Trim() ?? ""), P("@ActiveOnly", activeOnly)));

    public ServiceItem? GetById(int serviceId)
    {
        if (serviceId <= 0) return null;
        return Run("ServiceCatalogService.GetById", () =>
        {
            var dt = Db.QuerySP("usp_tblServices_GetById", P("@ServiceID", serviceId));
            return dt.Rows.Count == 0 ? null : Mappers.ToServiceItem(dt.Rows[0]);
        });
    }

    /// <summary>Adds a service. Returns the new ServiceID.</summary>
    public int Create(ServiceItem s)
    {
        var clean = Validate(s);
        Permissions.Require(AppAction.ManageServices);

        return Run("ServiceCatalogService.Create", () =>
        {
            var id = Db.ExecuteScalarSP("usp_tblServices_Create",
                P("@Code", clean.Code),
                P("@Name", clean.Name),
                P("@Category", clean.Category),
                P("@DrugCategoryCode", clean.DrugCategoryCode),
                P("@UnitPrice", clean.UnitPrice));
            return id == null ? 0 : Convert.ToInt32(id);
        }, DuplicateMessage);
    }

    public void Update(ServiceItem s)
    {
        if (s == null || s.ServiceID <= 0)
            throw new ValidationException("Select a service to update.", "Service");
        var clean = Validate(s);
        Permissions.Require(AppAction.ManageServices);

        RunVoid("ServiceCatalogService.Update", () =>
            Db.ExecuteSP("usp_tblServices_Update",
                P("@ServiceID", s.ServiceID),
                P("@Code", clean.Code),
                P("@Name", clean.Name),
                P("@Category", clean.Category),
                P("@DrugCategoryCode", clean.DrugCategoryCode),
                P("@UnitPrice", clean.UnitPrice),
                P("@IsActive", s.IsActive)), DuplicateMessage);
    }

    /// <summary>Hides a service from the charge screen. Old charges keep showing it.</summary>
    public void Deactivate(int serviceId)
    {
        if (serviceId <= 0)
            throw new ValidationException("Select a service to deactivate.", "Service");
        Permissions.Require(AppAction.ManageServices);

        RunVoid("ServiceCatalogService.Deactivate", () =>
            Db.ExecuteSP("usp_tblServices_Deactivate", P("@ServiceID", serviceId)));
    }

    /// <summary>Checks the fields and returns a cleaned copy (trimmed, canonical category).</summary>
    public static ServiceItem Validate(ServiceItem? s)
    {
        if (s == null) throw new ValidationException("Service details are required.");

        var code = (s.Code ?? "").Trim();
        if (code.Length == 0)
            throw new ValidationException("Code is required.", "Code");
        if (code.Length > 30)
            throw new ValidationException("Code must be 30 characters or fewer.", "Code");
        if (!CodePattern.IsMatch(code))
            throw new ValidationException("Code can only contain letters, numbers, - and _ (no spaces).", "Code");

        var name = (s.Name ?? "").Trim();
        if (name.Length == 0)
            throw new ValidationException("Name is required.", "Name");
        if (name.Length > 200)
            throw new ValidationException("Name must be 200 characters or fewer.", "Name");

        var category = ServiceCategory.All.FirstOrDefault(c =>
            c.Equals((s.Category ?? "").Trim(), StringComparison.OrdinalIgnoreCase));
        if (category == null)
            throw new ValidationException(
                "Choose a category: " + string.Join(", ", ServiceCategory.All) + ".", "Category");

        if (s.UnitPrice < 0)
            throw new ValidationException("Unit price cannot be negative.", "Unit price");
        if (decimal.Round(s.UnitPrice, 2) != s.UnitPrice)
            throw new ValidationException("Unit price can have at most 2 decimal places.", "Unit price");
        if (s.UnitPrice > 10_000_000m)
            throw new ValidationException("Unit price is too large.", "Unit price");

        var drug = string.IsNullOrWhiteSpace(s.DrugCategoryCode) ? null : s.DrugCategoryCode.Trim();
        if (drug != null && drug.Length > 50)
            throw new ValidationException("Drug category code must be 50 characters or fewer.", "Drug category");

        return new ServiceItem
        {
            ServiceID = s.ServiceID,
            Code = code.ToUpperInvariant(),
            Name = name,
            Category = category,
            DrugCategoryCode = drug,
            UnitPrice = s.UnitPrice,
            IsActive = s.IsActive
        };
    }
}
