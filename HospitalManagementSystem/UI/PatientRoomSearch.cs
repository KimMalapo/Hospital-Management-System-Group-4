using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BusinessLogic;

namespace UI
{
    public partial class PatientRoomSearch : Form
    {
        private readonly RoomService? _service;

        public PatientRoomSearch()
        {
            InitializeComponent();
            var cs = Config.GetConnectionString();
            if (!string.IsNullOrWhiteSpace(cs)) _service = new RoomService(cs);
        }

        // Read rooms assigned to patients
        public DataTable LoadPatientRooms(string sql = "SELECT * FROM dbo.tblRooms")
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
    }
}
