using System;
using System.Windows.Forms;

namespace UI
{
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl()
        {
            InitializeComponent();
        }

        private void btnRums_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView<RoomManagementUserControl>(pnlUCHandler);
        }

        private void btnPatients1_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView<PatientMaintenanceUserControl>(pnlUCHandler);
        }

        private void btnMdata_Click(object sender, EventArgs e)
        {
            UIHelper.ShowView<MasterDataUserControl>(pnlUCHandler);
        }
    }
}
