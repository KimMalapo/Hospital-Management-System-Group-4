using System.Data;
using Microsoft.Data.SqlClient;

namespace UI
{
    /// <summary>Search screen: find a patient and see their admission status.</summary>
    public partial class UnifiedSearchUserControl : UserControl
    {
        public UnifiedSearchUserControl()
        {
            InitializeComponent();
            UITheme.Apply(this);
        }

        private void UnifiedSearchUserControl_Load(object sender, EventArgs e) => ClearSearchFields();

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchInput = txtSearchInput.Text.Trim();

            if (string.IsNullOrEmpty(searchInput))
            {
                UIHelper.ShowWarning("Please enter a Patient ID or Name to search.", "Validation Error");
                return;
            }

            try
            {
                DataTable results = DbHelper.Query(
                    @"SELECT TOP (1)
                          p.PatientID, p.FirstName, p.LastName, p.DateOfBirth, p.Gender, p.ContactNumber,
                          a.Status AS AdmissionStatus, r.RoomNo, r.Status AS RoomStatus
                      FROM dbo.tblPatients p
                      LEFT JOIN dbo.tblAdmissions a
                          ON a.PatientID = p.PatientID AND a.Status = N'Admitted'
                      LEFT JOIN dbo.tblRoomAssignments ra
                          ON ra.AssignmentID = a.AssignmentID AND ra.DischargeDate IS NULL
                      LEFT JOIN dbo.tblRooms r ON r.RoomNo = ra.RoomNo
                      WHERE p.IsActive = 1
                        AND (CONVERT(NVARCHAR(20), p.PatientID) = @search
                             OR p.FirstName LIKE N'%' + @search + N'%'
                             OR p.LastName LIKE N'%' + @search + N'%'
                             OR p.FirstName + N' ' + p.LastName LIKE N'%' + @search + N'%'
                             OR p.ContactNumber LIKE N'%' + @search + N'%'
                             OR p.Email LIKE N'%' + @search + N'%')
                      ORDER BY CASE WHEN CONVERT(NVARCHAR(20), p.PatientID) = @search THEN 0 ELSE 1 END,
                               p.LastName, p.FirstName;",
                    new SqlParameter("@search", searchInput));

                if (results.Rows.Count == 0)
                {
                    ClearSearchFields();
                    lblValAdmissionStatus.Text = "Patient not found";
                    return;
                }

                ShowPatientDetails(results.Rows[0]);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError($"Unable to search patients: {ex.Message}", "Search Error");
            }
        }

        private void ShowPatientDetails(DataRow patient)
        {
            lblValPatientID.Text = patient["PatientID"].ToString();
            lblValFullName.Text = $"{patient["FirstName"]} {patient["LastName"]}";
            lblValGender.Text = ValueOrDefault(patient, "Gender", "Not specified");
            lblValContact.Text = ValueOrDefault(patient, "ContactNumber", "Not provided");
            lblValAge.Text = patient["DateOfBirth"] == DBNull.Value
                ? "Not provided"
                : UIHelper.CalculateAge(Convert.ToDateTime(patient["DateOfBirth"])).ToString();

            bool hasActiveAdmission = patient["AdmissionStatus"] != DBNull.Value;
            lblValAdmissionStatus.Text = hasActiveAdmission ? "Admitted" : "Not Admitted";
            lblValRoom.Text = ValueOrDefault(patient, "RoomNo", "No active room assignment");
            lblValBed.Text = hasActiveAdmission ? "Not tracked" : "---";
            lblValRoomStatus.Text = ValueOrDefault(patient, "RoomStatus", "---");
        }

        private static string? ValueOrDefault(DataRow row, string column, string fallback) =>
            row[column] == DBNull.Value ? fallback : row[column].ToString();

        private void ClearSearchFields()
        {
            lblValPatientID.Text = "---";
            lblValFullName.Text = "---";
            lblValAge.Text = "---";
            lblValGender.Text = "---";
            lblValContact.Text = "---";
            lblValAdmissionStatus.Text = "Not Registered";

            lblValRoom.Text = "No active room assignment";
            lblValBed.Text = "---";
            lblValRoomStatus.Text = "---";
        }
    }
}