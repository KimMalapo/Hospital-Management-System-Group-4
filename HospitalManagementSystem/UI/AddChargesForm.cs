using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogic;

namespace UI;

/// <summary>Add Charges window: pick a service and quantity, add it to the admission. Admins can void a charge.</summary>
public sealed partial class AddChargesForm : Form
{
    private readonly int _admissionId;
    private readonly ChargeService _charges;
    private readonly ServiceCatalogService _services;

    public AddChargesForm(int admissionId, string admissionNo)
    {
        _admissionId = admissionId;
        var connectionString = Config.GetConnectionString();
        _charges = new ChargeService(connectionString);
        _services = new ServiceCatalogService(connectionString);

        InitializeComponent();
        Text = $"Add charges - admission {admissionNo}";
        _quantity.Maximum = ChargeService.MaxQuantity;
        btnVoidCharge.Visible = Permissions.IsAllowed(AppAction.VoidCharge); // Admin only
        UITheme.Apply(this);
    }

    private void AddChargesForm_Load(object? sender, EventArgs e)
    {
        LoadServices();
        LoadCharges();
    }

    private void Search_TextChanged(object? sender, EventArgs e)
    {
        LoadServices();
    }

    private void LoadServices()
    {
        try
        {
            _servicesGrid.DataSource = _services.Search(_search.Text);
        }
        catch (Exception ex)
        {
            UIHelper.HandleDatabaseError(ex, "loading services");
        }
    }

    private void LoadCharges()
    {
        try
        {
            _chargesGrid.DataSource = _charges.GetByAdmission(_admissionId);
            _total.Text = $"Charges total: {_charges.GetSubtotal(_admissionId):₱#,##0.00}";
        }
        catch (Exception ex)
        {
            UIHelper.HandleDatabaseError(ex, "loading charges");
        }
    }

    private void Add_Click(object? sender, EventArgs e)
    {
        if (_servicesGrid.CurrentRow?.DataBoundItem is not DataRowView service)
        {
            UIHelper.ShowWarning(this, "Select a service from the list first.", "Service required");
            return;
        }

        try
        {
            _charges.Add(_admissionId, Convert.ToInt32(service["ServiceID"]), (int)_quantity.Value);
            _quantity.Value = 1;
            LoadCharges();
        }
        catch (Exception ex)
        {
            UIHelper.ShowServiceError(this, ex, "add charge");
        }
    }

    private void Void_Click(object? sender, EventArgs e)
    {
        if (_chargesGrid.CurrentRow?.DataBoundItem is not DataRowView charge)
        {
            UIHelper.ShowWarning(this, "Select the charge to void.", "Charge required");
            return;
        }

        var reason = UIHelper.AskText("Why is this charge being voided?", "Void charge");
        if (reason.Length == 0) return;

        try
        {
            _charges.Void(Convert.ToInt32(charge["ChargeID"]), reason);
            LoadCharges();
        }
        catch (Exception ex)
        {
            UIHelper.ShowServiceError(this, ex, "void charge");
        }
    }
}