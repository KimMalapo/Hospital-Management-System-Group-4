using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BusinessLogic;
using Microsoft.VisualBasic;
using System.IO;

namespace UI
{
    public partial class RoomManagement : Form
    {
        private readonly RoomService? _service;

        public RoomManagement()
        {
            InitializeComponent();
            var cs = Config.GetConnectionString();
            if (!string.IsNullOrWhiteSpace(cs)) _service = new RoomService(cs);

            // wire buttons
            this.btnAdd.Click += BtnAdd_Click;
            this.btnEdit.Click += BtnEdit_Click;

            // initial load
            RefreshRoomList();
        }

        // Read room list
        public DataTable LoadRooms(string sql = "SELECT * FROM dbo.tblRoomTypes")
        {
            try
            {
                return _service != null ? _service.GetRooms(sql) : new DataTable();
            }
            catch
            {
                return new DataTable();
            }
        }

        // Create a room record using parameterized insert SQL
        public int CreateRoom(string insertSql, params SqlParameter[] parameters)
        {
            // If service is not available, persist the operation to a temp file for later reconciliation
            if (_service == null)
            {
                var ok = SaveToTempFile(insertSql, parameters);
                return ok ? 1 : 0;
            }

            try
            {
                return _service != null ? _service.CreateRoom(insertSql, parameters) : 0;
            }
            catch (Exception ex)
            {
                // On DB errors (connection, etc.) fall back to writing the pending operation to a temp file
                try
                {
                    SaveToTempFile(insertSql, parameters);
                    return 1;
                }
                catch
                {
                    // failed to persist to temp file as well
                    return 0;
                }
            }
        }

        private bool SaveToTempFile(string sql, SqlParameter[] parameters)
        {
            try
            {
                var temp = Path.Combine(Path.GetTempPath(), "HMS_pending_rooms.txt");
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
                File.AppendAllText(temp, sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void RefreshRoomList()
        {
            try
            {
                listRoomNo.Items.Clear();
                listType.Items.Clear();
                listRate.Items.Clear();
                listStatus.Items.Clear();

                if (_service != null)
                {
                    var dt = LoadRooms("SELECT RoomNo, Type, Rate, Status FROM dbo.tblRooms");
                    foreach (DataRow r in dt.Rows)
                    {
                        listRoomNo.Items.Add(r["RoomNo"]?.ToString() ?? string.Empty);
                        listType.Items.Add(r["Type"]?.ToString() ?? string.Empty);
                        listRate.Items.Add(r["Rate"]?.ToString() ?? string.Empty);
                        listStatus.Items.Add(r["Status"]?.ToString() ?? string.Empty);
                    }
                }
                else
                {
                    // no DB - show pending entries from temp file if any
                    var temp = Path.Combine(Path.GetTempPath(), "HMS_pending_rooms.txt");
                    if (File.Exists(temp))
                    {
                        var content = File.ReadAllText(temp);
                        var blocks = content.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var block in blocks)
                        {
                            // look for parameter lines like @roomNo=, @type=, @rate=, @status=
                            string room = null, type = null, rate = null, status = null;
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
                            {
                                listRoomNo.Items.Add(room);
                                listType.Items.Add(type ?? string.Empty);
                                listRate.Items.Add(rate ?? string.Empty);
                                listStatus.Items.Add(status ?? string.Empty);
                            }
                        }
                    }
                }
            }
            catch
            {
                // ignore refresh errors to avoid crashing UI
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
                    MessageBox.Show("Room added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // refresh UI list immediately
                    if (_service != null)
                        RefreshRoomList();
                    else
                    {
                        // show the pending entry immediately
                        listRoomNo.Items.Add(roomNo);
                        listType.Items.Add(type);
                        listRate.Items.Add(rate);
                        listStatus.Items.Add(status);
                    }
                }
                else
                {
                    MessageBox.Show("No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding room: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            try
            {
                if (listRoomNo.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Select a room from the Room No. list to edit.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedIndex = listRoomNo.SelectedItems[0].Index;
                var selected = listRoomNo.SelectedItems[0].Text;
                var currentType = listType.Items.Count > selectedIndex ? listType.Items[selectedIndex].Text : string.Empty;
                var currentRate = listRate.Items.Count > selectedIndex ? listRate.Items[selectedIndex].Text : "0";
                var currentStatus = listStatus.Items.Count > selectedIndex ? listStatus.Items[selectedIndex].Text : string.Empty;

                var type = Interaction.InputBox("Edit Room Type:", "Edit Room", currentType);
                if (type == null) return;
                var rate = Interaction.InputBox("Edit Rate:", "Edit Room", currentRate);
                if (rate == null) return;
                var status = Interaction.InputBox("Edit Status:", "Edit Room", currentStatus);
                if (status == null) return;
                var sql = "UPDATE dbo.tblRooms SET Type=@type, Rate=@rate, Status=@status WHERE RoomNo=@roomNo";
                var rows = CreateRoom(sql,
                    new SqlParameter("@type", type),
                    new SqlParameter("@rate", rate),
                    new SqlParameter("@status", status),
                    new SqlParameter("@roomNo", selected)
                );

                if (rows > 0)
                {
                    MessageBox.Show("Room updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating room: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
