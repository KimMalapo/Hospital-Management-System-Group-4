using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class RoomManagementUserControl : UserControl
    {
        public RoomManagementUserControl()
        {
            InitializeComponent();
        }

        private void RoomManagementUserControl_Load(object sender, EventArgs e)
        {
            // Populate ComboBox defaults
            cmbRoomType.Items.Clear();
            cmbRoomType.Items.Add("Private");
            cmbRoomType.Items.Add("Semi-Private");
            cmbRoomType.Items.Add("Ward");
            cmbRoomType.Items.Add("Deluxe Private");

            cmbAvailability.Items.Clear();
            cmbAvailability.Items.Add("Available");
            cmbAvailability.Items.Add("Occupied");

            // Load initial sample room data
            LoadSampleRoomData();
        }

        private void LoadSampleRoomData()
        {
            dgvRooms.Rows.Clear();
            dgvRooms.Rows.Add("101", "Private", "P-101-A", "500.00", "Occupied");
            dgvRooms.Rows.Add("102", "Semi-Private", "S-102-B", "350.00", "Available");
            dgvRooms.Rows.Add("103", "Ward", "W-103-C", "200.00", "Available");
            dgvRooms.Rows.Add("104", "Deluxe Private", "D-104-D", "750.00", "Occupied");
            dgvRooms.Rows.Add("105", "Private", "P-105-A", "500.00", "Available");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRoomNo.Text) || string.IsNullOrEmpty(txtBedNo.Text))
            {
                MessageBox.Show("Room Number and Bed Number are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string roomNo = txtRoomNo.Text;
            string roomType = cmbRoomType.SelectedItem?.ToString() ?? "Private";
            string bedNo = txtBedNo.Text;
            string rate = numRate.Value.ToString("0.00");
            string status = cmbAvailability.SelectedItem?.ToString() ?? "Available";

            dgvRooms.Rows.Add(roomNo, roomType, bedNo, rate, status);
            ClearForm();
            MessageBox.Show("Room successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvRooms.SelectedRows[0];
                row.Cells["colRoomNo"].Value = txtRoomNo.Text;
                row.Cells["colRoomType"].Value = cmbRoomType.SelectedItem?.ToString();
                row.Cells["colBedNo"].Value = txtBedNo.Text;
                row.Cells["colRate"].Value = numRate.Value.ToString("0.00");
                row.Cells["colStatus"].Value = cmbAvailability.SelectedItem?.ToString();

                MessageBox.Show("Room record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a room record from the grid to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count > 0)
            {
                dgvRooms.Rows.RemoveAt(dgvRooms.SelectedRows[0].Index);
                ClearForm();
                MessageBox.Show("Room record deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a room record to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtRoomNo.Clear();
            cmbRoomType.SelectedIndex = -1;
            txtBedNo.Clear();
            numRate.Value = 0;
            cmbAvailability.SelectedIndex = -1;
            dgvRooms.ClearSelection();
        }

        private void dgvRooms_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvRooms.SelectedRows[0];
                txtRoomNo.Text = row.Cells["colRoomNo"].Value?.ToString();
                cmbRoomType.SelectedItem = row.Cells["colRoomType"].Value?.ToString();
                txtBedNo.Text = row.Cells["colBedNo"].Value?.ToString();
                if (decimal.TryParse(row.Cells["colRate"].Value?.ToString(), out decimal rate))
                {
                    numRate.Value = rate;
                }
                cmbAvailability.SelectedItem = row.Cells["colStatus"].Value?.ToString();
            }
        }
    }
}