using System.Data;
using BusinessLogic;

namespace UI
{
    /// <summary>Old room search window. Nothing opens it now.</summary>
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
            catch (Exception logEx)
            {
                Logger.Error("PatientRoomSearch.LoadPatientRooms failed", logEx);
                return new DataTable();
            }
        }
    }
}
