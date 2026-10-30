using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BusinessLogic
{
    public class RoomService
    {
        private readonly DbExecutor _db;
        private readonly LookupService _lookup;

        public RoomService(string connectionString)
        {
            _db = new DbExecutor(connectionString);
            _lookup = new LookupService(connectionString);
        }

        public DataTable GetAll() =>
            _db.QuerySP("usp_tblRooms_GetAll");

        // Old name the UI is calling
        public DataTable GetRooms(string sql = null) => GetAll();

        public DataTable GetById(string roomNo) =>
            _db.QuerySP("usp_tblRooms_GetById",
                new SqlParameter("@RoomNo", roomNo));

        public int Create(string roomNo, string roomTypeCode, decimal rate)
        {
            if (string.IsNullOrWhiteSpace(roomNo))
                throw new ArgumentException("Room number is required");
            if (rate < 0)
                throw new ArgumentException("Rate must be 0 or greater");

            var types = _lookup.GetAll("RoomTypes");
            var type = types.Find(t =>
                t.Code != null &&
                t.Code.Equals(roomTypeCode, StringComparison.OrdinalIgnoreCase) &&
                t.Status == "Active" &&
                !t.IsDeleted);

            if (type == null)
                throw new InvalidOperationException($"Room type '{roomTypeCode}' does not exist or is not active");

            return _db.ExecuteSP("usp_tblRooms_Create",
                new SqlParameter("@RoomNo", roomNo),
                new SqlParameter("@RoomType", roomTypeCode),
                new SqlParameter("@Rate", rate));
        }

        // Old name the UI is calling
        public int CreateRoom(string insertSql, params SqlParameter[] parameters) =>
            _db.Execute(insertSql, parameters);

        public int Update(string roomNo, string roomTypeCode, decimal rate)
        {
            if (rate < 0) throw new ArgumentException("Rate must be 0 or greater");

            var types = _lookup.GetAll("RoomTypes");
            var type = types.Find(t =>
                t.Code != null &&
                t.Code.Equals(roomTypeCode, StringComparison.OrdinalIgnoreCase) &&
                t.Status == "Active" &&
                !t.IsDeleted);

            if (type == null)
                throw new InvalidOperationException($"Room type '{roomTypeCode}' does not exist or is not active");

            return _db.ExecuteSP("usp_tblRooms_Update",
                new SqlParameter("@RoomNo", roomNo),
                new SqlParameter("@RoomType", roomTypeCode),
                new SqlParameter("@Rate", rate));
        }

        public int Delete(string roomNo) =>
            _db.ExecuteSP("usp_tblRooms_Delete",
                new SqlParameter("@RoomNo", roomNo));

        // Old name the UI is calling
        public int DeleteRoom(string roomNo) => Delete(roomNo);
    }
}