using System;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;

namespace UI
{
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

        private void btnHome_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView<HomeUserControl>(pnlUCHandler);
        }

        private void btnAdmission_Click(object sender, EventArgs e)
        {
            var view = new AdmissionUserControl();
            view.AdmissionCreated += AdmissionUserControl_AdmissionCreated;
            UIHelper.ShowView(pnlUCHandler, view);
        }

        private void btnActiveAdmissions_Click(object sender, EventArgs e)
        {
            ShowActiveAdmissions();
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView(pnlUCHandler, new BillingUserControl());
        }

        private void btnServiceCatalog_Click(object sender, EventArgs e)
        {
            if (!Permissions.IsAllowed(AppAction.ManageServices))
            {
                MessageBox.Show(this, "Only an Admin can access the Service Catalog.", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UIHelper.ShowView<ServiceCatalogUserControl>(pnlUCHandler);
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

            // Hide Service Catalog from Staff users (AC #7)
            btnServiceCatalog.Visible = Permissions.IsAllowed(AppAction.ManageServices);

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

        private void AdmissionUserControl_AdmissionCreated(object? sender, AdmissionCreatedEventArgs e)
        {
            ShowActiveAdmissions();
        }

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
                UIHelper.ShowView(pnlUCHandler, new BillingUserControl(e.AdmissionId));
                return;
            }

            var action = e.Action switch
            {
                ActiveAdmissionAction.Discharge => "Discharge",
                _ => "Continue"
            };
            MessageBox.Show(this,
                $"{action} for admission {e.AdmissionNo} will be available when its workflow screen is implemented.",
                "Workflow screen not available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}