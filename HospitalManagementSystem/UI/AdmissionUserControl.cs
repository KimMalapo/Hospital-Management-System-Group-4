using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI;

/// <summary>Admit Patient screen. Controls are made in code.</summary>
public sealed class AdmissionUserControl : UserControl
{
    private readonly ErrorProvider _errors = new();
    private readonly System.Windows.Forms.Timer _patientSearchTimer = new() { Interval = 300 };
    private readonly TextBox _patientSearch = new();
    private readonly DataGridView _patientsGrid = new();
    private readonly Label _selectedPatient = new();
    private readonly Label _status = new();
    private readonly ComboBox _rooms = new();
    private readonly ComboBox _departments = new();
    private readonly ComboBox _diagnoses = new();
    private readonly TextBox _physician = new();
    private readonly TextBox _reason = new();
    private readonly DateTimePicker _admitDate = new();
    private PatientService? _patientService;
    private RoomService? _roomService;
    private LookupService? _lookupService;
    private AdmissionService? _admissionService;
    private int? _selectedPatientId;
    private bool _loadingPatients;

    private const string NoPatientSelectedText = "No patient selected";

    public event EventHandler<AdmissionCreatedEventArgs>? AdmissionCreated;

    public AdmissionUserControl()
    {
        Dock = DockStyle.Fill;
        AutoScroll = true;
        _errors.ContainerControl = this;
        _errors.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        BuildLayout();
        Load += AdmissionUserControl_Load;
        _patientSearch.TextChanged += PatientSearch_TextChanged;
        _patientSearchTimer.Tick += PatientSearchTimer_Tick;
        _patientsGrid.SelectionChanged += PatientsGrid_SelectionChanged;
    }

    public void FocusPatient(int? patientId = null)
    {
        if (patientId is > 0)
        {
            _patientSearch.Text = patientId.Value.ToString();
            _patientSearchTimer.Stop();
            LoadPatients(_patientSearch.Text);
            foreach (DataGridViewRow row in _patientsGrid.Rows)
            {
                if (!int.TryParse(Convert.ToString(row.Cells["PatientID"].Value), out var rowPatientId) ||
                    rowPatientId != patientId.Value) continue;
                row.Selected = true;
                _patientsGrid.CurrentCell = row.Cells["PatientName"];
                break;
            }
        }
        else
        {
            _patientSearch.Focus();
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _patientSearchTimer.Dispose();
            _errors.Dispose();
        }
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
            RowCount = 2,
            BackColor = BackColor
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 76));
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
        heading.Controls.Add(CreateLabel("Admit a patient", new Font("Segoe UI", 20, FontStyle.Bold),
            Color.FromArgb(30, 41, 59), ContentAlignment.MiddleLeft), 0, 0);
        heading.Controls.Add(CreateLabel("Choose a registered patient, assign a room, and record the admission details.",
            new Font("Segoe UI", 9.5F), Color.FromArgb(100, 116, 139), ContentAlignment.MiddleLeft), 0, 1);
        root.Controls.Add(heading, 0, 0);

        var content = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 1,
            Margin = Padding.Empty
        };
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46));
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 18));
        content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54));
        root.Controls.Add(content, 0, 1);

        var patientGroup = CreateCard();
        var patientLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 5,
            Margin = Padding.Empty
        };
        patientLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        patientLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
        patientLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        patientLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        patientLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        patientLayout.Controls.Add(CreateSectionHeading("01", "Select patient", "Search by patient ID or name"), 0, 0);
        patientLayout.Controls.Add(CreateLabel("Patient search", new Font("Segoe UI", 9, FontStyle.Bold),
            Color.FromArgb(71, 85, 105), ContentAlignment.BottomLeft, new Padding(0, 3, 0, 2)), 0, 1);
        _patientSearch.Dock = DockStyle.Fill;
        _patientSearch.Font = new Font("Segoe UI", 10);
        _patientSearch.PlaceholderText = "Enter patient ID or name";
        _patientSearch.Margin = new Padding(0, 2, 0, 8);
        patientLayout.Controls.Add(_patientSearch, 0, 2);
        ConfigurePatientsGrid();
        patientLayout.Controls.Add(_patientsGrid, 0, 3);
        _selectedPatient.Text = NoPatientSelectedText;
        _selectedPatient.Dock = DockStyle.Fill;
        _selectedPatient.TextAlign = ContentAlignment.MiddleLeft;
        _selectedPatient.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _selectedPatient.ForeColor = Color.FromArgb(30, 64, 175);
        _selectedPatient.BackColor = Color.FromArgb(239, 246, 255);
        _selectedPatient.Padding = new Padding(12, 0, 12, 0);
        _selectedPatient.Margin = new Padding(0, 10, 0, 0);
        patientLayout.Controls.Add(_selectedPatient, 0, 4);
        patientGroup.Controls.Add(patientLayout);

        var detailsGroup = CreateCard();
        var details = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 9,
            Margin = Padding.Empty
        };
        details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31));
        details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 106));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
        details.RowStyles.Add(new RowStyle(SizeType.Absolute, 38));
        details.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var detailsHeading = CreateSectionHeading("02", "Admission details", "Required fields are marked with an asterisk");
        details.Controls.Add(detailsHeading, 0, 0);
        details.SetColumnSpan(detailsHeading, 2);

        ConfigureLookupCombo(_rooms);
        ConfigureLookupCombo(_departments);
        ConfigureLookupCombo(_diagnoses);
        AddField(details, 1, "Available room *", _rooms);
        AddField(details, 2, "Department *", _departments);
        AddField(details, 3, "Diagnosis *", _diagnoses);

        _physician.Dock = DockStyle.Fill;
        _physician.MaxLength = 200;
        _physician.PlaceholderText = "Enter physician name";
        _physician.Margin = new Padding(3, 8, 3, 8);
        AddField(details, 4, "Physician *", _physician);

        _reason.Dock = DockStyle.Fill;
        _reason.Multiline = true;
        _reason.MaxLength = 500;
        _reason.ScrollBars = ScrollBars.Vertical;
        _reason.PlaceholderText = "Add context for this admission (optional)";
        _reason.Margin = new Padding(3, 8, 3, 8);
        AddField(details, 5, "Reason", _reason);

        _admitDate.Dock = DockStyle.Fill;
        _admitDate.Format = DateTimePickerFormat.Custom;
        _admitDate.CustomFormat = "MMM dd, yyyy  |  hh:mm tt";
        _admitDate.ShowUpDown = false;
        _admitDate.Margin = new Padding(3, 8, 3, 8);
        AddField(details, 6, "Admission date *", _admitDate);

        _status.Dock = DockStyle.Fill;
        _status.ForeColor = Color.Firebrick;
        _status.Font = new Font("Segoe UI", 9);
        _status.TextAlign = ContentAlignment.MiddleLeft;
        details.Controls.Add(_status, 0, 7);
        details.SetColumnSpan(_status, 2);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false,
            Padding = new Padding(0, 8, 0, 0),
            Margin = Padding.Empty
        };
        var admitButton = new Button
        {
            Text = "Create admission",
            Width = 170,
            Height = 38,
            BackColor = Color.FromArgb(37, 99, 235),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
            Cursor = Cursors.Hand,
            Margin = new Padding(8, 0, 0, 0),
            UseVisualStyleBackColor = false
        };
        admitButton.FlatAppearance.BorderSize = 0;
        admitButton.Click += AdmitButton_Click;
        var clearButton = new Button
        {
            Text = "Clear form",
            Width = 105,
            Height = 38,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.White,
            ForeColor = Color.FromArgb(71, 85, 105),
            Font = new Font("Segoe UI", 9.5F),
            Cursor = Cursors.Hand,
            Margin = new Padding(8, 0, 0, 0)
        };
        clearButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        clearButton.Click += (_, _) => ClearForm();
        buttons.Controls.Add(admitButton);
        buttons.Controls.Add(clearButton);
        details.Controls.Add(buttons, 0, 8);
        details.SetColumnSpan(buttons, 2);
        detailsGroup.Controls.Add(details);

        content.Controls.Add(patientGroup, 0, 0);
        content.Controls.Add(detailsGroup, 2, 0);
        Controls.Add(root);
    }

    private static Control CreateSectionHeading(string step, string title, string description)
    {
        var heading = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 2,
            Margin = Padding.Empty
        };
        heading.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38));
        heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        heading.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
        heading.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var stepBadge = CreateLabel(step, new Font("Segoe UI", 9, FontStyle.Bold),
            Color.FromArgb(37, 99, 235), ContentAlignment.MiddleCenter, new Padding(0, 0, 8, 0));
        stepBadge.BackColor = Color.FromArgb(239, 246, 255);
        heading.Controls.Add(stepBadge, 0, 0);
        heading.SetRowSpan(stepBadge, 2);
        heading.Controls.Add(CreateLabel(title, new Font("Segoe UI", 11, FontStyle.Bold),
            Color.FromArgb(30, 41, 59), ContentAlignment.MiddleLeft, Padding.Empty), 1, 0);
        heading.Controls.Add(CreateLabel(description, new Font("Segoe UI", 8.5F),
            Color.FromArgb(100, 116, 139), ContentAlignment.MiddleLeft, Padding.Empty), 1, 1);
        return heading;
    }

    private void ConfigurePatientsGrid()
    {
        _patientsGrid.Dock = DockStyle.Fill;
        _patientsGrid.ReadOnly = true;
        _patientsGrid.AllowUserToAddRows = false;
        _patientsGrid.AllowUserToDeleteRows = false;
        _patientsGrid.AllowUserToResizeRows = false;
        _patientsGrid.AutoGenerateColumns = false;
        _patientsGrid.MultiSelect = false;
        _patientsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _patientsGrid.RowHeadersVisible = false;
        _patientsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _patientsGrid.BackgroundColor = Color.White;
        _patientsGrid.BorderStyle = BorderStyle.None;
        _patientsGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        _patientsGrid.GridColor = Color.FromArgb(226, 232, 240);
        _patientsGrid.EnableHeadersVisualStyles = false;
        _patientsGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        _patientsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        _patientsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
        _patientsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        _patientsGrid.ColumnHeadersHeight = 38;
        _patientsGrid.DefaultCellStyle.BackColor = Color.White;
        _patientsGrid.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
        _patientsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        _patientsGrid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
        _patientsGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
        _patientsGrid.RowTemplate.Height = 36;
        _patientsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "PatientID", DataPropertyName = "PatientID", HeaderText = "Patient ID", FillWeight = 22 });
        _patientsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "PatientName", DataPropertyName = "PatientName", HeaderText = "Patient", FillWeight = 58 });
        _patientsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Age", DataPropertyName = "Age", HeaderText = "Age", FillWeight = 20 });
    }

    private static void ConfigureLookupCombo(ComboBox combo)
    {
        combo.Dock = DockStyle.Fill;
        combo.DropDownStyle = ComboBoxStyle.DropDownList;
        combo.Font = new Font("Segoe UI", 10);
        combo.Margin = new Padding(3, 8, 3, 8);
    }

    private static void AddField(TableLayoutPanel layout, int row, string label, Control input)
    {
        layout.Controls.Add(CreateLabel(label, new Font("Segoe UI", 9, FontStyle.Bold),
            Color.FromArgb(71, 85, 105), ContentAlignment.MiddleLeft, new Padding(0, 4, 8, 4)), 0, row);
        layout.Controls.Add(input, 1, row);
    }

    private static Label CreateLabel(string text, Font font, Color foreColor, ContentAlignment textAlign, Padding? margin = null)
    {
        var label = new Label
        {
            Text = text,
            Dock = DockStyle.Fill,
            Font = font,
            ForeColor = foreColor,
            TextAlign = textAlign
        };
        if (margin.HasValue) label.Margin = margin.Value;
        return label;
    }

    private static Panel CreateCard() => new()
    {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        BorderStyle = BorderStyle.FixedSingle,
        Padding = new Padding(18),
        Margin = Padding.Empty
    };

    private void AdmissionUserControl_Load(object? sender, EventArgs e)
    {
        try
        {
            var connectionString = Config.GetConnectionString();
            _patientService = new PatientService(connectionString);
            _roomService = new RoomService(connectionString);
            _lookupService = new LookupService(connectionString);
            _admissionService = new AdmissionService(connectionString);
            LoadRooms();
            LoadLookupOptions(_departments, "DepartmentTypes");
            LoadLookupOptions(_diagnoses, "DiagnosisCodes");
            LoadPatients(string.Empty);
        }
        catch (Exception ex)
        {
            Logger.Error("AdmissionUserControl.Load failed", ex);
            _status.Text = "Admission options could not be loaded. Check the database connection and try again.";
        }
    }

    private void LoadRooms()
    {
        var options = new List<RoomOption>();
        var rows = _roomService!.GetAll();
        foreach (DataRow row in rows.Rows)
        {
            int occupiedBeds = rows.Columns.Contains("OccupiedBeds") && row["OccupiedBeds"] != DBNull.Value
                ? Convert.ToInt32(row["OccupiedBeds"]) : 0;
            int capacity = rows.Columns.Contains("Capacity") && row["Capacity"] != DBNull.Value
                ? Convert.ToInt32(row["Capacity"]) : 1;
            if (occupiedBeds >= capacity ||
                (rows.Columns.Contains("Status") &&
                 !string.Equals(Convert.ToString(row["Status"]), "Available", StringComparison.OrdinalIgnoreCase) &&
                 !string.Equals(Convert.ToString(row["Status"]), "Occupied", StringComparison.OrdinalIgnoreCase)))
                continue;
            if (rows.Columns.Contains("IsActive") &&
                (row.IsNull("IsActive") || !Convert.ToBoolean(row["IsActive"])))
                continue;

            options.Add(new RoomOption(
                Convert.ToString(row["RoomNo"]) ?? string.Empty,
                Convert.ToString(row["RoomTypeName"]) ?? Convert.ToString(row["RoomType"]) ?? string.Empty,
                Convert.ToDecimal(row["Rate"]),
                occupiedBeds,
                capacity));
        }

        _rooms.DataSource = options;
        _rooms.DisplayMember = nameof(RoomOption.DisplayText);
        _rooms.ValueMember = nameof(RoomOption.RoomNo);
        _rooms.SelectedIndex = -1;
    }

    private void LoadLookupOptions(ComboBox combo, string key)
    {
        var options = new List<LookupOption>();
        foreach (var item in _lookupService!.GetAll(key))
        {
            if (item.IsDeleted || !string.Equals(item.Status, "Active", StringComparison.OrdinalIgnoreCase))
                continue;
            options.Add(new LookupOption(item.Code ?? string.Empty, item.Name ?? item.Code ?? string.Empty));
        }

        combo.DataSource = options;
        combo.DisplayMember = nameof(LookupOption.DisplayText);
        combo.ValueMember = nameof(LookupOption.Code);
        combo.SelectedIndex = -1;
    }

    private void PatientSearch_TextChanged(object? sender, EventArgs e)
    {
        _patientSearchTimer.Stop();
        _patientSearchTimer.Start();
    }

    private void PatientSearchTimer_Tick(object? sender, EventArgs e)
    {
        _patientSearchTimer.Stop();
        LoadPatients(_patientSearch.Text.Trim());
    }

    private void LoadPatients(string searchTerm)
    {
        if (_patientService == null) return;

        try
        {
            _loadingPatients = true;
            _selectedPatientId = null;
            _selectedPatient.Text = NoPatientSelectedText;
            var source = _patientService.GetPatients(searchTerm);

            // Patients who are already admitted can't be admitted again, so leave them out of the list.
            var admittedIds = _admissionService == null
                ? new HashSet<int>()
                : _admissionService.GetActive().AsEnumerable().Select(r => Convert.ToInt32(r["PatientID"])).ToHashSet();

            var patients = new DataTable();
            patients.Columns.Add("PatientID", typeof(int));
            patients.Columns.Add("PatientName", typeof(string));
            patients.Columns.Add("Age", typeof(string));

            var hiddenCount = 0;
            foreach (DataRow row in source.Rows)
            {
                if (admittedIds.Contains(Convert.ToInt32(row["PatientID"])))
                {
                    hiddenCount++;
                    continue;
                }

                var dateOfBirth = row["DateOfBirth"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["DateOfBirth"]);
                var age = dateOfBirth.HasValue ? CalculateAge(dateOfBirth.Value).ToString() : "N/A";
                patients.Rows.Add(Convert.ToInt32(row["PatientID"]),
                    $"{row["LastName"]}, {row["FirstName"]}", age);
            }

            _patientsGrid.DataSource = patients;
            _patientsGrid.ClearSelection();
            _status.Text = patients.Rows.Count > 0 ? string.Empty
                : hiddenCount > 0 ? "The matching patients are already admitted."
                : "No registered patients match this search.";
        }
        catch (Exception ex)
        {
            Logger.Error("AdmissionUserControl.LoadPatients failed", ex);
            _status.Text = "Patient records could not be loaded. Check the database connection and try again.";
        }
        finally
        {
            _loadingPatients = false;
        }
    }

    private void PatientsGrid_SelectionChanged(object? sender, EventArgs e)
    {
        if (_loadingPatients || _patientsGrid.CurrentRow?.DataBoundItem is not DataRowView row)
            return;

        _selectedPatientId = Convert.ToInt32(row["PatientID"]);
        _selectedPatient.Text = $"#{_selectedPatientId} — {row["PatientName"]} ({row["Age"]})";
        _errors.SetError(_patientsGrid, string.Empty);
        _status.Text = string.Empty;
    }

    private void AdmitButton_Click(object? sender, EventArgs e)
    {
        if (!ValidateAdmission()) return;

        try
        {
            var admission = _admissionService!.Admit(
                _selectedPatientId!.Value,
                Convert.ToString(_rooms.SelectedValue),
                Convert.ToString(_departments.SelectedValue),
                Convert.ToString(_diagnoses.SelectedValue),
                _physician.Text,
                _reason.Text,
                _admitDate.Value);

            MessageBox.Show(this, $"Admission {admission.AdmissionNo} was created successfully.",
                "Admission saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var admissionId = admission.AdmissionID;
            var admissionNo = admission.AdmissionNo ?? string.Empty;
            ClearForm();
            AdmissionCreated?.Invoke(this, new AdmissionCreatedEventArgs(admissionId, admissionNo));
        }
        catch (ValidationException ex)
        {
            _status.Text = ex.Message;
            SetValidationError(ex.Field, ex.Message);
        }
        catch (Exception ex)
        {
            Logger.Error("AdmissionUserControl.Admit failed", ex);
            _status.Text = "The admission could not be saved. Check the details and try again.";
        }
    }

    private bool ValidateAdmission()
    {
        _errors.Clear();
        var valid = true;

        void Reject(Control control, string message)
        {
            _errors.SetError(control, message);
            valid = false;
        }

        if (_selectedPatientId is null) Reject(_patientsGrid, "Select a registered patient.");
        if (_rooms.SelectedValue is not string) Reject(_rooms, "Select an available room.");
        if (_departments.SelectedValue is not string) Reject(_departments, "Select a department.");
        if (_diagnoses.SelectedValue is not string) Reject(_diagnoses, "Select a diagnosis.");
        if (string.IsNullOrWhiteSpace(_physician.Text)) Reject(_physician, "Enter the attending physician.");
        if (_admitDate.Value > DateTime.Now.AddMinutes(5)) Reject(_admitDate, "The admit date cannot be in the future.");

        _status.Text = valid ? string.Empty : "Complete the required fields marked with an error.";
        return valid;
    }

    private void SetValidationError(string? field, string message)
    {
        Control? control = field?.ToLowerInvariant() switch
        {
            "patient" => _patientsGrid,
            "room" => _rooms,
            "department" => _departments,
            "diagnosis" => _diagnoses,
            "attending physician" => _physician,
            "admit date" => _admitDate,
            _ => null
        };
        if (control != null)
            _errors.SetError(control, message);
    }

    private void ClearForm()
    {
        _errors.Clear();
        _patientSearchTimer.Stop();
        _patientSearch.Clear();
        _patientsGrid.ClearSelection();
        _selectedPatientId = null;
        _selectedPatient.Text = NoPatientSelectedText;
        _rooms.SelectedIndex = -1;
        _departments.SelectedIndex = -1;
        _diagnoses.SelectedIndex = -1;
        _physician.Clear();
        _reason.Clear();
        _admitDate.Value = DateTime.Now;
        _status.Text = string.Empty;
        LoadPatients(string.Empty);
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }

    private sealed record RoomOption(string RoomNo, string RoomTypeName, decimal Rate, int OccupiedBeds, int Capacity)
    {
        public string DisplayText => $"{RoomNo} — {RoomTypeName} — {OccupiedBeds}/{Capacity} occupied, {Capacity - OccupiedBeds} beds free — ₱{Rate:#,##0.00}";
    }

    private sealed record LookupOption(string Code, string Name)
    {
        public string DisplayText => $"{Name} ({Code})";
    }
}

/// <summary>Says a patient was just admitted.</summary>
public sealed class AdmissionCreatedEventArgs(int admissionId, string admissionNo) : EventArgs
{
    public int AdmissionId { get; } = admissionId;
    public string AdmissionNo { get; } = admissionNo;
}
