using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BusinessLogic;

namespace UI
{
    public partial class PatientInformationMaintenance : Form
    {
        private readonly PatientService? _service;

        public PatientInformationMaintenance()
        {
            InitializeComponent();
            var cs = Config.GetConnectionString();
            if (!string.IsNullOrWhiteSpace(cs)) _service = new PatientService(cs);

            // Wire up button handlers so the form is functional
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
            catch { }
        }

        // Simple read for patient list. Call with SQL like "SELECT * FROM dbo.tblPatients"
        public DataTable LoadPatients(string sql = "SELECT * FROM dbo.tblPatients")
        {
            try
            {
                return _service != null ? _service.GetPatients(sql) : new DataTable();
            }
            catch
            {
                return new DataTable();
            }
        }

        // Simple create for patient. Provide parameterized SQL and SqlParameters.
        public int CreatePatient(string insertSql, params SqlParameter[] parameters)
        {
            try
            {
                return _service != null ? _service.CreatePatient(insertSql, parameters) : 0;
            }
            catch
            {
                return 0;
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            var id = txtPatientID.Text.Trim();
            var name = txtName.Text.Trim();
            if (!int.TryParse(txtAge.Text.Trim(), out var age))
            {
                MessageBox.Show("Please enter a valid age.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var gender = cbGender.SelectedItem?.ToString() ?? string.Empty;
            var address = txtAddress.Text.Trim();
            var contact = txtContactNo.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sql = "INSERT INTO dbo.tblPatients (PatientID, Name, Age, Gender, Address, ContactNo) VALUES (@id, @name, @age, @gender, @address, @contact)";
                var rows = CreatePatient(sql,
                    new SqlParameter("@id", string.IsNullOrWhiteSpace(id) ? (object)DBNull.Value : id),
                    new SqlParameter("@name", name),
                    new SqlParameter("@age", age),
                    new SqlParameter("@gender", gender),
                    new SqlParameter("@address", address),
                    new SqlParameter("@contact", contact)
                );

                if (rows > 0)
                {
                    MessageBox.Show("Patient added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding patient: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            var id = txtPatientID.Text.Trim();
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Patient ID is required for update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out var age))
            {
                MessageBox.Show("Please enter a valid age.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var name = txtName.Text.Trim();
            var gender = cbGender.SelectedItem?.ToString() ?? string.Empty;
            var address = txtAddress.Text.Trim();
            var contact = txtContactNo.Text.Trim();

            try
            {
                var sql = "UPDATE dbo.tblPatients SET Name=@name, Age=@age, Gender=@gender, Address=@address, ContactNo=@contact WHERE PatientID=@id";
                var rows = CreatePatient(sql,
                    new SqlParameter("@name", name),
                    new SqlParameter("@age", age),
                    new SqlParameter("@gender", gender),
                    new SqlParameter("@address", address),
                    new SqlParameter("@contact", contact),
                    new SqlParameter("@id", id)
                );

                if (rows > 0)
                {
                    MessageBox.Show("Patient updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating patient: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            var id = txtPatientID.Text.Trim();
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Patient ID is required for delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this patient?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var sql = "DELETE FROM dbo.tblPatients WHERE PatientID=@id";
                var rows = CreatePatient(sql, new SqlParameter("@id", id));
                if (rows > 0)
                {
                    MessageBox.Show("Patient deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting patient: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
