using System.Data;
using BusinessLogic;

namespace UI
{
    /// <summary>Old patient search window. Nothing opens it now.</summary>
    public partial class PatientInformationSearch : Form
    {
        private readonly PatientService? _service;

        public PatientInformationSearch()
        {
            InitializeComponent();
            UITheme.Apply(this);
            var cs = Config.GetConnectionString();
            if (!string.IsNullOrWhiteSpace(cs)) _service = new PatientService(cs);
            btnSearch.Click += BtnSearch_Click;
        }

        public DataTable SearchPatients(string searchTerm) =>
            _service?.GetPatients(searchTerm)
                ?? throw new InvalidOperationException("The patient database service is not configured.");

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                UIHelper.ShowWarning(this, "Enter a patient ID, name, or contact number to search.", "Search required");
                txtSearch.Focus();
                return;
            }

            try
            {
                var results = SearchPatients(txtSearch.Text.Trim());
                if (results.Rows.Count == 0)
                {
                    ClearPatientDetails();
                    UIHelper.ShowInfo("No matching patient was found.", "Search");
                    return;
                }

                ShowPatientDetails(results.Rows[0]);
            }
            catch (Exception ex)
            {
                Logger.Error("PatientInformationSearch.SearchPatients failed", ex);
                ClearPatientDetails();
                UIHelper.ShowError("Unable to search patients. Check the database connection and try again.", "Database error");
            }
        }

        private void ShowPatientDetails(DataRow patient)
        {
            lblPateintID.Text = Convert.ToString(patient["PatientID"]);
            lblName.Text = $"{patient["FirstName"]} {patient["LastName"]}";
            lblGender.Text = patient.IsNull("Gender") ? "N/A" : Convert.ToString(patient["Gender"]);
            lblContact.Text = patient.IsNull("ContactNumber") ? "N/A" : Convert.ToString(patient["ContactNumber"]);
            lblAge.Text = patient.IsNull("DateOfBirth")
                ? "N/A"
                : UIHelper.CalculateAge(Convert.ToDateTime(patient["DateOfBirth"])).ToString();
        }

        private void ClearPatientDetails()
        {
            lblPateintID.Text = string.Empty;
            lblName.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblGender.Text = string.Empty;
            lblContact.Text = string.Empty;
        }
    }
}
