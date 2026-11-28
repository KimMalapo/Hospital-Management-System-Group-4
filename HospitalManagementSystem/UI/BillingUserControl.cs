using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI;



public sealed partial class BillingUserControl : UserControl
{
    private readonly int? _initialAdmissionId;
    private AdmissionService? _admissionService;
    private BillingService? _billingService;
    private Bill? _bill;
    private bool _loading;

    public event EventHandler<BillNavigationEventArgs>? GoToPaymentRequested;

    public BillingUserControl(int? admissionId = null)
    {
        _initialAdmissionId = admissionId;
        InitializeComponent();
        UITheme.Apply(this);
        ConfigureItemsGrid();

        Load += BillingUserControl_Load;
        _admissions.SelectedIndexChanged += Admissions_SelectedIndexChanged;
        _generate.Click += Generate_Click;
        _goToPayment.Click += GoToPayment_Click;
        _printStatement.Click += PrintStatement_Click;
        _applyDiscount.Click += ApplyDiscount_Click;
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
                row["AdmissionDisplay"] = $"{row["AdmissionNo"]} | {row["PatientName"]} | Room {row["RoomNo"]} | {row["Status"]}";
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
        _errors.Clear();
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
                _goToPayment.Enabled = false;
                _printStatement.Enabled = false;
                SetMessage("No bill exists yet. Generate one to calculate the room stay and posted charges.", false);
                return;
            }

            _items.DataSource = _billingService.GetItems(_bill.BillID);
            SetBillTotals(_bill);
            _generate.Enabled = false;
            _generate.Text = "Bill Already Generated";
            _goToPayment.Enabled = !string.Equals(_bill.Status, BillStatus.Voided, StringComparison.OrdinalIgnoreCase);
            _printStatement.Enabled = true;
            _discount.Enabled = _bill.Status != BillStatus.Paid && _bill.Status != BillStatus.Voided && _bill.Balance > 0;
            _discountReason.Enabled = _discount.Enabled;
            _applyDiscount.Enabled = _discount.Enabled;
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

    private void GoToPayment_Click(object? sender, EventArgs e)
    {
        if (_bill == null) return;
        GoToPaymentRequested?.Invoke(this, new BillNavigationEventArgs(_bill.BillID, _bill.AdmissionID));
    }

    private void PrintStatement_Click(object? sender, EventArgs e)
    {
        if (_bill == null) return;
        try
        {
            using var doc = new UI.Printing.StatementPrintDocument(_bill.BillID, Config.GetConnectionString());
            UI.Printing.PrintHelper.ShowPreview(this, doc);
        }
        catch (Exception ex)
        {
            Logger.Error("BillingUserControl.PrintStatement failed", ex);
            SetMessage("The billing statement could not be opened for printing.");
        }
    }

    private void ApplyDiscount_Click(object? sender, EventArgs e)
    {
        _errors.Clear();
        _message.Text = string.Empty;
        if (_bill == null || _billingService == null) return;

        if (!decimal.TryParse(_discount.Text.Trim(), out var amount) || amount < 0)
        {
            _errors.SetError(_discount, "Enter a valid discount amount of zero or more.");
            return;
        }
        if (amount > _bill.Subtotal)
        {
            _errors.SetError(_discount, $"Discount cannot exceed the subtotal of {FormatMoney(_bill.Subtotal)}.");
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
            if (ex is ValidationException vex)
                _errors.SetError(_discount, vex.Message);
            SetMessage(ex is ValidationException ? ex.Message : "The discount could not be applied. Please try again.");
        }
        finally
        {
            if (_bill != null)
                _applyDiscount.Enabled = _bill.Status != BillStatus.Paid && _bill.Status != BillStatus.Voided && _bill.Balance > 0;
        }
    }

    private void ConfigureItemsGrid()
    {
        _items.AutoGenerateColumns = false;
        _items.RowTemplate.Height = 36;
        _items.Columns.Clear();
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemType", HeaderText = "Type", FillWeight = 18 });
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", FillWeight = 42 });
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Quantity", FillWeight = 13 });
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Unit Price", FillWeight = 14, DefaultCellStyle = new DataGridViewCellStyle { Format = "₱#,##0.00", Alignment = DataGridViewContentAlignment.MiddleRight } });
        _items.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Amount", HeaderText = "Line Total", FillWeight = 17, DefaultCellStyle = new DataGridViewCellStyle { Format = "₱#,##0.00", Alignment = DataGridViewContentAlignment.MiddleRight } });
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
            _generate.Text = "Generate Bill";
        }
        else
        {
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
        _goToPayment.Enabled = false;
        _printStatement.Enabled = false;
        _message.Text = string.Empty;
    }

    private void SetMessage(string text, bool isError = true)
    {
        _message.ForeColor = isError ? Color.FromArgb(185, 28, 28) : Color.FromArgb(21, 128, 61);
        _message.Text = text;
    }

    private static string FormatMoney(decimal amount) => amount.ToString("₱#,##0.00");
}

public sealed class BillNavigationEventArgs : EventArgs
{
    public int BillId { get; }
    public int AdmissionId { get; }

    public BillNavigationEventArgs(int billId, int admissionId)
    {
        BillId = billId;
        AdmissionId = admissionId;
    }
}