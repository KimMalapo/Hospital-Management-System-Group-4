using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;

namespace UI
{
    public partial class PatientMaintenanceUserControl : UserControl
    {
        private PatientService? _patientService;
        private AdmissionService? _admissionService;
        private bool _databaseWarningShown;

        public PatientMaintenanceUserControl()
        {
            InitializeComponent();
            txtSearch.TextChanged += TxtSearch_TextChanged;
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
            // Implementation for Adding Patient
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Implementation for Updating Patient Record
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Implementation for Deleting Patient Record
            // Rule: Delete allowed only if patient has no active admission or related records
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear input fields
            txtPatientID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            numAge.Value = 0;
            cmbGender.SelectedIndex = -1;
            txtAddress.Clear();
            txtContactNo.Clear();
            lblAdmissionStatusVal.Text = "Not Admitted";
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
            }
        }
    }
}