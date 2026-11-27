using BusinessLogic;
using Model;

namespace UI
{
    /// <summary>Old add/edit/delete patient window. Nothing opens it now.</summary>
    public partial class PatientInformationMaintenance : Form
    {
        private readonly PatientService? _service;

        public PatientInformationMaintenance()
        {
            InitializeComponent();
            UITheme.Apply(this);

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

            this.btnAdd.Click += BtnAdd_Click;
            this.btnUpdate.Click += BtnUpdate_Click;
            this.btnDelete.Click += BtnDelete_Click;

            try
            {
                cbGender.Items.Clear();
                cbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
                cbGender.SelectedIndex = 0;
            }
            catch (Exception logEx) { Logger.Error("PatientInformationMaintenance: could not populate gender list", logEx); }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (!HasRequiredFields(includePatientId: false)) return;

            if (!int.TryParse(txtAge.Text.Trim(), out var age) || age < 0 || age > 150)
            {
                UIHelper.ShowWarning("Enter a valid age between 0 and 150.", "Validation Warning");
                return;
            }
            if (!TryParseName(out var firstName, out var lastName)) return;

            if (_service == null)
            {
                ShowServiceNotInitialized();
                return;
            }

            var patient = BuildPatient(firstName, lastName, age);

            try
            {
                int newId = _service.Create(patient);
                if (newId > 0)
                {
                    UIHelper.ShowInfo($"Patient added successfully! Generated ID: {newId}", "Success");
                    ClearFields();
                }
                else
                {
                    UIHelper.ShowError("Failed to add patient record.", "Error");
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
                UIHelper.ShowWarning("Patient ID is required to update a record.", "Validation Warning");
                return;
            }
            if (!TryParseName(out var firstName, out var lastName)) return;
            if (!HasRequiredFields(includePatientId: true)) return;

            if (!int.TryParse(txtPatientID.Text.Trim(), out var patientId) ||
                !int.TryParse(txtAge.Text.Trim(), out var age) || age < 0 || age > 150)
            {
                UIHelper.ShowWarning("Enter a valid patient ID and an age between 0 and 150.", "Validation Warning");
                return;
            }

            if (_service == null)
            {
                ShowServiceNotInitialized();
                return;
            }

            var patient = BuildPatient(firstName, lastName, age);
            patient.PatientID = patientId;

            try
            {
                int rows = _service.Update(patient);
                if (rows > 0)
                {
                    UIHelper.ShowInfo("Patient updated successfully.", "Success");
                    ClearFields();
                }
                else
                {
                    UIHelper.ShowWarning("No records were updated. Please check if the Patient ID exists.", "Warning");
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
                UIHelper.ShowWarning("A valid numeric Patient ID is required for deletion.", "Validation Warning");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this patient?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (_service == null)
            {
                ShowServiceNotInitialized();
                return;
            }

            try
            {
                int rows = _service.Delete(patientId);
                if (rows > 0)
                {
                    UIHelper.ShowInfo("Patient deleted successfully.", "Success");
                    ClearFields();
                }
                else
                {
                    UIHelper.ShowWarning("No records were deleted. Patient ID may not exist.", "Warning");
                }
            }
            catch (Exception ex)
            {
                UIHelper.HandleDatabaseError(ex, "deleting patient");
            }
        }

        private static void ShowServiceNotInitialized() =>
            UIHelper.ShowError("Database service is not initialized.", "Configuration Error");

        // Shows a warning and returns false when any required field is empty.
        private bool HasRequiredFields(bool includePatientId)
        {
            var fields = new Dictionary<string, string>();
            if (includePatientId) fields["Patient ID"] = txtPatientID.Text.Trim();
            fields["Name"] = txtName.Text.Trim();
            fields["Age"] = txtAge.Text.Trim();
            fields["Gender"] = cbGender.SelectedItem?.ToString() ?? string.Empty;
            fields["Address"] = txtAddress.Text.Trim();
            fields["Contact No."] = txtContactNo.Text.Trim();

            if (UIHelper.ValidateFields(fields, out string validationError)) return true;

            UIHelper.ShowWarning(validationError, "Validation Warning");
            return false;
        }

        // The date of birth is estimated from the entered age.
        private Patient BuildPatient(string firstName, string lastName, int age) => new()
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = cbGender.SelectedItem?.ToString(),
            Address = txtAddress.Text.Trim(),
            ContactNumber = txtContactNo.Text.Trim(),
            DateOfBirth = DateTime.Today.AddYears(-age)
        };

        private void ClearFields()
        {
            txtPatientID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            if (cbGender.Items.Count > 0) cbGender.SelectedIndex = 0;
        }

        private bool TryParseName(out string firstName, out string lastName)
        {
            var parts = txtName.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length < 2)
            {
                firstName = string.Empty;
                lastName = string.Empty;
                UIHelper.ShowWarning("Enter both a first name and a last name.", "Validation Warning");
                txtName.Focus();
                return false;
            }

            firstName = parts[0];
            lastName = string.Join(' ', parts.Skip(1));
            return true;
        }
    }
}
