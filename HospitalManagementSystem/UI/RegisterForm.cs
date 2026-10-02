using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace UI
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            btnCreate.Click += BtnCreate_Click;
        }

        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!PasswordHelper.IsStrongPassword(password))
            {
                MessageBox.Show("Password is not strong enough. It must be at least 8 characters and include upper, lower, digit, and special character.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var connStr = Config.GetConnectionString() ?? throw new InvalidOperationException("No connection string available");
            try
            {
                using var conn = new SqlConnection(connStr);
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.tblUsers (Email, Password) VALUES (@e, @p);";
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@p", PasswordHelper.HashPassword(password));
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("User created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
