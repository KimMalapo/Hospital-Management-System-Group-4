using BusinessLogic;

namespace UI
{
    /// <summary>Home screen after login: four dashboard numbers and shortcuts to Rooms, Patients and Master Data.</summary>
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
            Load += (_, _) => ShowDashboardCounts();
        }

        // Four number cards between the welcome box and the module cards.
        private void ShowDashboardCounts()
        {
            try
            {
                var counts = new ReportService(Config.GetConnectionString()).GetDashboardCounts();
                var cards = new (string Caption, string Value)[]
                {
                    ("Active admissions", counts.ActiveAdmissions.ToString()),
                    ("Available rooms", counts.AvailableRooms.ToString()),
                    ("Unpaid bills", counts.UnpaidBills.ToString()),
                    ("Collected today", counts.TodaysCollections.ToString("₱#,##0.00"))
                };

                const int left = 49, top = 285, height = 72, gap = 20, total = 1393;
                var width = (total - gap * (cards.Length - 1)) / cards.Length;
                for (var i = 0; i < cards.Length; i++)
                {
                    var card = new Panel
                    {
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(left + i * (width + gap), top),
                        Size = new Size(width, height)
                    };
                    // The caption (Fill) is added first so the number (Top) is docked before it and the two don't overlap.
                    card.Controls.Add(new Label
                    {
                        Text = cards[i].Caption,
                        Dock = DockStyle.Fill,
                        Font = new Font("Yu Gothic", 10F),
                        ForeColor = Color.DimGray,
                        TextAlign = ContentAlignment.TopCenter
                    });
                    card.Controls.Add(new Label
                    {
                        Text = cards[i].Value,
                        Dock = DockStyle.Top,
                        Height = 42,
                        Font = new Font("Yu Gothic Medium", 18F, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter
                    });
                    pnlUCHandler.Controls.Add(card);
                    card.BringToFront();
                }
            }
            catch (Exception ex)
            {
                // The home screen still works without the numbers.
                Logger.Error("HomeUserControl.ShowDashboardCounts failed", ex);
            }
        }

        private void btnRums_Click(object sender, EventArgs e) =>
            UIHelper.ShowView<RoomManagementUserControl>(pnlUCHandler);

        private void btnPatients1_Click(object sender, EventArgs e) =>
            UIHelper.ShowView<PatientMaintenanceUserControl>(pnlUCHandler);

        private void btnMdata_Click(object sender, EventArgs e) =>
            UIHelper.ShowView<MasterDataUserControl>(pnlUCHandler);
    }
}
