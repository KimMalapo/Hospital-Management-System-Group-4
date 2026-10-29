using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.XPath;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace UI
{
    public partial class MasterDataUserControl : UserControl
    {
        public MasterDataUserControl()
        {
            InitializeComponent();
        }

        private void MasterDataUserControl_Load(object sender, EventArgs e)
        {
            cmbDataType.Items.Clear();
            cmbDataType.Items.Add("Service Type");
            cmbDataType.Items.Add("Room Type");
            cmbDataType.Items.Add("Treatment Code");

            LoadSampleData();
        }

        private void LoadSampleData()
        {
            dgvMasterData.Rows.Clear();
            dgvMasterData.Rows.Add("1", "Service Type", "Lab Test", "LT001", "50.00", "Active");
            dgvMasterData.Rows.Add("2", "Room Type", "Deluxe", "DR301", "300.00", "Active");
            dgvMasterData.Rows.Add("3", "Treatment Code", "Consultation", "CN101", "75.00", "Active");
            dgvMasterData.Rows.Add("4", "Room Type", "Semi-Private", "SPR201", "200.00", "Inactive");
            dgvMasterData.Rows.Add("5", "Service Type", "Radiology", "RT002", "120.00", "Active");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validation & Add logic
            if (string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dataType = cmbDataType.SelectedItem?.ToString() ?? "Service Type";
            string desc = txtDescription.Text;
            string code = txtCode.Text;
            string rate = numRate.Value.ToString("0.00");
            string status = chkActive.Checked ? "Active" : "Inactive";

            int newId = dgvMasterData.Rows.Count + 1;
            dgvMasterData.Rows.Add(newId.ToString(), dataType, desc, code, rate, status);
            ClearForm();
            MessageBox.Show("Master data successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMasterData.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvMasterData.SelectedRows[0];
                row.Cells["colDataType"].Value = cmbDataType.SelectedItem?.ToString();
                row.Cells["colDescription"].Value = txtDescription.Text;
                row.Cells["colCode"].Value = txtCode.Text;
                row.Cells["colRate"].Value = numRate.Value.ToString("0.00");
                row.Cells["colStatus"].Value = chkActive.Checked ? "Active" : "Inactive";
                MessageBox.Show("Master data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a record from the grid to edit.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMasterData.SelectedRows.Count > 0)
            {
                dgvMasterData.Rows.RemoveAt(dgvMasterData.SelectedRows[0].Index);
                ClearForm();
                MessageBox.Show("Record deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            cmbDataType.SelectedIndex = -1;
            txtDescription.Clear();
            txtCode.Clear();
            numRate.Value = 0;
            chkActive.Checked = true;
            dgvMasterData.ClearSelection();
        }

        private void dgvMasterData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMasterData.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvMasterData.SelectedRows[0];
                cmbDataType.SelectedItem = row.Cells["colDataType"].Value?.ToString();
                txtDescription.Text = row.Cells["colDescription"].Value?.ToString();
                txtCode.Text = row.Cells["colCode"].Value?.ToString();
                if (decimal.TryParse(row.Cells["colRate"].Value?.ToString(), out decimal rate))
                {
                    numRate.Value = rate;
                }
                chkActive.Checked = (row.Cells["colStatus"].Value?.ToString() == "Active");
            }
        }
    }
}