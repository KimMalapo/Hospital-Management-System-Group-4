using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using BusinessLogic;
using Model;

namespace UI
{
    public partial class PatientInformationMaintenance : Form
    {
        private readonly PatientService? _service;

        public PatientInformationMaintenance()
        {
            InitializeComponent();

            try
            {
                var cs = Config.GetConnectionString();
                if (!string.IsNullOrWhiteSpace(cs))
                {
                    _service = new PatientService(cs);
                }
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "initializing patient service");
            }

            // Wire up button handlers
            this.btnAdd.Click += BtnAdd_Click;
            this.btnUpdate.Click += BtnUpdate_Click;
            this.btnDelete.Click += BtnDelete_Click;

            // Populate gender choices
            try
            {
                cbGender.Items.Clear();
                cbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
                if (cbGender.Items.Count > 0) cbGender.SelectedIndex = 0;
            }
            catch (Exception logEx) { Logger.Error("PatientInformationMaintenance: could not populate gender list", logEx); }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            // 1. Use UIHelper to validate empty fields
            var fieldsToCheck = new Dictionary<string, string>
            {
                { "Name", txtName.Text.Trim() },
                { "Age", txtAge.Text.Trim() },
                { "Gender", cbGender.SelectedItem?.ToString() ?? string.Empty },
                { "Address", txtAddress.Text.Trim() },
                { "Contact No.", txtContactNo.Text.Trim() }
            };

            if (!UIHelper.ValidateFields(fieldsToCheck, out string validationError))
            {
                MessageBox.Show(validationError, "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out var age))
            {
                MessageBox.Show("Please enter a valid numeric value for Age.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_service == null)
            {
                MessageBox.Show("Database service is not initialized.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Map form inputs to the Patient model
            var patient = new Patient
            {
                FirstName = txtName.Text.Trim(),
                LastName = "", // If you have a separate Last Name textbox, map it here
                Gender = cbGender.SelectedItem?.ToString(),
                Address = txtAddress.Text.Trim(),
                ContactNumber = txtContactNo.Text.Trim(),
                DateOfBirth = DateTime.Today.AddYears(-age) // Optional DOB estimate based on age
            };

            // 3. Execute via PatientService with UIHelper handling database connection errors
            try
            {
                int newId = _service.Create(patient);
                if (newId > 0)
                {
                    MessageBox.Show($"Patient added successfully! Generated ID: {newId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to add patient record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "adding patient");
            }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text))
            {
                MessageBox.Show("Patient ID is required to update a record.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fieldsToCheck = new Dictionary<string, string>
            {
                { "Patient ID", txtPatientID.Text.Trim() },
                { "Name", txtName.Text.Trim() },
                { "Age", txtAge.Text.Trim() },
                { "Gender", cbGender.SelectedItem?.ToString() ?? string.Empty },
                { "Address", txtAddress.Text.Trim() },
                { "Contact No.", txtContactNo.Text.Trim() }
            };

            if (!UIHelper.ValidateFields(fieldsToCheck, out string validationError))
            {
                MessageBox.Show(validationError, "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtPatientID.Text.Trim(), out var patientId) || !int.TryParse(txtAge.Text.Trim(), out var age))
            {
                MessageBox.Show("Please enter valid numeric values for Patient ID and Age.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_service == null)
            {
                MessageBox.Show("Database service is not initialized.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var patient = new Patient
            {
                PatientID = patientId,
                FirstName = txtName.Text.Trim(),
                LastName = "",
                Gender = cbGender.SelectedItem?.ToString(),
                Address = txtAddress.Text.Trim(),
                ContactNumber = txtContactNo.Text.Trim(),
                DateOfBirth = DateTime.Today.AddYears(-age)
            };

            try
            {
                int rows = _service.Update(patient);
                if (rows > 0)
                {
                    MessageBox.Show("Patient updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("No records were updated. Please check if the Patient ID exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "updating patient");
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtPatientID.Text.Trim(), out var patientId))
            {
                MessageBox.Show("A valid numeric Patient ID is required for deletion.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this patient?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (_service == null)
            {
                MessageBox.Show("Database service is not initialized.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int rows = _service.Delete(patientId);
                if (rows > 0)
                {
                    MessageBox.Show("Patient deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("No records were deleted. Patient ID may not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "deleting patient");
            }
        }

        private void ClearFields()
        {
            txtPatientID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            if (cbGender.Items.Count > 0) cbGender.SelectedIndex = 0;
        }
    }
}