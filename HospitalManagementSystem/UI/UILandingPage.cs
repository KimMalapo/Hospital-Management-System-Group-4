using BusinessLogic;

namespace UI
{
    /// <summary>Main window after login. The left menu swaps the screen on the right.</summary>
    public partial class UILandingPage : Form
    {
        public UILandingPage()
        {
            InitializeComponent();
            UITheme.Apply(this);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
            ReturnToLogin();
        }

        private void btnHome_Click(object sender, EventArgs e) =>
            UIHelper.ShowView<HomeUserControl>(pnlUCHandler);

        private void btnAdmission_Click(object sender, EventArgs e)
        {
            var view = new AdmissionUserControl();
            view.AdmissionCreated += AdmissionUserControl_AdmissionCreated;
            UIHelper.ShowView(pnlUCHandler, view);
        }

        private void btnActiveAdmissions_Click(object sender, EventArgs e) => ShowActiveAdmissions();

        private void btnBilling_Click(object sender, EventArgs e) =>
            UIHelper.ShowView(pnlUCHandler, new BillingUserControl());

        private void UILandingPage_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsAuthenticated)
            {
                UIHelper.ShowInfo("Your session is no longer valid. Please sign in again.", "Sign-in required");
                ReturnToLogin();
                return;
            }

            lblSignedInAs.Text = SessionManager.CurrentEmail ?? "Unknown";
            btnReports.Visible = Permissions.IsAllowed(AppAction.ViewReports); // Admin only
            UIHelper.ShowView<HomeUserControl>(pnlUCHandler);
        }

        private void ReturnToLogin()
        {
            var loginForm = Application.OpenForms.OfType<LoginForm>().FirstOrDefault() ?? new LoginForm();
            loginForm.Show();
            Close();
        }

        private void btnPatientMaintenance_Click(object sender, EventArgs e) =>
            UIHelper.ShowView<PatientMaintenanceUserControl>(pnlUCHandler);

        private void btnSearch_Click(object sender, EventArgs e) =>
            UIHelper.ShowView<UnifiedSearchUserControl>(pnlUCHandler);

        private void btnMasterData_Click(object sender, EventArgs e) =>
            UIHelper.ShowView<MasterDataUserControl>(pnlUCHandler);

        private void btnReports_Click(object sender, EventArgs e) =>
            UIHelper.ShowView<ReportsUserControl>(pnlUCHandler);

        private void btnRoom_Click(object sender, EventArgs e) =>
            UIHelper.ShowView<RoomManagementUserControl>(pnlUCHandler);

        private void AdmissionUserControl_AdmissionCreated(object? sender, AdmissionCreatedEventArgs e) =>
            ShowActiveAdmissions();

        private void ShowActiveAdmissions()
        {
            var view = new ActiveAdmissionsUserControl();
            view.ActionRequested += ActiveAdmissionsUserControl_ActionRequested;
            UIHelper.ShowView(pnlUCHandler, view);
        }

        private void ActiveAdmissionsUserControl_ActionRequested(object? sender, ActiveAdmissionActionEventArgs e)
        {
            if (e.Action == ActiveAdmissionAction.AddCharges)
            {
                UIHelper.ShowView(pnlUCHandler, new ChargesUserControl(e.AdmissionId));
                return;
            }

            if (e.Action == ActiveAdmissionAction.ViewBill)
            {
                ShowBilling(e.AdmissionId);
                return;
            }

            Form dialog = e.Action == ActiveAdmissionAction.AddCharges
                ? new AddChargesForm(e.AdmissionId, e.AdmissionNo)
                : new DischargeForm(e.AdmissionId, e.AdmissionNo);

            using (dialog)
                dialog.ShowDialog(this);

            // Charges and discharges change the list, so reload it.
            (sender as ActiveAdmissionsUserControl)?.RefreshAdmissions();
        }
    }
}