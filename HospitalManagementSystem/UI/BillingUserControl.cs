using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI;

/// <summary>Billing screen: makes the bill and applies a discount. Controls are made in code.</summary>
public sealed class BillingUserControl : UserControl
{
    private readonly ComboBox _admissions = new();
    private readonly DataGridView _items = new();
    private readonly TextBox _discount = new();
    private readonly TextBox _discountReason = new();
    private readonly Label _subtotal = new();
    private readonly Label _discountTotal = new();
    private readonly Label _total = new();
    private readonly Label _paid = new();
    private readonly Label _balance = new();
    private readonly Label _billStatus = new();
    private readonly Label _message = new();
    private readonly Button _generate = new();
    private readonly Button _applyDiscount = new();
    private readonly Button _payments = new();
    private readonly Button _voidBill = new();
    private readonly ErrorProvider _errors = new();
    private readonly int? _initialAdmissionId;
    private AdmissionService? _admissionService;
    private BillingService? _billingService;
    private Bill? _bill;
    private bool _loading;

    public BillingUserControl(int? admissionId = null)
    {
        _initialAdmissionId = admissionId;
        Dock = DockStyle.Fill;
        _errors.ContainerControl = this;
        _errors.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        BuildLayout();
        Load += BillingUserControl_Load;
        _admissions.SelectedIndexChanged += Admissions_SelectedIndexChanged;
        _generate.Click += Generate_Click;
        _applyDiscount.Click += ApplyDiscount_Click;
        _payments.Click += Payments_Click;
        _voidBill.Click += VoidBill_Click;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _errors.Dispose();
        base.Dispose(disposing);
    }

    private void BuildLayout()
    {
        BackColor = Color.FromArgb(245, 247, 251);
        Font = new Font("Segoe UI", 9.5F);

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(28, 22, 28, 24),
            ColumnCount = 1,
            RowCount = 3,
            BackColor = BackColor
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 86));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var heading = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = Padding.Empty };
        heading.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        heading.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        heading.Controls.Add(new Label
        {
            Text = "Billing",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 41, 59),
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);
        heading.Controls.Add(new Label
        {
            Text = "Generate and review the itemized bill for an admission.",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 9.5F),
            ForeColor = Color.FromArgb(100, 116, 139),
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 1);
        root.Controls.Add(heading, 0, 0);

        var admissionCard = CreateCard(new Padding(16, 10, 16, 10));
        var admissionLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 2, Margin = Padding.Empty };
        admissionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
        admissionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        admissionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180));
        admissionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
        admissionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var admissionLabel = CreateFieldLabel("Admission");
        admissionLayout.Controls.Add(admissionLabel, 0, 0);
        admissionLayout.SetColumnSpan(admissionLabel, 2);
        _admissions.Dock = DockStyle.Fill;
        _admissions.DropDownStyle = ComboBoxStyle.DropDownList;
        _admissions.Font = new Font("Segoe UI", 10);
        _admissions.Margin = new Padding(0, 4, 12, 0);
        admissionLayout.Controls.Add(_admissions, 0, 1);
        admissionLayout.SetColumnSpan(_admissions, 2);
        _generate.Text = "Generate bill";
        StylePrimaryButton(_generate);
        _generate.Dock = DockStyle.Fill;
        _generate.Margin = new Padding(0, 4, 0, 0);
        admissionLayout.Controls.Add(_generate, 2, 1);
        admissionCard.Controls.Add(admissionLayout);
        root.Controls.Add(admissionCard, 0, 1);

        var body = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = new Padding(0, 14, 0, 0) };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69));
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31));
        root.Controls.Add(body, 0, 2);

        var itemsCard = CreateCard(new Padding(14));
        var itemsLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = Padding.Empty };
        itemsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        itemsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        itemsLayout.Controls.Add(CreateSectionTitle("Bill items"), 0, 0);
        ConfigureItemsGrid();
        _items.Dock = DockStyle.Fill;
        itemsLayout.Controls.Add(_items, 0, 1);
        itemsCard.Controls.Add(itemsLayout);
        body.Controls.Add(itemsCard, 0, 0);

        var totalsCard = CreateCard(new Padding(18));
        var totals = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 13, Margin = Padding.Empty };
        totals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        totals.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        for (var i = 0; i < 12; i++) totals.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        totals.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var summaryTitle = CreateSectionTitle("Bill summary");
        totals.Controls.Add(summaryTitle, 0, 0);
        totals.SetColumnSpan(summaryTitle, 2);
        AddTotal(totals, 1, "Status", _billStatus);
        AddTotal(totals, 2, "Subtotal", _subtotal);
        AddTotal(totals, 3, "Discount", _discountTotal);
        AddTotal(totals, 4, "Total", _total, true);
        AddTotal(totals, 5, "Amount paid", _paid);
        AddTotal(totals, 6, "Balance", _balance, true);

        var discountHeading = new Label
        {
            Text = "Apply discount",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 41, 59),
            TextAlign = ContentAlignment.MiddleLeft,
            Margin = new Padding(0, 12, 0, 0)
        };
        totals.Controls.Add(discountHeading, 0, 7);
        totals.SetColumnSpan(discountHeading, 2);
        _discount.PlaceholderText = "Amount";
        _discount.Dock = DockStyle.Fill;
        _discount.Margin = new Padding(0, 4, 8, 4);
        totals.Controls.Add(_discount, 0, 8);
        _discountReason.PlaceholderText = "Reason (required)";
        _discountReason.Dock = DockStyle.Fill;
        _discountReason.MaxLength = 300;
        _discountReason.Margin = new Padding(0, 4, 0, 4);
        totals.Controls.Add(_discountReason, 1, 8);
        _applyDiscount.Text = "Apply discount";
        StylePrimaryButton(_applyDiscount);
        _applyDiscount.Dock = DockStyle.Fill;
        _applyDiscount.Margin = new Padding(0, 4, 0, 4);
        totals.Controls.Add(_applyDiscount, 0, 9);
        totals.SetColumnSpan(_applyDiscount, 2);
        _payments.Text = "Payments / receipts";
        StylePrimaryButton(_payments);
        _payments.Dock = DockStyle.Fill;
        _payments.Margin = new Padding(0, 4, 0, 4);
        totals.Controls.Add(_payments, 0, 10);
        totals.SetColumnSpan(_payments, 2);
        _voidBill.Text = "Void bill";
        _voidBill.Dock = DockStyle.Fill;
        _voidBill.Margin = new Padding(0, 4, 0, 4);
        _voidBill.Visible = Permissions.IsAllowed(AppAction.VoidBill); // Admin only
        totals.Controls.Add(_voidBill, 0, 11);
        totals.SetColumnSpan(_voidBill, 2);
        _message.Dock = DockStyle.Fill;
        _message.ForeColor = Color.FromArgb(185, 28, 28);
        _message.TextAlign = ContentAlignment.MiddleLeft;
        _message.AutoEllipsis = true;
        totals.Controls.Add(_message, 0, 12);
        totals.SetColumnSpan(_message, 2);
        totalsCard.Controls.Add(totals);
        body.Controls.Add(totalsCard, 1, 0);

        Controls.Add(root);
    }

    private void BillingUserControl_Load(object? sender, EventArgs e)
    {
        try
        {
            var connectionString = Config.GetConnectionString();
            _admissionService = new AdmissionService(connectionString);
            _billingService = new BillingService(connectionString);
            _loading = true;
            var admissions = _admissionService.Search(string.Empty);
            admissions.Columns.Add("AdmissionDisplay", typeof(string));
            foreach (DataRow row in admissions.Rows)
            {
                row["AdmissionDisplay"] = $"{row["AdmissionNo"]}  |  {row["PatientName"]}  |  Room {row["RoomNo"]}  |  {row["Status"]}";
            }
            _admissions.DataSource = admissions;
            _admissions.DisplayMember = "AdmissionDisplay";
            _admissions.ValueMember = "AdmissionID";
            if (_initialAdmissionId.HasValue)
                _admissions.SelectedValue = _initialAdmissionId.Value;
            else if (_admissions.Items.Count > 0)
                _admissions.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Logger.Error("BillingUserControl.Load failed", ex);
            SetMessage("Admissions could not be loaded. Check the database connection and try again.");
        }
        finally
        {
            _loading = false;
        }
        RefreshBill();
    }

    private void Admissions_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!_loading) RefreshBill();
    }

    private void RefreshBill()
    {
        if (_billingService == null || _admissions.SelectedValue is not int admissionId)
        {
            ClearBill();
            return;
        }

        try
        {
            _loading = true;
            _message.Text = string.Empty;
            _bill = _billingService.GetByAdmission(admissionId);
            if (_bill == null)
            {
                _items.DataSource = null;
                SetBillTotals(null);
                _generate.Enabled = IsSelectedAdmissionActive();
                SetMessage("No bill exists yet. Generate one to calculate the room stay and posted charges.", false);
                return;
            }

            _items.DataSource = _billingService.GetItems(_bill.BillID);
            SetBillTotals(_bill);
            _generate.Enabled = false;
            _generate.Text = "Bill already generated";
        }
        catch (Exception ex)
        {
            Logger.Error("BillingUserControl.RefreshBill failed", ex);
            SetMessage("Bill details could not be loaded. Please try again.");
        }
        finally
        {
            _loading = false;
        }
    }

    private void Generate_Click(object? sender, EventArgs e)
    {
        if (_admissions.SelectedValue is not int admissionId || _billingService == null) return;
        try
        {
            _generate.Enabled = false;
            _bill = _billingService.Generate(admissionId);
            RefreshBill();
            SetMessage($"Bill {_bill.BillNo} is ready.", false);
        }
        catch (Exception ex)
        {
            Logger.Error("BillingUserControl.Generate failed", ex);
            SetMessage(ex is ValidationException ? ex.Message : "The bill could not be generated. Please try again.");
            _generate.Enabled = false;
        }
    }

    private void ApplyDiscount_Click(object? sender, EventArgs e)
    {
        _errors.Clear();
        if (_bill == null || _billingService == null) return;
        if (!decimal.TryParse(_discount.Text.Trim(), out var amount) || amount < 0)
        {
            _errors.SetError(_discount, "Enter a valid discount amount of zero or more.");
            return;
        }
        if (string.IsNullOrWhiteSpace(_discountReason.Text))
        {
            _errors.SetError(_discountReason, "Enter a reason for the discount.");
            return;
        }

        try
        {
            _applyDiscount.Enabled = false;
            _billingService.ApplyDiscount(_bill.BillID, amount, _discountReason.Text.Trim());
            _discount.Clear();
            _discountReason.Clear();
            RefreshBill();
            SetMessage("Discount updated.", false);
        }
        catch (Exception ex)
        {
            Logger.Error("BillingUserControl.ApplyDiscount failed", ex);
            SetMessage(ex is ValidationException ? ex.Message : "The discount could not be applied. Please try again.");
        }
    }

    // Opens the payments window; the bill totals may change there, so reload afterwards.
    private void Payments_Click(object? sender, EventArgs e)
    {
        if (_bill == null) return;

        using (var form = new PaymentsForm(_bill.BillID, _bill.BillNo ?? string.Empty))
            form.ShowDialog(this);

        RefreshBill();
    }

    private void VoidBill_Click(object? sender, EventArgs e)
    {
        if (_bill == null || _billingService == null) return;

        var reason = UIHelper.AskText("Why is this bill being voided?", "Void bill");
        if (reason.Length == 0) return;

        try
        {
            _billingService.Void(_bill.BillID, reason);
            RefreshBill();
            SetMessage("Bill voided.", false);
        }
        catch (Exception ex)
        {
            UIHelper.ShowServiceError(this, ex, "void bill");
        }
    }

    private void ConfigureItemsGrid()
    {
        _items.ReadOnly = true;
        _items.AllowUserToAddRows = false;
        _items.AllowUserToDeleteRows = false;
        _items.AllowUserToResizeRows = false;
        _items.AutoGenerateColumns = false;
        _items.MultiSelect = false;
        _items.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _items.RowHeadersVisible = false;
        _items.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _items.BackgroundColor = Color.White;
        _items.BorderStyle = BorderStyle.None;
        _items.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        _items.GridColor = Color.FromArgb(226, 232, 240);
        _items.EnableHeadersVisualStyles = false;
        _items.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        _items.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        _items.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
        _items.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        _items.ColumnHeadersHeight = 38;
        _items.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
        _items.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        _items.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
        _items.RowTemplate.Height = 36;
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemType", HeaderText = "Type", FillWeight = 18 });
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", FillWeight = 42 });
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Quantity", FillWeight = 13 });
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Unit price", FillWeight = 14, DefaultCellStyle = new DataGridViewCellStyle { Format = "₱#,##0.00", Alignment = DataGridViewContentAlignment.MiddleRight } });
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Amount", HeaderText = "Line total", FillWeight = 17, DefaultCellStyle = new DataGridViewCellStyle { Format = "₱#,##0.00", Alignment = DataGridViewContentAlignment.MiddleRight } });
    }

    private bool IsSelectedAdmissionActive() =>
        _admissions.SelectedItem is DataRowView row &&
        string.Equals(Convert.ToString(row["Status"]), AdmissionStatus.Admitted, StringComparison.OrdinalIgnoreCase);

    private void SetBillTotals(Bill? bill)
    {
        _billStatus.Text = bill?.Status ?? "Not generated";
        _subtotal.Text = FormatMoney(bill?.Subtotal ?? 0m);
        _discountTotal.Text = FormatMoney(bill?.Discount ?? 0m);
        _total.Text = FormatMoney(bill?.TotalAmount ?? 0m);
        _paid.Text = FormatMoney(bill?.AmountPaid ?? 0m);
        _balance.Text = FormatMoney(bill?.Balance ?? 0m);
        if (bill == null)
        {
            _discount.Clear();
            _discountReason.Clear();
            _discount.Enabled = false;
            _discountReason.Enabled = false;
            _applyDiscount.Enabled = false;
            _payments.Enabled = false;
            _voidBill.Enabled = false;
            _generate.Text = "Generate bill";
        }
        else
        {
            _payments.Enabled = bill.Status != BillStatus.Voided;
            _voidBill.Enabled = bill.Status != BillStatus.Voided;
            _discount.Enabled = bill.Status != BillStatus.Paid && bill.Status != BillStatus.Voided && bill.Balance > 0;
            _discountReason.Enabled = _discount.Enabled;
            _applyDiscount.Enabled = _discount.Enabled;
        }
    }

    private void ClearBill()
    {
        _bill = null;
        _items.DataSource = null;
        SetBillTotals(null);
        _generate.Enabled = false;
        _message.Text = string.Empty;
    }

    private void SetMessage(string text, bool isError = true)
    {
        _message.ForeColor = isError ? Color.FromArgb(185, 28, 28) : Color.FromArgb(21, 128, 61);
        _message.Text = text;
    }

    private static string FormatMoney(decimal amount) => amount.ToString("₱#,##0.00");

    private static Panel CreateCard(Padding padding) => new()
    {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        BorderStyle = BorderStyle.FixedSingle,
        Padding = padding,
        Margin = Padding.Empty
    };

    private static Label CreateSectionTitle(string text) => new()
    {
        Text = text,
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 11, FontStyle.Bold),
        ForeColor = Color.FromArgb(30, 41, 59),
        TextAlign = ContentAlignment.MiddleLeft,
        Margin = Padding.Empty
    };

    private static Label CreateFieldLabel(string text) => new()
    {
        Text = text,
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 9, FontStyle.Bold),
        ForeColor = Color.FromArgb(71, 85, 105),
        TextAlign = ContentAlignment.MiddleLeft,
        Margin = Padding.Empty
    };

    private static void AddTotal(TableLayoutPanel layout, int row, string label, Label value, bool emphasized = false)
    {
        value.Dock = DockStyle.Fill;
        value.TextAlign = ContentAlignment.MiddleRight;
        value.Font = new Font("Segoe UI", emphasized ? 10F : 9.5F, emphasized ? FontStyle.Bold : FontStyle.Regular);
        value.ForeColor = emphasized ? Color.FromArgb(30, 41, 59) : Color.FromArgb(71, 85, 105);
        layout.Controls.Add(CreateFieldLabel(label), 0, row);
        layout.Controls.Add(value, 1, row);
    }

    private static void StylePrimaryButton(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.BackColor = Color.FromArgb(37, 99, 235);
        button.ForeColor = Color.White;
        button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        button.UseVisualStyleBackColor = false;
        button.Cursor = Cursors.Hand;
    }
}
