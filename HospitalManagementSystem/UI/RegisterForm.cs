using System;
using System.Data;
using System.Windows.Forms;
using BusinessLogic;

namespace UI
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            btnCreate.Click += BtnCreate_Click;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in email and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!PasswordHelper.IsStrongPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters and include upper, lower, digit and special.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var connStr = Config.GetConnectionString();
            if (string.IsNullOrWhiteSpace(connStr))
            {
                MessageBox.Show("Database connection not configured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var svc = new UserService(connStr);
                var rows = svc.CreateUser(email, PasswordHelper.HashPassword(password));
                if (rows > 0)
                {
                    MessageBox.Show("User created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
