using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI
{
    public partial class PatientMaintenanceUserControl : UserControl
    {
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
            btnViewDischargeRecord.Enabled = false;
        }

        private void PatientMaintenanceUserControl_Load(object sender, EventArgs e)
        {
            // Populate Gender ComboBox defaults
            cmbGender.Items.Clear();
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.Items.Add("Other");

            lblAdmissionStatusVal.Text = "Not Admitted";
            RefreshPatientTable();
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            RefreshPatientTable();
        }

        private void RefreshPatientTable()
        {
            try
            {
                var connectionString = Config.GetConnectionString();
                _patientService ??= new PatientService(connectionString);
                _admissionService ??= new AdmissionService(connectionString);

                var searchTerm = txtSearch.Text.Trim();
                var patients = string.IsNullOrEmpty(searchTerm)
                    ? _patientService.GetAll()
                    : _patientService.Search(searchTerm);
                var activeAdmissions = _admissionService.GetActive();
                var admissionsByPatient = new Dictionary<int, DataRow>();

                foreach (DataRow admission in activeAdmissions.Rows)
                    admissionsByPatient[Convert.ToInt32(admission["PatientID"])] = admission;

                dgvPatients.Rows.Clear();
                foreach (DataRow patient in patients.Rows)
                {
                    var patientId = Convert.ToInt32(patient["PatientID"]);
                    var age = patient.IsNull("DateOfBirth")
                        ? "N/A"
                        : CalculateAge(Convert.ToDateTime(patient["DateOfBirth"])).ToString();
                    var status = "Not Admitted";
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

                _databaseWarningShown = false;
            }
            catch (Exception ex)
            {
                Logger.Error("PatientMaintenanceUserControl.RefreshPatientTable failed", ex);
                dgvPatients.Rows.Clear();
                if (!_databaseWarningShown)
                {
                    MessageBox.Show("Unable to load patient records. Check the database connection and try again.",
                        "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _databaseWarningShown = true;
                }
            }
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryBuildPatient(out var patient)) return;

            try
            {
                _patientService ??= new PatientService(Config.GetConnectionString());
                var patientId = _patientService.Create(patient);
                if (patientId <= 0)
                {
                    MessageBox.Show(this, "The patient record was not created. Please try again.", "Patient not saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RefreshPatientTable();
                ClearPatientForm();
                MessageBox.Show(this, $"Patient record created. Patient ID: {patientId}.", "Patient saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(this, "Select a patient record to update.", "Patient required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!TryBuildPatient(out var patient)) return;
            patient.PatientID = patientId;

            try
            {
                _patientService ??= new PatientService(Config.GetConnectionString());
                if (_patientService.Update(patient) == 0)
                {
                    MessageBox.Show(this, "The patient record could not be found or was not updated.", "No changes saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RefreshPatientTable();
                    return;
                }

                RefreshPatientTable();
                ClearPatientForm();
                MessageBox.Show(this, "Patient record updated.", "Patient saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(this, "Select a patient record to deactivate.", "Patient required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(this, "Deactivate this patient record? Historical records will be retained.",
                    "Confirm patient deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                _patientService ??= new PatientService(Config.GetConnectionString());
                if (_patientService.Delete(patientId) == 0)
                {
                    MessageBox.Show(this, "The patient record could not be found or was not deactivated.", "No changes made", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RefreshPatientTable();
                    return;
                }

                RefreshPatientTable();
                ClearPatientForm();
                MessageBox.Show(this, "Patient record deactivated.", "Patient updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "deactivating patient");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearPatientForm();
        }

        private void ClearPatientForm()
        {
            txtPatientID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            numAge.Value = 0;
            cmbGender.SelectedIndex = -1;
            txtAddress.Clear();
            txtContactNo.Clear();
            lblAdmissionStatusVal.Text = "Not Admitted";
            dgvAdmissionHistory.Rows.Clear();
            btnViewDischargeRecord.Enabled = false;
        }

        private bool TryBuildPatient(out Model.Patient patient)
        {
            patient = new Model.Patient();
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show(this, "Enter both the patient's first and last name.", "Name required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // Populate textboxes when a row is selected in DataGridView
            if (dgvPatients.SelectedRows.Count > 0)
            {
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
                lblAdmissionStatusVal.Text = row.Cells["colAdmissionStatus"].Value?.ToString() ?? "Not Admitted";

                if (int.TryParse(txtPatientID.Text, out var patientId))
                    RefreshPatientAdmissions(patientId);
                else
                {
                    dgvAdmissionHistory.Rows.Clear();
                    btnViewDischargeRecord.Enabled = false;
                }
            }
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