using System.Data;
using Microsoft.Data.SqlClient;
using Model;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class RoomService
    {
        private readonly DbExecutor _db;
        private readonly LookupService _lookupService;

        public RoomService(string connectionString)
        {
            _db = new DbExecutor(connectionString);
            _lookupService = new LookupService(connectionString);
        }

        public DataTable GetRooms(string sql = "SELECT * FROM dbo.tblRoomTypes") => _db.Query(sql);

        public int CreateRoom(string insertSql, params SqlParameter[] parameters) => _db.Execute(insertSql, parameters);

        public int DeleteRoom(string roomNo) => _db.Execute("DELETE FROM dbo.tblRooms WHERE RoomNo = @roomNo", new SqlParameter("@roomNo", roomNo));

        /// <summary>
        /// Search for room types by Code or Name
        /// </summary>
        public List<LookupItem> SearchRoomTypes(string searchTerm) => _lookupService.Search("RoomTypes", searchTerm);
    }
}
