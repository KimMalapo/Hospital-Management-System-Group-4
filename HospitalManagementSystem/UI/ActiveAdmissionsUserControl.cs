using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI;

/// <summary>Active Admissions screen: who's admitted now, with buttons to add charges, view the bill or discharge.</summary>
public sealed class ActiveAdmissionsUserControl : UserControl
{
    private readonly TextBox _search = new();
    private readonly ComboBox _status = new();
    private readonly DataGridView _admissionsGrid = new();
    private readonly Label _emptyState = new();
    private readonly Panel _emptyStatePanel = new();
    private readonly System.Windows.Forms.Timer _searchTimer = new() { Interval = 300 };
    private AdmissionService? _admissionService;

    private const string LoadFailedText = "Admissions could not be loaded. Check the database connection and try again.";

    public event EventHandler<ActiveAdmissionActionEventArgs>? ActionRequested;

    public ActiveAdmissionsUserControl()
    {
        Dock = DockStyle.Fill;
        BuildLayout();
        Load += ActiveAdmissionsUserControl_Load;
        _search.TextChanged += Search_TextChanged;
        _status.SelectedIndexChanged += Status_SelectedIndexChanged;
        _searchTimer.Tick += SearchTimer_Tick;
        _admissionsGrid.CellContentClick += AdmissionsGrid_CellContentClick;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            _searchTimer.Dispose();
        base.Dispose(disposing);
    }

    public void RefreshAdmissions()
    {
        if (_admissionService == null) return;

        try
        {
            var term = _search.Text.Trim();
            var statusFilter = _status.SelectedItem?.ToString() ?? "Admitted";
            var rows = string.IsNullOrEmpty(term) && statusFilter == "Admitted"
                ? _admissionService.GetActive()
                : _admissionService.Search(term);
            var filtered = rows.Clone();

            foreach (DataRow row in rows.Rows)
            {
                var status = Convert.ToString(row["Status"]);
                if (statusFilter != "All statuses" &&
                    !string.Equals(status, statusFilter, StringComparison.OrdinalIgnoreCase))
                    continue;
                filtered.ImportRow(row);
            }

            _admissionsGrid.DataSource = filtered;
            var hasRows = filtered.Rows.Count > 0;
            _admissionsGrid.Visible = hasRows;
            _emptyStatePanel.Visible = !hasRows;
            _emptyState.Text = statusFilter == "Admitted" && string.IsNullOrWhiteSpace(term)
                ? "No active admissions yet."
                : "No admissions match your search and status filter.";
        }
        catch (Exception ex)
        {
            Logger.Error("ActiveAdmissionsUserControl.RefreshAdmissions failed", ex);
            _admissionsGrid.DataSource = null;
            _admissionsGrid.Visible = false;
            _emptyStatePanel.Visible = true;
            _emptyState.Text = LoadFailedText;
        }
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
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 94));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var heading = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Margin = Padding.Empty
        };
        heading.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
        heading.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        heading.Controls.Add(new Label
        {
            Text = "Active admissions",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 41, 59),
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);
        heading.Controls.Add(new Label
        {
            Text = "Review current and previous stays, then continue with the next care or billing step.",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 9.5F),
            ForeColor = Color.FromArgb(100, 116, 139),
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 1);
        root.Controls.Add(heading, 0, 0);

        var toolbarCard = CreateCard(new Padding(16, 10, 16, 10), new Padding(0, 0, 0, 14));
        var toolbar = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 2,
            Margin = Padding.Empty
        };
        toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
        toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85));
        toolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190));
        toolbar.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
        toolbar.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        AddToolbarLabel(toolbar, "Search admissions", 0, 0);
        AddToolbarLabel(toolbar, "Status", 2, 0);
        _search.Dock = DockStyle.Fill;
        _search.Font = new Font("Segoe UI", 10);
        _search.PlaceholderText = "Patient name, room, or admission number";
        _search.Margin = new Padding(0, 3, 14, 0);
        toolbar.Controls.Add(_search, 0, 1);
        toolbar.SetColumnSpan(_search, 2);
        _status.Dock = DockStyle.Fill;
        _status.DropDownStyle = ComboBoxStyle.DropDownList;
        _status.Font = new Font("Segoe UI", 9.5F);
        _status.Items.AddRange(new object[] { "Admitted", "Discharged", "Cancelled", "All statuses" });
        _status.SelectedIndex = 0;
        _status.Margin = new Padding(0, 3, 0, 0);
        toolbar.Controls.Add(_status, 3, 1);
        toolbarCard.Controls.Add(toolbar);
        root.Controls.Add(toolbarCard, 0, 1);

        var gridCard = CreateCard(new Padding(12), Padding.Empty);
        var gridContainer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
        ConfigureGrid();
        _admissionsGrid.Dock = DockStyle.Fill;
        _emptyStatePanel.Dock = DockStyle.Fill;
        _emptyStatePanel.BackColor = Color.White;
        _emptyStatePanel.Visible = true;
        var emptyLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(20)
        };
        emptyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        emptyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
        emptyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        _emptyState.Text = "No active admissions yet.";
        _emptyState.Dock = DockStyle.Fill;
        _emptyState.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        _emptyState.ForeColor = Color.FromArgb(71, 85, 105);
        _emptyState.TextAlign = ContentAlignment.MiddleCenter;
        emptyLayout.Controls.Add(_emptyState, 0, 1);
        _emptyStatePanel.Controls.Add(emptyLayout);
        gridContainer.Controls.Add(_admissionsGrid);
        gridContainer.Controls.Add(_emptyStatePanel);
        gridCard.Controls.Add(gridContainer);
        root.Controls.Add(gridCard, 0, 2);
        Controls.Add(root);
    }

    private static Panel CreateCard(Padding padding, Padding margin) => new()
    {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        BorderStyle = BorderStyle.FixedSingle,
        Padding = padding,
        Margin = margin
    };

    private static DataGridViewButtonColumn CreateActionColumn(string name, string text, float fillWeight) => new()
    {
        Name = name,
        Text = text,
        UseColumnTextForButtonValue = true,
        HeaderText = "",
        FillWeight = fillWeight,
        FlatStyle = FlatStyle.Flat,
        DefaultCellStyle =
        {
            BackColor = Color.FromArgb(239, 246, 255),
            ForeColor = Color.FromArgb(37, 99, 235),
            SelectionBackColor = Color.FromArgb(219, 234, 254),
            SelectionForeColor = Color.FromArgb(30, 64, 175)
        }
    };

    private static void AddToolbarLabel(TableLayoutPanel layout, string text, int column, int row)
    {
        layout.Controls.Add(new Label
        {
            Text = text,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
            ForeColor = Color.FromArgb(71, 85, 105),
            TextAlign = ContentAlignment.MiddleLeft,
            Margin = Padding.Empty
        }, column, row);
    }

    private void ConfigureGrid()
    {
        _admissionsGrid.ReadOnly = true;
        _admissionsGrid.AllowUserToAddRows = false;
        _admissionsGrid.AllowUserToDeleteRows = false;
        _admissionsGrid.AllowUserToResizeRows = false;
        _admissionsGrid.AutoGenerateColumns = false;
        _admissionsGrid.MultiSelect = false;
        _admissionsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _admissionsGrid.RowHeadersVisible = false;
        _admissionsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _admissionsGrid.BackgroundColor = Color.White;
        _admissionsGrid.BorderStyle = BorderStyle.None;
        _admissionsGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        _admissionsGrid.GridColor = Color.FromArgb(226, 232, 240);
        _admissionsGrid.EnableHeadersVisualStyles = false;
        _admissionsGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        _admissionsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        _admissionsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
        _admissionsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        _admissionsGrid.ColumnHeadersHeight = 42;
        _admissionsGrid.DefaultCellStyle.BackColor = Color.White;
        _admissionsGrid.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
        _admissionsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        _admissionsGrid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
        _admissionsGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
        _admissionsGrid.RowTemplate.Height = 40;
        _admissionsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 253);
        _admissionsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "AdmissionID", DataPropertyName = "AdmissionID", Visible = false });
        _admissionsGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "AdmissionNo", HeaderText = "Admission No.", FillWeight = 105 });
        _admissionsGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PatientName", HeaderText = "Patient", FillWeight = 145 });
        _admissionsGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomNo", HeaderText = "Room", FillWeight = 55 });
        _admissionsGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiagnosisName", HeaderText = "Diagnosis", FillWeight = 105 });
        _admissionsGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "AdmitDate", HeaderText = "Admitted", FillWeight = 85, DefaultCellStyle = new DataGridViewCellStyle { Format = "MMM dd, yyyy" } });
        _admissionsGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DaysStayed", HeaderText = "Days", FillWeight = 40 });
        _admissionsGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RunningTotal", HeaderText = "Running Total", FillWeight = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "₱#,##0.00", Alignment = DataGridViewContentAlignment.MiddleRight } });
        _admissionsGrid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Status", FillWeight = 70 });
        _admissionsGrid.Columns.Add(CreateActionColumn("AddCharges", "Add charges", 80));
        _admissionsGrid.Columns.Add(CreateActionColumn("ViewBill", "View bill", 65));
        _admissionsGrid.Columns.Add(CreateActionColumn("Discharge", "Discharge", 70));
    }

    private void ActiveAdmissionsUserControl_Load(object? sender, EventArgs e)
    {
        try
        {
            _admissionService = new AdmissionService(Config.GetConnectionString());
            RefreshAdmissions();
        }
        catch (Exception ex)
        {
            Logger.Error("ActiveAdmissionsUserControl.Load failed", ex);
            _emptyState.Visible = true;
            _emptyState.Text = LoadFailedText;
        }
    }

    private void Search_TextChanged(object? sender, EventArgs e)
    {
        _searchTimer.Stop();
        _searchTimer.Start();
    }

    private void SearchTimer_Tick(object? sender, EventArgs e)
    {
        _searchTimer.Stop();
        RefreshAdmissions();
    }

    private void Status_SelectedIndexChanged(object? sender, EventArgs e) => RefreshAdmissions();

    private void AdmissionsGrid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || _admissionsGrid.Rows[e.RowIndex].DataBoundItem is not DataRowView row)
            return;

        var action = _admissionsGrid.Columns[e.ColumnIndex].Name switch
        {
            "AddCharges" => ActiveAdmissionAction.AddCharges,
            "ViewBill" => ActiveAdmissionAction.ViewBill,
            "Discharge" => ActiveAdmissionAction.Discharge,
            _ => (ActiveAdmissionAction?)null
        };
        if (action is null) return;

        ActionRequested?.Invoke(this, new ActiveAdmissionActionEventArgs(
            Convert.ToInt32(row["AdmissionID"]),
            Convert.ToString(row["AdmissionNo"]) ?? string.Empty,
            action.Value));
    }
}

/// <summary>The buttons on each row of the list.</summary>
public enum ActiveAdmissionAction
{
    AddCharges,
    ViewBill,
    Discharge
}

/// <summary>Which button was clicked, and for which admission.</summary>
public sealed class ActiveAdmissionActionEventArgs(int admissionId, string admissionNo, ActiveAdmissionAction action) : EventArgs
{
    public int AdmissionId { get; } = admissionId;
    public string AdmissionNo { get; } = admissionNo;
    public ActiveAdmissionAction Action { get; } = action;
}
