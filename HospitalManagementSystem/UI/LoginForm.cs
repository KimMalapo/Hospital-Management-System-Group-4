using BusinessLogic;

namespace UI
{
    /// <summary>Login window. Checks the password, then opens the main screen.</summary>
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            UITheme.Apply(this);

            btnLogin.MouseEnter += BtnLogin_MouseEnter;
            btnLogin.MouseLeave += BtnLogin_MouseLeave;

            linkRegister.MouseEnter += LinkRegister_MouseEnter;
            linkRegister.MouseLeave += LinkRegister_MouseLeave;
            linkRegister.Click += LinkRegister_Click;
        }

        private void LinkRegister_Click(object? sender, EventArgs e)
        {
            using var registerForm = new RegisterForm();
            registerForm.ShowDialog(this);
        }

        private void BtnLogin_MouseEnter(object? sender, EventArgs e) =>
            btnLogin.BackColor = Color.FromArgb(70, 130, 255);

        private void BtnLogin_MouseLeave(object? sender, EventArgs e) =>
            btnLogin.BackColor = Color.FromArgb(52, 110, 245);

        private void LinkRegister_MouseEnter(object? sender, EventArgs e) =>
            linkRegister.LinkColor = Color.FromArgb(30, 80, 200);

        private void LinkRegister_MouseLeave(object? sender, EventArgs e) =>
            linkRegister.LinkColor = Color.FromArgb(52, 110, 245);

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                UIHelper.ShowWarning("Please fill all fields.", "Validation");
                return;
            }

            try
            {
                if (!AuthService.IsConfigured)
                    throw new InvalidOperationException(AuthService.ConfigurationMessage);

                var result = new UserService(Config.GetConnectionString()).Login(username, password);
                if (result.Success)
                {
                    var token = new AuthService().CreateToken(username, result.Role);
                    SessionManager.SetToken(token);
                    new UILandingPage().Show();
                    Hide();
                }
                else
                {
                    UIHelper.ShowWarning("Invalid email/username or password.", "Authentication Failed");
                }
            }
            catch (InvalidOperationException ex)
            {
                Logger.Error("LoginForm: configuration error", ex);
                UIHelper.ShowError(ex.Message, "Configuration");
            }
            catch (Exception ex)
            {
                Logger.Error("LoginForm: login failed while connecting to database", ex);
                UIHelper.ShowError("Unable to sign in. Check the database connection and try again.", "Sign-in failed");
            }
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            txtPassword.PasswordChar = txtPassword.UseSystemPasswordChar ? '•' : '\0';
        }
    }
}
