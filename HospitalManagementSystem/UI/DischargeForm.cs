using BusinessLogic;

namespace UI;

/// <summary>Discharge window: shows whether the patient can leave (bill generated and fully paid) and records the discharge.</summary>
public sealed class DischargeForm : Form
{
    private readonly int _admissionId;
    private readonly AdmissionService _admissions;

    private readonly Label _check = new();
    private readonly TextBox _summary = new();
    private readonly Button _discharge = new();
    private readonly Button _cancel = new();

    public DischargeForm(int admissionId, string admissionNo)
    {
        _admissionId = admissionId;
        _admissions = new AdmissionService(Config.GetConnectionString());

        Text = $"Discharge - admission {admissionNo}";
        Size = new Size(560, 400);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Font = new Font("Segoe UI", 10);
        BuildLayout();
        UITheme.Apply(this);
        Load += (_, _) => ShowCheck();
    }

    private void BuildLayout()
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(20) };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

        _check.Dock = DockStyle.Fill;
        _check.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        layout.Controls.Add(_check, 0, 0);

        layout.Controls.Add(new Label { Text = "Discharge summary (optional)", AutoSize = true }, 0, 1);

        _summary.Dock = DockStyle.Fill;
        _summary.Multiline = true;
        _summary.MaxLength = 1000;
        _summary.ScrollBars = ScrollBars.Vertical;
        layout.Controls.Add(_summary, 0, 2);

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
        _cancel.Name = "btnCancel";
        _cancel.Text = "Cancel";
        _cancel.Size = new Size(110, 34);
        _cancel.DialogResult = DialogResult.Cancel;
        buttons.Controls.Add(_cancel);
        _discharge.Name = "btnDischargePatient";
        _discharge.Text = "Discharge patient";
        _discharge.Size = new Size(160, 34);
        _discharge.Click += Discharge_Click;
        buttons.Controls.Add(_discharge);
        layout.Controls.Add(buttons, 0, 3);

        Controls.Add(layout);
        CancelButton = _cancel;
    }

    // The button is only usable when the bill is generated and fully paid.
    private void ShowCheck()
    {
        try
        {
            var check = _admissions.CheckDischarge(_admissionId);
            _check.Text = check.Message;
            _check.ForeColor = check.CanDischarge ? Color.FromArgb(22, 101, 52) : Color.FromArgb(153, 27, 27);
            _discharge.Enabled = check.CanDischarge;
        }
        catch (Exception ex)
        {
            _discharge.Enabled = false;
            UIHelper.HandleDatabaseError(ex, "checking the discharge");
        }
    }

    private void Discharge_Click(object? sender, EventArgs e)
    {
        if (MessageBox.Show(this, "Discharge this patient? This cannot be undone.", "Confirm discharge",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            _admissions.Discharge(_admissionId, _summary.Text);
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            UIHelper.ShowServiceError(this, ex, "discharge");
            ShowCheck();
        }
    }
}
