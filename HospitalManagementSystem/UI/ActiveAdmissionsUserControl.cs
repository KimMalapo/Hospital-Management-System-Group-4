using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogic;

namespace UI;

public sealed partial class ActiveAdmissionsUserControl : UserControl
{
    private AdmissionService? _admissionService;

    public event EventHandler<ActiveAdmissionActionEventArgs>? ActionRequested;

    public ActiveAdmissionsUserControl()
    {
        InitializeComponent();
        UITheme.Apply(this);
        if (_status.Items.Count > 0)
            _status.SelectedIndex = 0;
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
            _emptyState.Text = "Admissions could not be loaded. Check the database connection and try again.";
        }
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
            _admissionsGrid.Visible = false;
            _emptyStatePanel.Visible = true;
            _emptyState.Text = "Admissions could not be loaded. Check the database connection and try again.";
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

public enum ActiveAdmissionAction
{
    AddCharges,
    ViewBill,
    Discharge
}

public sealed class ActiveAdmissionActionEventArgs(int admissionId, string admissionNo, ActiveAdmissionAction action) : EventArgs
{
    public int AdmissionId { get; } = admissionId;
    public string AdmissionNo { get; } = admissionNo;
    public ActiveAdmissionAction Action { get; } = action;
}