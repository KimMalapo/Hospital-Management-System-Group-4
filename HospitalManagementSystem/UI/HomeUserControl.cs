using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class HomeUserControl : UserControl
    {
        UILandingPage landingPage;
        public HomeUserControl()
        {
            InitializeComponent();
            landingPage = (UILandingPage)this.FindForm();
        }

        private void btnRums_Click(object sender, EventArgs e)
        {
            RoomManagementUserControl userControl = new RoomManagementUserControl();
            userControl.Dock = DockStyle.Fill;
            pnlUCHandler.Controls.Clear();
            pnlUCHandler.Controls.Add(userControl);
        }

        private void btnPatients1_Click(object sender, EventArgs e)
        {
            PatientMaintenanceUserControl userControl = new PatientMaintenanceUserControl();
            userControl.Dock = DockStyle.Fill;
            pnlUCHandler.Controls.Clear();
            pnlUCHandler.Controls.Add(userControl);
        }

        private void btnMdata_Click(object sender, EventArgs e)
        {
            MasterDataUserControl userControl = new MasterDataUserControl();
            userControl.Dock = DockStyle.Fill;
            pnlUCHandler.Controls.Clear();
            pnlUCHandler.Controls.Add(userControl);
        }
    }
}
