using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace UI
{
    public partial class RoomManagementUserControl : UserControl
    {
        private readonly NumericUpDown _capacity = new();

        public RoomManagementUserControl()
        {
            InitializeComponent();
            UITheme.Apply(this);
            InitializeCapacityEditor();
        }

        private void RoomManagementUserControl_Load(object sender, EventArgs e)
        {
            cmbAvailability.Items.Clear();
            cmbAvailability.Items.AddRange(new object[] { "Available", "Occupied", "Maintenance", "Reserved" });
            txtBedNo.ReadOnly = true;
            txtBedNo.Text = "0 / 1 occupied, 1 free";
            txtSearch.TextChanged += txtSearch_TextChanged;
            LoadRoomTypes();
            LoadRoomData();
        }

        private void LoadRoomTypes()
        {
            DataTable roomTypes = DbHelper.Query(
                "SELECT Code, Name FROM dbo.tblRoomTypes WHERE IsDeleted = 0 AND Status = N'Active' ORDER BY Name");
            cmbRoomType.DataSource = roomTypes;
            cmbRoomType.DisplayMember = "Name";
            cmbRoomType.ValueMember = "Code";
            cmbRoomType.SelectedIndex = roomTypes.Rows.Count > 0 ? 0 : -1;
        }

        private void LoadRoomData()
        {
            try
            {
                dgvRooms.Rows.Clear();
                DataTable rooms = DbHelper.Query(
                    @"SELECT r.RoomNo, r.RoomType, COALESCE(rt.Name, r.RoomType) AS RoomTypeName,
                             r.Rate, r.Capacity,
                             (SELECT COUNT(*) FROM dbo.tblRoomAssignments ra
                              WHERE ra.RoomNo = r.RoomNo AND ra.DischargeDate IS NULL) AS OccupiedBeds,
                             CASE WHEN r.Status IN (N'Maintenance', N'Reserved') THEN r.Status
                                  WHEN (SELECT COUNT(*) FROM dbo.tblRoomAssignments ra
                                        WHERE ra.RoomNo = r.RoomNo AND ra.DischargeDate IS NULL) >= r.Capacity
                                  THEN N'Occupied' ELSE N'Available' END AS DisplayStatus
                      FROM dbo.tblRooms r
                      LEFT JOIN dbo.tblRoomTypes rt ON rt.Code = r.RoomType AND rt.IsDeleted = 0
                      WHERE r.IsActive = 1
                        AND (@search = N'' OR r.RoomNo LIKE N'%' + @search + N'%'
                             OR r.RoomType LIKE N'%' + @search + N'%'
                             OR rt.Name LIKE N'%' + @search + N'%'
                             OR CASE WHEN r.Status IN (N'Maintenance', N'Reserved') THEN r.Status
                                     WHEN (SELECT COUNT(*) FROM dbo.tblRoomAssignments ra
                                           WHERE ra.RoomNo = r.RoomNo AND ra.DischargeDate IS NULL) >= r.Capacity
                                     THEN N'Occupied' ELSE N'Available' END LIKE N'%' + @search + N'%')
                      ORDER BY r.RoomNo;",
                    new SqlParameter("@search", txtSearch.Text.Trim()));

                foreach (DataRow room in rooms.Rows)
                {
                    int occupied = Convert.ToInt32(room["OccupiedBeds"]);
                    int capacity = Convert.ToInt32(room["Capacity"]);
                    dgvRooms.Rows.Add(room["RoomNo"], room["RoomTypeName"],
                        $"{occupied} / {capacity} occupied, {Math.Max(0, capacity - occupied)} free",
                        Convert.ToDecimal(room["Rate"]).ToString("0.00"), room["DisplayStatus"], capacity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load rooms: {ex.Message}", "Room Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object? sender, EventArgs e) => LoadRoomData();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNo.Text) || cmbRoomType.SelectedValue is not string roomType)
            {
                MessageBox.Show("Room number and room type are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int inserted = Convert.ToInt32(DbHelper.ExecuteScalar(
                    "EXEC dbo.usp_tblRooms_Create @RoomNo, @RoomType, @Rate, @Capacity",
                    new SqlParameter("@RoomNo", txtRoomNo.Text.Trim()),
                    new SqlParameter("@RoomType", roomType),
                    new SqlParameter("@Rate", numRate.Value),
                    new SqlParameter("@Capacity", decimal.ToInt32(_capacity.Value))));
                if (inserted == 0)
                    throw new InvalidOperationException("The room could not be created.");
                LoadRoomData();
                ClearForm();
                MessageBox.Show("Room added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to add room: {ex.Message}", "Room Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count == 0 || cmbRoomType.SelectedValue is not string roomType)
            {
                MessageBox.Show("Select a room and room type before updating.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int updated = Convert.ToInt32(DbHelper.ExecuteScalar(
                    "EXEC dbo.usp_tblRooms_Update @RoomNo, @RoomType, @Rate, @Capacity",
                    new SqlParameter("@RoomNo", txtRoomNo.Text.Trim()),
                    new SqlParameter("@RoomType", roomType),
                    new SqlParameter("@Rate", numRate.Value),
                    new SqlParameter("@Capacity", decimal.ToInt32(_capacity.Value))));
                if (updated == 0)
                    throw new InvalidOperationException("The selected room was not found or is inactive.");
                if (cmbAvailability.SelectedItem is string status && status != "Occupied")
                {
                    int rows;
                    if (status == "Available")
                    {
                        rows = DbHelper.Execute(
                            @"UPDATE dbo.tblRooms
                              SET Status = CASE WHEN (SELECT COUNT(*) FROM dbo.tblRoomAssignments
                                                       WHERE RoomNo = @RoomNo AND DischargeDate IS NULL) >= Capacity
                                                THEN N'Occupied' ELSE N'Available' END,
                                  UpdatedAt = SYSUTCDATETIME()
                              WHERE RoomNo = @RoomNo AND IsActive = 1;",
                            new SqlParameter("@RoomNo", txtRoomNo.Text.Trim()));
                    }
                    else
                    {
                        rows = DbHelper.Execute(
                            @"UPDATE dbo.tblRooms
                              SET Status = @Status, UpdatedAt = SYSUTCDATETIME()
                              WHERE RoomNo = @RoomNo AND IsActive = 1
                                AND NOT EXISTS
                                (
                                    SELECT 1 FROM dbo.tblRoomAssignments
                                    WHERE RoomNo = @RoomNo AND DischargeDate IS NULL
                                );",
                            new SqlParameter("@Status", status),
                            new SqlParameter("@RoomNo", txtRoomNo.Text.Trim()));
                    }
                    if (rows == 0)
                        throw new InvalidOperationException("Room status cannot be changed while patients are admitted.");
                }
                LoadRoomData();
                MessageBox.Show("Room updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to update room: {ex.Message}", "Room Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a room to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string roomNo = dgvRooms.SelectedRows[0].Cells["colRoomNo"].Value?.ToString() ?? string.Empty;
            if (MessageBox.Show($"Deactivate room {roomNo}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                DbHelper.Execute("EXEC dbo.usp_tblRooms_Delete @RoomNo", new SqlParameter("@RoomNo", roomNo));
                LoadRoomData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to delete room: {ex.Message}", "Room Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            txtRoomNo.Clear();
            if (cmbRoomType.Items.Count > 0) cmbRoomType.SelectedIndex = 0;
            txtBedNo.Text = "0 / 1 occupied, 1 free";
            numRate.Value = 0;
            _capacity.Value = 1;
            cmbAvailability.SelectedIndex = -1;
            dgvRooms.ClearSelection();
        }

        private void dgvRooms_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvRooms.SelectedRows[0];
            txtRoomNo.Text = row.Cells["colRoomNo"].Value?.ToString();
            string roomTypeName = row.Cells["colRoomType"].Value?.ToString() ?? string.Empty;
            for (int i = 0; i < cmbRoomType.Items.Count; i++)
            {
                if (cmbRoomType.GetItemText(cmbRoomType.Items[i]) == roomTypeName)
                {
                    cmbRoomType.SelectedIndex = i;
                    break;
                }
            }
            txtBedNo.Text = row.Cells["colBedNo"].Value?.ToString() ?? string.Empty;
            _capacity.Value = Convert.ToInt32(row.Cells["colCapacity"].Value);
            if (decimal.TryParse(row.Cells["colRate"].Value?.ToString(), out decimal rate)) numRate.Value = rate;
            cmbAvailability.SelectedItem = row.Cells["colStatus"].Value?.ToString();
        }

        private void InitializeCapacityEditor()
        {
            _capacity.Minimum = 1;
            _capacity.Maximum = 100;
            _capacity.Value = 1;
            _capacity.Font = new System.Drawing.Font("Segoe UI", 10F);
            _capacity.Location = new System.Drawing.Point(160, 360);
            _capacity.Size = new System.Drawing.Size(290, 31);
            _capacity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            var label = new Label
            {
                Text = "Bed Capacity:",
                AutoSize = true,
                Location = new System.Drawing.Point(25, 365),
                Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold)
            };
            grpRoomDetails.Controls.Add(label);
            grpRoomDetails.Controls.Add(_capacity);
            btnClear.Location = new System.Drawing.Point(btnClear.Left, 415);
        }
    }
}
