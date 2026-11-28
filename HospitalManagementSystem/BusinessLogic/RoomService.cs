using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BusinessLogic
{
    /// <summary>Rooms: list, add, update, delete. The room type has to be active.</summary>
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

        private void EnsureActiveRoomType(string roomTypeCode)
        {
            var isActive = _lookup.GetAll("RoomTypes").Any(t =>
                t.Code != null &&
                t.Code.Equals(roomTypeCode, StringComparison.OrdinalIgnoreCase) &&
                t.Status == "Active" &&
                !t.IsDeleted);

            if (!isActive)
                throw new InvalidOperationException($"Room type '{roomTypeCode}' does not exist or is not active");
        }

        public int Create(string roomNo, string roomTypeCode, decimal rate, int capacity = 1)
        {
            if (string.IsNullOrWhiteSpace(roomNo))
                throw new ArgumentException("Room number is required");
            if (rate < 0)
                throw new ArgumentException("Rate must be 0 or greater");
            if (capacity < 1)
                throw new ArgumentException("Room capacity must be at least one bed.");

            EnsureActiveRoomType(roomTypeCode);

            return _db.ExecuteSP("usp_tblRooms_Create",
                new SqlParameter("@RoomNo", roomNo),
                new SqlParameter("@RoomType", roomTypeCode),
                new SqlParameter("@Rate", rate),
                new SqlParameter("@Capacity", capacity));
        }

        // Old name the UI is calling
        public int CreateRoom(string insertSql, params SqlParameter[] parameters) =>
            _db.Execute(insertSql, parameters);

        public int Update(string roomNo, string roomTypeCode, decimal rate, int capacity = 1)
        {
            if (rate < 0) throw new ArgumentException("Rate must be 0 or greater");
            if (capacity < 1) throw new ArgumentException("Room capacity must be at least one bed.");

            EnsureActiveRoomType(roomTypeCode);

            return _db.ExecuteSP("usp_tblRooms_Update",
                new SqlParameter("@RoomNo", roomNo),
                new SqlParameter("@RoomType", roomTypeCode),
                new SqlParameter("@Rate", rate),
                new SqlParameter("@Capacity", capacity));
        }

        public int Delete(string roomNo) =>
            _db.ExecuteSP("usp_tblRooms_Delete",
                new SqlParameter("@RoomNo", roomNo));

        // Old name the UI is calling
        public int DeleteRoom(string roomNo) => Delete(roomNo);
    }
}