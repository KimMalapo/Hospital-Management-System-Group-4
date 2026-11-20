using System;
using System.Windows.Forms;

namespace UI
{
    public partial class UILandingPage : Form
    {
        public UILandingPage()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
            ReturnToLogin();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView<HomeUserControl>(pnlUCHandler);
        }

        private void UILandingPage_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsAuthenticated)
            {
                MessageBox.Show("Your session is no longer valid. Please sign in again.", "Sign-in required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReturnToLogin();
                return;
            }

            lblSignedInAs.Text = SessionManager.CurrentEmail ?? "Unknown";
            UIHelper.ShowView<HomeUserControl>(pnlUCHandler);
        }

        private void ReturnToLogin()
        {
            var loginForm = Application.OpenForms.OfType<LoginForm>().FirstOrDefault() ?? new LoginForm();
            loginForm.Show();
            Close();
        }

        private void btnPatientMaintenance_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView<PatientMaintenanceUserControl>(pnlUCHandler);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView<UnifiedSearchUserControl>(pnlUCHandler);
        }

        private void btnMasterData_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView<MasterDataUserControl>(pnlUCHandler);
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView<RoomManagementUserControl>(pnlUCHandler);
        }
    }
}
