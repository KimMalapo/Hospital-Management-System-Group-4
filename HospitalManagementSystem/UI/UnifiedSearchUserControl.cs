using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

            // TODO: Implement database query to retrieve patient and active admission / room assignment details.
            // Example stub logic:
            // if (patientFound) { PopulateData(...); } else { Show "Patient not found"; }
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