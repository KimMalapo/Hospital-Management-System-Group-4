using System;
using System.Net.Mail;
using System.Windows.Forms;
using BusinessLogic;
using Microsoft.Data.SqlClient;

namespace UI
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            UITheme.Apply(this);
            btnCreate.Click += BtnCreate_Click;
            txtPassword.TextChanged += TxtPassword_TextChanged;
            txtConfirmPassword.TextChanged += TxtConfirmPassword_TextChanged;
        }

        private void TxtPassword_TextChanged(object? sender, EventArgs e)
        {
            bool isStrong = PasswordHelper.IsStrongPassword(txtPassword.Text);
            lblPasswordHelp.ForeColor = isStrong
                ? System.Drawing.Color.FromArgb(22, 101, 52)
                : System.Drawing.Color.FromArgb(100, 116, 139);
            lblPasswordHelp.Text = isStrong
                ? "Password meets the security requirements."
                : "Use 8+ characters with uppercase, lowercase, a number and a symbol.";
            UpdatePasswordMatch();
        }

        private void TxtConfirmPassword_TextChanged(object? sender, EventArgs e) => UpdatePasswordMatch();

        private void UpdatePasswordMatch()
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                lblFormStatus.Text = string.Empty;
                return;
            }

            bool matches = string.Equals(txtPassword.Text, txtConfirmPassword.Text, StringComparison.Ordinal);
            lblFormStatus.ForeColor = matches
                ? System.Drawing.Color.FromArgb(22, 101, 52)
                : System.Drawing.Color.FromArgb(185, 28, 28);
            lblFormStatus.Text = matches ? "Passwords match." : "Passwords do not match.";
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            if (!MailAddress.TryCreate(email, out var address) ||
                !string.Equals(address.Address, email, StringComparison.OrdinalIgnoreCase))
            {
                ShowError("Enter a valid email address.");
                txtEmail.Focus();
                return;
            }

            if (!PasswordHelper.IsStrongPassword(password))
            {
                ShowError("Choose a password that meets all the security requirements.");
                txtPassword.Focus();
                return;
            }

            if (!string.Equals(password, txtConfirmPassword.Text, StringComparison.Ordinal))
            {
                ShowError("The passwords do not match.");
                txtConfirmPassword.Focus();
                return;
            }

            var connStr = Config.GetConnectionString();
            if (string.IsNullOrWhiteSpace(connStr))
            {
                ShowError("Account registration is not configured. Contact your system administrator.");
                return;
            }

            btnCreate.Enabled = false;
            btnCreate.Text = "Creating account...";
            try
            {
                var svc = new UserService(connStr);
                var rows = svc.CreateUser(email, PasswordHelper.HashPassword(password));
                if (rows > 0)
                {
                    MessageBox.Show(this, "Your account has been created. You can now sign in.",
                        "Account created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    ShowError("The account could not be created. Please try again.");
                }
            }
            catch (SqlException ex) when (ex.Number is 2601 or 2627)
            {
                ShowError("An account with this email already exists.");
                txtEmail.Focus();
                txtEmail.SelectAll();
            }
            catch (Exception ex)
            {
                Logger.Error("RegisterForm: Error creating user", ex);
                ShowError("Unable to create the account right now. Check the connection and try again.");
            }
            finally
            {
                if (!IsDisposed)
                {
                    btnCreate.Enabled = true;
                    btnCreate.Text = "Create account";
                }
            }
        }

        private void ShowError(string message)
        {
            lblFormStatus.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            lblFormStatus.Text = message;
        }
    }
}
