using System.Data;
using System.Drawing.Printing;
using BusinessLogic;
using Model;

namespace UI;

/// <summary>Payments window for one bill: record a payment, see the payment history, print a receipt. Admins can void a payment.</summary>
public sealed class PaymentsForm : Form
{
    private readonly int _billId;
    private readonly PaymentService _payments;
    private readonly BillingService _billing;

    private readonly Label _summary = new();
    private readonly TextBox _amount = new();
    private readonly ComboBox _method = new();
    private readonly TextBox _reference = new();
    private readonly Button _record = new();
    private readonly DataGridView _grid = new();
    private readonly Button _print = new();
    private readonly Button _void = new();
    private readonly Button _close = new();

    public PaymentsForm(int billId, string billNo)
    {
        _billId = billId;
        var connectionString = Config.GetConnectionString();
        _payments = new PaymentService(connectionString);
        _billing = new BillingService(connectionString);

        Text = $"Payments - bill {billNo}";
        Size = new Size(960, 640);
        MinimumSize = new Size(820, 560);
        StartPosition = FormStartPosition.CenterParent;
        Font = new Font("Segoe UI", 10);
        BuildLayout();
        UITheme.Apply(this);
        Load += (_, _) => Reload();
    }

    private void BuildLayout()
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(16) };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

        _summary.Dock = DockStyle.Fill;
        _summary.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        _summary.TextAlign = ContentAlignment.MiddleLeft;
        layout.Controls.Add(_summary, 0, 0);

        var entry = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
        entry.Controls.Add(new Label { Text = "Amount", AutoSize = true, Margin = new Padding(0, 8, 6, 0) });
        _amount.Width = 110;
        entry.Controls.Add(_amount);
        entry.Controls.Add(new Label { Text = "Method", AutoSize = true, Margin = new Padding(14, 8, 6, 0) });
        _method.DropDownStyle = ComboBoxStyle.DropDownList;
        _method.Items.AddRange(PaymentMethod.All);
        _method.SelectedIndex = 0;
        _method.Width = 140;
        entry.Controls.Add(_method);
        entry.Controls.Add(new Label { Text = "Reference no.", AutoSize = true, Margin = new Padding(14, 8, 6, 0) });
        _reference.Width = 160;
        _reference.MaxLength = 100;
        entry.Controls.Add(_reference);
        _record.Name = "btnRecordPayment";
        _record.Text = "Record payment";
        _record.Size = new Size(150, 32);
        _record.Margin = new Padding(14, 0, 0, 0);
        _record.Click += Record_Click;
        entry.Controls.Add(_record);
        layout.Controls.Add(entry, 0, 1);

        _grid.Dock = DockStyle.Fill;
        _grid.ReadOnly = true;
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AutoGenerateColumns = false;
        _grid.MultiSelect = false;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.RowHeadersVisible = false;
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReceiptNo", HeaderText = "Receipt", FillWeight = 18 });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "PaymentDate", HeaderText = "Date", FillWeight = 20,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Amount", HeaderText = "Amount", FillWeight = 14,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "₱#,##0.00", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Method", HeaderText = "Method", FillWeight = 14 });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReferenceNo", HeaderText = "Reference", FillWeight = 18 });
        _grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", FillWeight = 12 });
        layout.Controls.Add(_grid, 0, 2);

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
        _close.Name = "btnClose";
        _close.Text = "Close";
        _close.Size = new Size(110, 32);
        _close.DialogResult = DialogResult.OK;
        buttons.Controls.Add(_close);
        _void.Name = "btnVoidPayment";
        _void.Text = "Void payment";
        _void.Size = new Size(150, 32);
        _void.Visible = Permissions.IsAllowed(AppAction.VoidPayment); // Admin only
        _void.Click += Void_Click;
        buttons.Controls.Add(_void);
        _print.Name = "btnPrintReceipt";
        _print.Text = "Print receipt";
        _print.Size = new Size(150, 32);
        _print.Click += Print_Click;
        buttons.Controls.Add(_print);
        layout.Controls.Add(buttons, 0, 3);

        Controls.Add(layout);
        CancelButton = _close;
    }

    // Reloads the bill totals and the payment list.
    private void Reload()
    {
        try
        {
            var bill = _billing.GetById(_billId);
            if (bill != null)
            {
                _summary.Text = $"Total {bill.TotalAmount:₱#,##0.00}   |   Paid {bill.AmountPaid:₱#,##0.00}   |   Balance {bill.Balance:₱#,##0.00}   |   {bill.Status}";
                _record.Enabled = bill.Status != BillStatus.Paid && bill.Status != BillStatus.Voided && bill.Balance > 0;
            }
            _grid.DataSource = _payments.GetByBill(_billId);
        }
        catch (Exception ex)
        {
            UIHelper.HandleDatabaseError(ex, "loading payments");
        }
    }

    private void Record_Click(object? sender, EventArgs e)
    {
        if (!decimal.TryParse(_amount.Text.Trim(), out var amount) || amount <= 0)
        {
            UIHelper.ShowWarning(this, "Enter the amount received (more than zero).", "Amount required");
            return;
        }

        try
        {
            var payment = _payments.Post(_billId, amount, _method.SelectedItem?.ToString(), _reference.Text);
            _amount.Clear();
            _reference.Clear();
            Reload();
            if (MessageBox.Show(this, $"Payment recorded. Receipt {payment.ReceiptNo}.\n\nPrint the receipt now?", "Payment saved",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                PrintReceipt(payment.PaymentID);
        }
        catch (Exception ex)
        {
            UIHelper.ShowServiceError(this, ex, "record payment");
        }
    }

    private void Void_Click(object? sender, EventArgs e)
    {
        if (_grid.CurrentRow?.DataBoundItem is not DataRowView row)
        {
            UIHelper.ShowWarning(this, "Select the payment to void.", "Payment required");
            return;
        }
        if (Convert.ToString(row["Status"]) == "Voided")
        {
            UIHelper.ShowWarning(this, "That payment is already voided.", "Payment voided");
            return;
        }

        var reason = UIHelper.AskText("Why is this payment being voided?", "Void payment");
        if (reason.Length == 0) return;

        try
        {
            _payments.Void(Convert.ToInt32(row["PaymentID"]), reason);
            Reload();
        }
        catch (Exception ex)
        {
            UIHelper.ShowServiceError(this, ex, "void payment");
        }
    }

    private void Print_Click(object? sender, EventArgs e)
    {
        if (_grid.CurrentRow?.DataBoundItem is not DataRowView row)
        {
            UIHelper.ShowWarning(this, "Select a payment to print its receipt.", "Payment required");
            return;
        }
        if (Convert.ToString(row["Status"]) == "Voided")
        {
            UIHelper.ShowWarning(this, "A voided payment has no receipt.", "Payment voided");
            return;
        }

        PrintReceipt(Convert.ToInt32(row["PaymentID"]));
    }

    // Opens a print preview of a plain receipt (the user can print from there).
    private void PrintReceipt(int paymentId)
    {
        try
        {
            var receipt = _payments.GetReceipt(paymentId);
            if (receipt == null)
            {
                UIHelper.ShowWarning(this, "The receipt could not be found.", "Receipt not found");
                return;
            }

            using var document = new PrintDocument();
            document.PrintPage += (_, e) => DrawReceipt(e, receipt);
            using var preview = new PrintPreviewDialog { Document = document, Width = 800, Height = 900 };
            preview.ShowDialog(this);
        }
        catch (Exception ex)
        {
            UIHelper.ShowServiceError(this, ex, "print receipt");
        }
    }

    private static void DrawReceipt(PrintPageEventArgs e, ReceiptData r)
    {
        var graphics = e.Graphics!;
        using var title = new Font("Segoe UI", 16, FontStyle.Bold);
        using var body = new Font("Segoe UI", 11);
        var left = e.MarginBounds.Left;
        var y = (float)e.MarginBounds.Top;

        graphics.DrawString("Payment Receipt", title, Brushes.Black, left, y);
        y += 50;

        var lines = new (string Label, string Value)[]
        {
            ("Receipt no.", r.ReceiptNo ?? ""),
            ("Date", r.PaymentDate?.ToString("g") ?? ""),
            ("Patient", r.PatientName ?? ""),
            ("Admission", r.AdmissionNo ?? ""),
            ("Bill", r.BillNo ?? ""),
            ("Method", r.Method ?? ""),
            ("Reference", r.ReferenceNo ?? ""),
            ("Amount paid", r.Amount.ToString("₱#,##0.00")),
            ("Bill total", r.TotalAmount.ToString("₱#,##0.00")),
            ("Balance after", r.BalanceAfter.ToString("₱#,##0.00")),
            ("Received by", r.ReceivedBy ?? "")
        };

        foreach (var (label, value) in lines)
        {
            graphics.DrawString(label, body, Brushes.Gray, left, y);
            graphics.DrawString(value, body, Brushes.Black, left + 170, y);
            y += 30;
        }
    }
}
