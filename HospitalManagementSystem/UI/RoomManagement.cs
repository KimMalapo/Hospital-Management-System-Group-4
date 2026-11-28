using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using BusinessLogic;
using Microsoft.VisualBasic;

namespace UI
{
    /// <summary>Old room window. Nothing opens it now.</summary>
    public partial class RoomManagement : Form
    {
        private readonly RoomService? _service;
        private readonly LookupService? _lookupService;

        public RoomManagement()
        {
            InitializeComponent();
            UITheme.Apply(this);
            var cs = Config.GetConnectionString();
            if (!string.IsNullOrWhiteSpace(cs))
            {
                _service = new RoomService(cs);
                _lookupService = new LookupService(cs);
            }

            this.btnAdd.Click += BtnAdd_Click;
            this.btnEdit.Click += BtnEdit_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.btnSearch.Click += BtnSearch_Click;

            RefreshRoomList();
        }

        // The four parallel lists that together show one room per row.
        private ListView[] RoomLists => new[] { listRoomNo, listType, listRate, listStatus };

        private static string PendingRoomsFile => Path.Combine(Path.GetTempPath(), "HMS_pending_rooms.txt");

        // Read room list
        public DataTable LoadRooms(string sql = "SELECT * FROM dbo.tblRoomTypes")
        {
            try
            {
                return _service != null ? _service.GetRooms(sql) : new DataTable();
            }
            catch (Exception logEx)
            {
                Logger.Error("RoomManagement.LoadRooms failed", logEx);
                return new DataTable();
            }
        }

        // Create a room record using parameterized insert SQL
        public int CreateRoom(string insertSql, params SqlParameter[] parameters)
        {
            // If service is not available, persist the operation to a temp file for later reconciliation
            if (_service == null)
                return SaveToTempFile(insertSql, parameters) ? 1 : 0;

            try
            {
                return _service.CreateRoom(insertSql, parameters);
            }
            catch (Exception ex)
            {
                // On DB errors (connection, etc.) fall back to writing the pending operation to a temp file
                Logger.Error("RoomManagement.CreateRoom: database error, saving to temp file", ex);
                SaveToTempFile(insertSql, parameters);
                return 1;
            }
        }

        private bool SaveToTempFile(string sql, SqlParameter[] parameters)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("=== " + DateTime.UtcNow.ToString("o") + " ===");
                sb.AppendLine(sql);
                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        sb.AppendLine($"{p.ParameterName}={p.Value}");
                    }
                }
                sb.AppendLine();
                File.AppendAllText(PendingRoomsFile, sb.ToString());
                return true;
            }
            catch (Exception logEx)
            {
                Logger.Error("RoomManagement.SaveToTempFile failed", logEx);
                return false;
            }
        }

        private void AddRoomRow(string roomNo, string type, string rate, string status)
        {
            listRoomNo.Items.Add(roomNo);
            listType.Items.Add(type);
            listRate.Items.Add(rate);
            listStatus.Items.Add(status);
        }

        private void RefreshRoomList()
        {
            try
            {
                foreach (var list in RoomLists) list.Items.Clear();

                if (_service != null)
                    LoadRoomRowsFromDatabase();
                else
                    LoadRoomRowsFromTempFile();
            }
            catch (Exception logEx)
            {
                // ignore refresh errors to avoid crashing UI
                Logger.Error("RoomManagement.RefreshRoomList failed", logEx);
            }
        }

        private void LoadRoomRowsFromDatabase()
        {
            var rooms = LoadRooms("SELECT RoomNo, Type, Rate, Status FROM dbo.tblRooms");
            foreach (DataRow r in rooms.Rows)
            {
                AddRoomRow(
                    r["RoomNo"]?.ToString() ?? string.Empty,
                    r["Type"]?.ToString() ?? string.Empty,
                    r["Rate"]?.ToString() ?? string.Empty,
                    r["Status"]?.ToString() ?? string.Empty);
            }
        }

        // No DB - show pending entries from temp file if any
        private void LoadRoomRowsFromTempFile()
        {
            if (!File.Exists(PendingRoomsFile)) return;

            var content = File.ReadAllText(PendingRoomsFile);
            var blocks = content.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                // look for parameter lines like @roomNo=, @type=, @rate=, @status=
                string? room = null, type = null, rate = null, status = null;
                var lines = block.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var idx = line.IndexOf('=');
                    if (idx <= 0) continue;
                    var key = line.Substring(0, idx).Trim();
                    var val = line.Substring(idx + 1).Trim();
                    switch (key.ToLowerInvariant())
                    {
                        case "@roomno": room = val; break;
                        case "@type": type = val; break;
                        case "@rate": rate = val; break;
                        case "@status": status = val; break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(room))
                    AddRoomRow(room, type ?? string.Empty, rate ?? string.Empty, status ?? string.Empty);
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                var roomNo = Interaction.InputBox("Enter Room No:", "Add Room", "");
                if (string.IsNullOrWhiteSpace(roomNo)) return;

                var type = Interaction.InputBox("Enter Room Type:", "Add Room", "");
                if (string.IsNullOrWhiteSpace(type)) return;

                var rate = Interaction.InputBox("Enter Rate:", "Add Room", "0");
                var status = Interaction.InputBox("Enter Status (e.g. Available/Occupied):", "Add Room", "Available");

                var sql = "INSERT INTO dbo.tblRooms (RoomNo, Type, Rate, Status) VALUES (@roomNo, @type, @rate, @status)";
                var rows = CreateRoom(sql,
                    new SqlParameter("@roomNo", roomNo),
                    new SqlParameter("@type", type),
                    new SqlParameter("@rate", rate),
                    new SqlParameter("@status", status)
                );

                if (rows > 0)
                {
                    UIHelper.ShowInfo("Room added.", "Success");
                    if (_service != null)
                        RefreshRoomList();
                    else
                        AddRoomRow(roomNo, type, rate, status); // show the pending entry immediately
                }
                else
                {
                    UIHelper.ShowError("No rows affected.", "Error");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("RoomManagement: Error adding room", ex);
                UIHelper.ShowError("Error adding room: " + ex.Message, "Error");
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            try
            {
                if (listRoomNo.SelectedItems.Count == 0)
                {
                    UIHelper.ShowWarning("Select a room from the Room No. list to edit.", "Validation");
                    return;
                }

                var selectedIndex = listRoomNo.SelectedItems[0].Index;
                var selected = listRoomNo.SelectedItems[0].Text;
                var currentType = listType.Items.Count > selectedIndex ? listType.Items[selectedIndex].Text : string.Empty;
                var currentRate = listRate.Items.Count > selectedIndex ? listRate.Items[selectedIndex].Text : "0";
                var currentStatus = listStatus.Items.Count > selectedIndex ? listStatus.Items[selectedIndex].Text : string.Empty;

                var type = Interaction.InputBox("Edit Room Type:", "Edit Room", currentType);
                var rate = Interaction.InputBox("Edit Rate:", "Edit Room", currentRate);
                var status = Interaction.InputBox("Edit Status:", "Edit Room", currentStatus);
                var sql = "UPDATE dbo.tblRooms SET Type=@type, Rate=@rate, Status=@status WHERE RoomNo=@roomNo";
                var rows = CreateRoom(sql,
                    new SqlParameter("@type", type),
                    new SqlParameter("@rate", rate),
                    new SqlParameter("@status", status),
                    new SqlParameter("@roomNo", selected)
                );

                if (rows > 0)
                {
                    UIHelper.ShowInfo("Room updated.", "Success");
                    if (_service != null)
                        RefreshRoomList();
                    else
                    {
                        // update the visible lists for the pending entry
                        if (listType.Items.Count > selectedIndex) listType.Items[selectedIndex].Text = type;
                        if (listRate.Items.Count > selectedIndex) listRate.Items[selectedIndex].Text = rate;
                        if (listStatus.Items.Count > selectedIndex) listStatus.Items[selectedIndex].Text = status;
                    }
                }
                else
                {
                    UIHelper.ShowError("No rows affected.", "Error");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("RoomManagement: Error updating room", ex);
                UIHelper.ShowError("Error updating room: " + ex.Message, "Error");
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (listRoomNo.SelectedItems.Count == 0)
                {
                    UIHelper.ShowInfo("Please select a room to delete.", "Information");
                    return;
                }

                var selectedIndex = listRoomNo.SelectedItems[0].Index;
                var selected = listRoomNo.SelectedItems[0].Text;

                var confirm = MessageBox.Show($"Are you sure you want to delete room '{selected}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                if (_service == null)
                {
                    // fallback - write delete SQL to temp file for later reconciliation
                    var sql = "DELETE FROM dbo.tblRooms WHERE RoomNo = @roomNo";
                    SaveToTempFile(sql, new SqlParameter[] { new SqlParameter("@roomNo", selected) });
                    UIHelper.ShowInfo("Delete recorded for later execution (no DB connection).", "Info");
                    foreach (var list in RoomLists)
                    {
                        if (list.Items.Count > selectedIndex) list.Items.RemoveAt(selectedIndex);
                    }
                    return;
                }

                var rows = _service.DeleteRoom(selected);
                if (rows > 0)
                {
                    UIHelper.ShowInfo("Room deleted.", "Success");
                    RefreshRoomList();
                }
                else
                {
                    UIHelper.ShowError("No rows affected.", "Error");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("RoomManagement: Error deleting room", ex);
                UIHelper.ShowError("Error deleting room: " + ex.Message, "Error");
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                var searchTerm = txtSearch.Text.Trim();
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    UIHelper.ShowWarning("Please enter a search term (Code or Name).", "Validation");
                    return;
                }

                if (_lookupService == null)
                {
                    UIHelper.ShowError("Search is not available without a database connection.", "Error");
                    return;
                }

                // Search in tblRoomTypes lookup table
                var results = _lookupService.Search("RoomTypes", searchTerm);

                if (results.Count == 0)
                {
                    UIHelper.ShowInfo("No matching records found.", "Search Results");
                    return;
                }

                foreach (var list in RoomLists) list.Items.Clear();

                foreach (var item in results)
                {
                    AddRoomRow(item.Code ?? string.Empty, item.Name ?? string.Empty,
                        item.Status ?? string.Empty, item.Notes ?? string.Empty);
                }

                UIHelper.ShowInfo($"Found {results.Count} matching record(s).", "Search Results");
            }
            catch (Exception ex)
            {
                Logger.Error("RoomManagement: Error during search", ex);
                UIHelper.ShowError("Error during search: " + ex.Message, "Error");
            }
        }
    }
}
