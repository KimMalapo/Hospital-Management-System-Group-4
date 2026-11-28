using System.Data;
using BusinessLogic;
using Model;

namespace UI
{
    /// <summary>Patient Maintenance screen: add, update, delete patients.</summary>
    public partial class PatientMaintenanceUserControl : UserControl
    {
        private const string NotAdmitted = "Not Admitted";

        private PatientService? _patientService;
        private AdmissionService? _admissionService;
        private bool _databaseWarningShown;

        public event EventHandler<int>? DischargeRecordRequested;

        public PatientMaintenanceUserControl()
        {
            InitializeComponent();
            UITheme.Apply(this);
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtPatientID.ReadOnly = true;

            dgvPatients.SelectionChanged += dgvPatients_SelectionChanged;
        }

        private void PatientMaintenanceUserControl_Load(object sender, EventArgs e)
        {
            cmbGender.Items.Clear();
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });

            lblAdmissionStatusVal.Text = NotAdmitted;
            RefreshPatientTable();
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e) => RefreshPatientTable();

        private PatientService GetPatientService() =>
            _patientService ??= new PatientService(Config.GetConnectionString());

        private void RefreshPatientTable()
        {
            try
            {
                var connectionString = Config.GetConnectionString();
                _patientService ??= new PatientService(connectionString);
                _admissionService ??= new AdmissionService(connectionString);

                var searchTerm = txtSearch.Text.Trim();
                var showDeleted = chkShowDeleted.Checked;
                var patients = showDeleted
                    ? _patientService.GetDeleted(searchTerm)
                    : string.IsNullOrEmpty(searchTerm)
                        ? _patientService.GetAll()
                        : _patientService.Search(searchTerm);
                var activeAdmissions = _admissionService.GetActive();
                var admissionsByPatient = new Dictionary<int, DataRow>();

                foreach (DataRow admission in activeAdmissions.Rows)
                    admissionsByPatient[Convert.ToInt32(admission["PatientID"])] = admission;

                dgvPatients.Rows.Clear();
                foreach (DataRow patient in patients.Rows)
                    AddPatientRow(patient, admissionsByPatient);

                _databaseWarningShown = false;
            }
            catch (Exception ex)
            {
                Logger.Error("PatientMaintenanceUserControl.RefreshPatientTable failed", ex);
                dgvPatients.Rows.Clear();
                if (!_databaseWarningShown)
                {
                    UIHelper.ShowError("Unable to load patient records. Check the database connection and try again.", "Database error");
                    _databaseWarningShown = true;
                }
            }
        }

        private void AddPatientRow(DataRow patient, Dictionary<int, DataRow> admissionsByPatient)
        {
            var patientId = Convert.ToInt32(patient["PatientID"]);
            var age = patient.IsNull("DateOfBirth")
                ? "N/A"
                : UIHelper.CalculateAge(Convert.ToDateTime(patient["DateOfBirth"])).ToString();
            var status = NotAdmitted;
            var room = string.Empty;

            if (admissionsByPatient.TryGetValue(patientId, out var admission))
            {
                status = Convert.ToString(admission["Status"]) ?? status;
                room = admission.IsNull("RoomNo") ? string.Empty : Convert.ToString(admission["RoomNo"]) ?? string.Empty;
            }

            dgvPatients.Rows.Add(
                patientId,
                patient["FirstName"],
                patient["LastName"],
                age,
                patient.IsNull("Gender") ? string.Empty : patient["Gender"],
                patient.IsNull("Address") ? string.Empty : patient["Address"],
                patient.IsNull("ContactNumber") ? string.Empty : patient["ContactNumber"],
                status,
                room);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryBuildPatient(out var patient)) return;

            try
            {
                var patientId = GetPatientService().Create(patient);
                if (patientId <= 0)
                {
                    UIHelper.ShowWarning(this, "The patient record was not created. Please try again.", "Patient not saved");
                    return;
                }

                RefreshPatientTable();
                ClearPatientForm();
                UIHelper.ShowInfo(this, $"Patient record created. Patient ID: {patientId}.", "Patient saved");
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "adding patient");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPatientID.Text, out var patientId))
            {
                UIHelper.ShowWarning(this, "Select a patient record to update.", "Patient required");
                return;
            }
            if (!TryBuildPatient(out var patient)) return;
            patient.PatientID = patientId;

            try
            {
                if (GetPatientService().Update(patient) == 0)
                {
                    UIHelper.ShowWarning(this, "The patient record could not be found or was not updated.", "No changes saved");
                    RefreshPatientTable();
                    return;
                }

                RefreshPatientTable();
                ClearPatientForm();
                UIHelper.ShowInfo(this, "Patient record updated.", "Patient saved");
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "updating patient");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPatientID.Text, out var patientId))
            {
                UIHelper.ShowWarning(this, "Select a patient record to deactivate.", "Patient required");
                return;
            }

            if (MessageBox.Show(this, "Deactivate this patient record? Historical records will be retained.",
                    "Confirm patient deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                if (GetPatientService().Delete(patientId) == 0)
                {
                    UIHelper.ShowWarning(this, "The patient record could not be found or was not deactivated.", "No changes made");
                    RefreshPatientTable();
                    return;
                }

                RefreshPatientTable();
                ClearPatientForm();
                UIHelper.ShowInfo(this, "Patient record deactivated.", "Patient updated");
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "deactivating patient");
            }
        }

        // Switches between the normal list and the deactivated (soft-deleted) patients.
        private void chkShowDeleted_CheckedChanged(object? sender, EventArgs e)
        {
            var showDeleted = chkShowDeleted.Checked;
            btnAdd.Enabled = !showDeleted;
            btnUpdate.Enabled = !showDeleted;
            btnDelete.Enabled = !showDeleted;
            btnRestore.Enabled = showDeleted;
            ClearPatientForm();
            RefreshPatientTable();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPatientID.Text, out var patientId))
            {
                UIHelper.ShowWarning(this, "Select a deactivated patient to restore.", "Patient required");
                return;
            }

            try
            {
                if (GetPatientService().Restore(patientId) == 0)
                {
                    UIHelper.ShowWarning(this, "The patient could not be found or is already active.", "No changes made");
                    RefreshPatientTable();
                    return;
                }

                RefreshPatientTable();
                ClearPatientForm();
                UIHelper.ShowInfo(this, "Patient record restored.", "Patient updated");
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "restoring patient");
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearPatientForm();

        private void ClearPatientForm()
        {
            txtPatientID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            numAge.Value = 0;
            cmbGender.SelectedIndex = -1;
            txtAddress.Clear();
            txtContactNo.Clear();
            lblAdmissionStatusVal.Text = NotAdmitted;
        }

        private bool TryBuildPatient(out Model.Patient patient)
        {
            patient = new Model.Patient();
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                UIHelper.ShowWarning(this, "Enter both the patient's first and last name.", "Name required");
                return false;
            }

            patient = new Model.Patient
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                DateOfBirth = DateTime.Today.AddYears(-(int)numAge.Value),
                Gender = cmbGender.SelectedItem?.ToString(),
                Address = txtAddress.Text.Trim(),
                ContactNumber = txtContactNo.Text.Trim()
            };
            return true;
        }

        private void dgvPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvPatients.SelectedRows[0];
            txtPatientID.Text = row.Cells["colPatientID"].Value?.ToString();
            txtFirstName.Text = row.Cells["colFirstName"].Value?.ToString();
            txtLastName.Text = row.Cells["colLastName"].Value?.ToString();
            if (int.TryParse(row.Cells["colAge"].Value?.ToString(), out int age))
            {
                numAge.Value = Math.Max(numAge.Minimum, Math.Min(numAge.Maximum, age));
            }
            cmbGender.SelectedItem = row.Cells["colGender"].Value?.ToString();
            txtAddress.Text = row.Cells["colAddress"].Value?.ToString();
            txtContactNo.Text = row.Cells["colContact"].Value?.ToString();
            lblAdmissionStatusVal.Text = row.Cells["colAdmissionStatus"].Value?.ToString() ?? NotAdmitted;
        }

        private void RefreshPatientAdmissions(int patientId)
        {
            dgvAdmissionHistory.Rows.Clear();
            btnViewDischargeRecord.Enabled = false;

            try
            {
                _admissionService ??= new AdmissionService(Config.GetConnectionString());
                var history = _admissionService.GetByPatient(patientId);

                foreach (DataRow row in history.Rows)
                {
                    var admissionId = Convert.ToInt32(row["AdmissionID"]);
                    var admissionNo = Convert.ToString(row["AdmissionNo"]) ?? string.Empty;
                    var roomNo = row.IsNull("RoomNo") ? "-" : Convert.ToString(row["RoomNo"]) ?? "-";
                    var admitDate = row.IsNull("AdmitDate")
                        ? "-"
                        : TimeConversion.ToLocal(Convert.ToDateTime(row["AdmitDate"])).ToString("yyyy-MM-dd");
                    var status = Convert.ToString(row["Status"]) ?? string.Empty;

                    dgvAdmissionHistory.Rows.Add(admissionId, admissionNo, roomNo, admitDate, status);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PatientMaintenanceUserControl.RefreshPatientAdmissions failed", ex);
            }
        }

        private void dgvAdmissionHistory_SelectionChanged(object sender, EventArgs e)
        {
            btnViewDischargeRecord.Enabled = TryGetSelectedDischargedAdmissionId(out _);
        }

        private void dgvAdmissionHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                OpenSelectedDischargeRecord();
        }

        private void btnViewDischargeRecord_Click(object sender, EventArgs e)
        {
            OpenSelectedDischargeRecord();
        }

        private void OpenSelectedDischargeRecord()
        {
            if (!TryGetSelectedDischargedAdmissionId(out var admissionId))
            {
                MessageBox.Show(this, "Select a discharged admission from the patient's history to view its final record.",
                    "Discharged admission required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DischargeRecordRequested?.Invoke(this, admissionId);
        }

        private bool TryGetSelectedDischargedAdmissionId(out int admissionId)
        {
            admissionId = 0;
            if (dgvAdmissionHistory.SelectedRows.Count == 0)
                return false;

            var row = dgvAdmissionHistory.SelectedRows[0];
            var status = Convert.ToString(row.Cells["colHistStatus"].Value);
            if (!string.Equals(status, AdmissionStatus.Discharged, StringComparison.OrdinalIgnoreCase))
                return false;

            return int.TryParse(Convert.ToString(row.Cells["colHistAdmissionID"].Value), out admissionId) && admissionId > 0;
        }
    }
}
