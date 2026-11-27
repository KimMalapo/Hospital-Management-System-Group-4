using System.Data;
using BusinessLogic;

namespace UI;

/// <summary>Add Charges window: pick a service and quantity, add it to the admission. Admins can void a charge.</summary>
public sealed class AddChargesForm : Form
{
    private readonly int _admissionId;
    private readonly ChargeService _charges;
    private readonly ServiceCatalogService _services;

    private readonly TextBox _search = new();
    private readonly DataGridView _servicesGrid = new();
    private readonly NumericUpDown _quantity = new();
    private readonly Button _add = new();
    private readonly DataGridView _chargesGrid = new();
    private readonly Label _total = new();
    private readonly Button _void = new();
    private readonly Button _close = new();

    public AddChargesForm(int admissionId, string admissionNo)
    {
        _admissionId = admissionId;
        var connectionString = Config.GetConnectionString();
        _charges = new ChargeService(connectionString);
        _services = new ServiceCatalogService(connectionString);

        Text = $"Add charges - admission {admissionNo}";
        Size = new Size(980, 700);
        MinimumSize = new Size(820, 600);
        StartPosition = FormStartPosition.CenterParent;
        Font = new Font("Segoe UI", 10);
        BuildLayout();
        UITheme.Apply(this);
        Load += (_, _) => { LoadServices(); LoadCharges(); };
    }

    private void BuildLayout()
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 6, Padding = new Padding(16) };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

        _search.Dock = DockStyle.Fill;
        _search.PlaceholderText = "Search services by name or code";
        _search.TextChanged += (_, _) => LoadServices();
        layout.Controls.Add(_search, 0, 0);

        ConfigureGrid(_servicesGrid);
        _servicesGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Service", FillWeight = 50 });
        _servicesGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Category", HeaderText = "Category", FillWeight = 25 });
        _servicesGrid.Columns.Add(MoneyColumn("UnitPrice", "Unit price", 25));
        layout.Controls.Add(_servicesGrid, 0, 1);

        var addRow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 8, 0, 0) };
        addRow.Controls.Add(new Label { Text = "Quantity", AutoSize = true, Margin = new Padding(0, 8, 8, 0) });
        _quantity.Minimum = 1;
        _quantity.Maximum = ChargeService.MaxQuantity;
        _quantity.Value = 1;
        _quantity.Width = 80;
        addRow.Controls.Add(_quantity);
        _add.Name = "btnAddCharge";
        _add.Text = "Add charge";
        _add.Size = new Size(140, 32);
        _add.Margin = new Padding(16, 0, 0, 0);
        _add.Click += Add_Click;
        addRow.Controls.Add(_add);
        layout.Controls.Add(addRow, 0, 2);

        ConfigureGrid(_chargesGrid);
        _chargesGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Charge", FillWeight = 38 });
        _chargesGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Qty", FillWeight = 8 });
        _chargesGrid.Columns.Add(MoneyColumn("UnitPrice", "Unit price", 16));
        _chargesGrid.Columns.Add(MoneyColumn("LineTotal", "Total", 16));
        _chargesGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "ChargedAt", HeaderText = "Added", FillWeight = 22,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
        });
        layout.Controls.Add(_chargesGrid, 0, 3);

        _total.Dock = DockStyle.Fill;
        _total.TextAlign = ContentAlignment.MiddleRight;
        _total.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        layout.Controls.Add(_total, 0, 4);

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
        _close.Name = "btnClose";
        _close.Text = "Close";
        _close.Size = new Size(120, 32);
        _close.DialogResult = DialogResult.OK;
        buttons.Controls.Add(_close);
        _void.Name = "btnVoidCharge";
        _void.Text = "Void selected charge";
        _void.Size = new Size(180, 32);
        _void.Visible = Permissions.IsAllowed(AppAction.VoidCharge); // Admin only
        _void.Click += Void_Click;
        buttons.Controls.Add(_void);
        layout.Controls.Add(buttons, 0, 5);

        Controls.Add(layout);
        CancelButton = _close;
    }

    private static void ConfigureGrid(DataGridView grid)
    {
        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoGenerateColumns = false;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.RowHeadersVisible = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private static DataGridViewTextBoxColumn MoneyColumn(string property, string header, float weight) => new()
    {
        DataPropertyName = property,
        HeaderText = header,
        FillWeight = weight,
        DefaultCellStyle = new DataGridViewCellStyle { Format = "₱#,##0.00", Alignment = DataGridViewContentAlignment.MiddleRight }
    };

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
