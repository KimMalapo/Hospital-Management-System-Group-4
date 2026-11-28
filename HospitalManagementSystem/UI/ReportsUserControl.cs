using System.Data;
using System.Text;
using BusinessLogic;

namespace UI;

/// <summary>Reports screen (Admin only): pick a report and a date range, run it, and save the result as a CSV file.</summary>
public sealed class ReportsUserControl : UserControl
{
    private const string BillingSummary = "Billing summary";
    private const string PaymentHistory = "Payment history";
    private const string DailyCollections = "Daily collections";
    private const string OutstandingBalances = "Outstanding balances";
    private const string Admissions = "Admissions";
    private const string RoomOccupancy = "Room occupancy";

    private readonly ComboBox _report = new();
    private readonly DateTimePicker _from = new();
    private readonly DateTimePicker _to = new();
    private readonly Button _run = new();
    private readonly Button _export = new();
    private readonly DataGridView _grid = new();
    private readonly Label _status = new();
    private ReportService? _reports;

    public ReportsUserControl()
    {
        Dock = DockStyle.Fill;
        BackColor = Color.FromArgb(245, 247, 251);
        Font = new Font("Segoe UI", 10);
        BuildLayout();
        Load += (_, _) => _reports = new ReportService(Config.GetConnectionString());
    }

    private void BuildLayout()
    {
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(28, 22, 28, 24) };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));

        root.Controls.Add(new Label
        {
            Text = "Reports",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 41, 59),
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);

        var filters = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
        filters.Controls.Add(new Label { Text = "Report", AutoSize = true, Margin = new Padding(0, 8, 6, 0) });
        _report.DropDownStyle = ComboBoxStyle.DropDownList;
        _report.Items.AddRange(new object[] { BillingSummary, PaymentHistory, DailyCollections, OutstandingBalances, Admissions, RoomOccupancy });
        _report.SelectedIndex = 0;
        _report.Width = 220;
        _report.SelectedIndexChanged += (_, _) => UpdateDateControls();
        filters.Controls.Add(_report);

        filters.Controls.Add(new Label { Text = "From", AutoSize = true, Margin = new Padding(16, 8, 6, 0) });
        _from.Format = DateTimePickerFormat.Short;
        _from.Width = 120;
        _from.Value = DateTime.Today.AddDays(-30);
        filters.Controls.Add(_from);
        filters.Controls.Add(new Label { Text = "To", AutoSize = true, Margin = new Padding(12, 8, 6, 0) });
        _to.Format = DateTimePickerFormat.Short;
        _to.Width = 120;
        _to.Value = DateTime.Today;
        filters.Controls.Add(_to);

        _run.Text = "Run report";
        _run.Size = new Size(130, 32);
        _run.Margin = new Padding(16, 0, 0, 0);
        StyleButton(_run, primary: true);
        _run.Click += Run_Click;
        filters.Controls.Add(_run);

        _export.Text = "Save as CSV";
        _export.Size = new Size(130, 32);
        _export.Margin = new Padding(8, 0, 0, 0);
        StyleButton(_export, primary: false);
        _export.Click += Export_Click;
        filters.Controls.Add(_export);
        root.Controls.Add(filters, 0, 1);

        _grid.Dock = DockStyle.Fill;
        _grid.ReadOnly = true;
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AutoGenerateColumns = true;
        _grid.RowHeadersVisible = false;
        _grid.BackgroundColor = Color.White;
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        root.Controls.Add(_grid, 0, 2);

        _status.Dock = DockStyle.Fill;
        _status.TextAlign = ContentAlignment.MiddleLeft;
        _status.ForeColor = Color.FromArgb(100, 116, 139);
        root.Controls.Add(_status, 0, 3);

        Controls.Add(root);
        UpdateDateControls();
    }

    private static void StyleButton(Button button, bool primary)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = primary ? 0 : 1;
        button.BackColor = primary ? Color.FromArgb(37, 99, 235) : Color.White;
        button.ForeColor = primary ? Color.White : Color.FromArgb(51, 65, 85);
        button.UseVisualStyleBackColor = false;
        button.Cursor = Cursors.Hand;
    }

    // Only the reports that use dates need the date pickers: daily collections uses just "To" as its day.
    private void UpdateDateControls()
    {
        var report = _report.SelectedItem?.ToString();
        var usesRange = report is BillingSummary or PaymentHistory or Admissions;
        _from.Enabled = usesRange;
        _to.Enabled = usesRange || report == DailyCollections;
    }

    private void Run_Click(object? sender, EventArgs e)
    {
        if (_reports == null) return;

        try
        {
            DataTable result = _report.SelectedItem?.ToString() switch
            {
                BillingSummary => _reports.BillingSummary(_from.Value, _to.Value),
                PaymentHistory => _reports.PaymentHistory(_from.Value, _to.Value),
                DailyCollections => _reports.DailyCollections(_to.Value),
                OutstandingBalances => _reports.OutstandingBalances(),
                Admissions => _reports.Admissions(_from.Value, _to.Value),
                RoomOccupancy => _reports.RoomOccupancy(),
                _ => new DataTable()
            };

            _grid.DataSource = result;
            _status.Text = $"{result.Rows.Count} row(s).";
        }
        catch (Exception ex)
        {
            UIHelper.ShowServiceError(this, ex, "run report");
        }
    }

    private void Export_Click(object? sender, EventArgs e)
    {
        if (_grid.DataSource is not DataTable table || table.Rows.Count == 0)
        {
            UIHelper.ShowWarning(this, "Run a report first. There is nothing to save yet.", "No data");
            return;
        }

        using var dialog = new SaveFileDialog
        {
            Filter = "CSV file (*.csv)|*.csv",
            FileName = $"{_report.SelectedItem} {DateTime.Today:yyyy-MM-dd}.csv"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            File.WriteAllText(dialog.FileName, ToCsv(table), new UTF8Encoding(true));
            _status.Text = $"Saved {table.Rows.Count} row(s) to {dialog.FileName}.";
        }
        catch (Exception ex)
        {
            UIHelper.ShowError($"The file could not be saved: {ex.Message}", "Save failed");
        }
    }

    private static string ToCsv(DataTable table)
    {
        var csv = new StringBuilder();
        csv.AppendLine(string.Join(",", table.Columns.Cast<DataColumn>().Select(c => Quote(c.ColumnName))));
        foreach (DataRow row in table.Rows)
            csv.AppendLine(string.Join(",", row.ItemArray.Select(v => Quote(Convert.ToString(v)))));
        return csv.ToString();
    }

    // Wraps a value in quotes so commas and quotes inside it don't break the columns.
    private static string Quote(string? value) => "\"" + (value ?? "").Replace("\"", "\"\"") + "\"";
}
