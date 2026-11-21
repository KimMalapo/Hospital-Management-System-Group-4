using System;
using System.Windows.Forms;

namespace UI
{
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl()
        {
            InitializeComponent();
            UITheme.Apply(this);
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;
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
