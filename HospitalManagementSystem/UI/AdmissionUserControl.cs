using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI;

public sealed partial class AdmissionUserControl : UserControl
{
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
        InitializeComponent();
        UITheme.Apply(this);
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

    private void ClearButton_Click(object? sender, EventArgs e)
    {
        ClearForm();
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