using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace UI
{
    public partial class PatientMaintenanceUserControl : UserControl
    {
        public PatientMaintenanceUserControl()
        {
            InitializeComponent();
        }

        private void PatientMaintenanceUserControl_Load(object sender, EventArgs e)
        {
            // Populate Gender ComboBox defaults
            cmbGender.Items.Clear();
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.Items.Add("Other");

            // Default status label
            lblAdmissionStatusVal.Text = "Not Admitted";
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