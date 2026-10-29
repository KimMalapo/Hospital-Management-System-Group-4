using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            this.Close();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            HomeUserControl homeUserControl = new HomeUserControl();
            homeUserControl.Dock = DockStyle.Fill;
            pnlUCHandler.Controls.Clear();
            pnlUCHandler.Controls.Add(homeUserControl);

        }

        private void UILandingPage_Load(object sender, EventArgs e)
        {
            HomeUserControl homeUserControl = new HomeUserControl();
            homeUserControl.Dock = DockStyle.Fill;
            pnlUCHandler.Controls.Clear();
            pnlUCHandler.Controls.Add(homeUserControl);
        }

        private void btnPatientMaintenance_Click(object sender, EventArgs e)
        {
            PatientMaintenanceUserControl userControl = new PatientMaintenanceUserControl();
            userControl.Dock = DockStyle.Fill;
            pnlUCHandler.Controls.Clear();
            pnlUCHandler.Controls.Add(userControl);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            UnifiedSearchUserControl userControl = new UnifiedSearchUserControl();
            userControl.Dock = DockStyle.Fill;
            pnlUCHandler.Controls.Clear();
            pnlUCHandler.Controls.Add(userControl);
        }

        private void btnMasterData_Click(object sender, EventArgs e)
        {
            MasterDataUserControl userControl = new MasterDataUserControl();
            userControl.Dock = DockStyle.Fill;
            pnlUCHandler.Controls.Clear();
            pnlUCHandler.Controls.Add(userControl);
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            RoomManagementUserControl userControl = new RoomManagementUserControl();
            userControl.Dock = DockStyle.Fill;
            pnlUCHandler.Controls.Clear();
            pnlUCHandler.Controls.Add(userControl);
        }
    }
}
