using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace UI
{
    public partial class UnifiedSearchUserControl : UserControl
    {
        public UnifiedSearchUserControl()
        {
            InitializeComponent();
        }

        private void UnifiedSearchUserControl_Load(object sender, EventArgs e)
        {
            // Initial state / defaults
            ClearSearchFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchInput = txtSearchInput.Text.Trim();

            if (string.IsNullOrEmpty(searchInput))
            {
                MessageBox.Show("Please enter a Patient ID or Name to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                DataRow patient = results.Rows[0];
                lblValPatientID.Text = patient["PatientID"].ToString();
                lblValFullName.Text = $"{patient["FirstName"]} {patient["LastName"]}";
                lblValGender.Text = patient["Gender"] == DBNull.Value ? "Not specified" : patient["Gender"].ToString();
                lblValContact.Text = patient["ContactNumber"] == DBNull.Value ? "Not provided" : patient["ContactNumber"].ToString();

                if (patient["DateOfBirth"] == DBNull.Value)
                {
                    lblValAge.Text = "Not provided";
                }
                else
                {
                    DateTime dateOfBirth = Convert.ToDateTime(patient["DateOfBirth"]);
                    int age = DateTime.Today.Year - dateOfBirth.Year;
                    if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
                    lblValAge.Text = age.ToString();
                }

                bool hasActiveAdmission = patient["AdmissionStatus"] != DBNull.Value;
                lblValAdmissionStatus.Text = hasActiveAdmission ? "Admitted" : "Not Admitted";
                lblValRoom.Text = patient["RoomNo"] == DBNull.Value ? "No active room assignment" : patient["RoomNo"].ToString();
                lblValBed.Text = hasActiveAdmission ? "Not tracked" : "---";
                lblValRoomStatus.Text = patient["RoomStatus"] == DBNull.Value ? "---" : patient["RoomStatus"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to search patients: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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