using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLogic;

namespace UI
{
    public partial class PatientInformationSearch : Form
    {
        private readonly PatientService? _service;

        public PatientInformationSearch()
        {
            InitializeComponent();
            var cs = Config.GetConnectionString();
            if (!string.IsNullOrWhiteSpace(cs)) _service = new PatientService(cs);
            btnSearch.Click += BtnSearch_Click;
        }

        public DataTable SearchPatients(string searchTerm)
        {
            return _service?.GetPatients(searchTerm)
                ?? throw new InvalidOperationException("The patient database service is not configured.");
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                var results = SearchPatients(txtSearch.Text.Trim());
                if (results.Rows.Count == 0)
                {
                    lblPateintID.Text = string.Empty;
                    lblName.Text = string.Empty;
                    lblAge.Text = string.Empty;
                    lblGender.Text = string.Empty;
                    lblContact.Text = string.Empty;
                    MessageBox.Show("No matching patient was found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var patient = results.Rows[0];
                lblPateintID.Text = Convert.ToString(patient["PatientID"]);
                lblName.Text = $"{patient["FirstName"]} {patient["LastName"]}";
                lblGender.Text = patient.IsNull("Gender") ? "N/A" : Convert.ToString(patient["Gender"]);
                lblContact.Text = patient.IsNull("ContactNumber") ? "N/A" : Convert.ToString(patient["ContactNumber"]);

                if (patient.IsNull("DateOfBirth"))
                {
                    lblAge.Text = "N/A";
                }
                else
                {
                    var dateOfBirth = Convert.ToDateTime(patient["DateOfBirth"]);
                    var age = DateTime.Today.Year - dateOfBirth.Year;
                    if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
                    lblAge.Text = age.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("PatientInformationSearch.SearchPatients failed", ex);
                MessageBox.Show("Unable to search patients. Check the database connection and try again.",
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
