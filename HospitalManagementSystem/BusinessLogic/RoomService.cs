using System.Data;
using Microsoft.Data.SqlClient;

namespace BusinessLogic
{
    public class RoomService
    {
        private readonly DbExecutor _db;

        public RoomService(string connectionString)
        {
            _db = new DbExecutor(connectionString);
        }

        public DataTable GetRooms(string sql = "SELECT * FROM dbo.tblRoomTypes")
        {
            return _db.Query(sql);
        }

        public int CreateRoom(string insertSql, params SqlParameter[] parameters)
        {
            return _db.Execute(insertSql, parameters);
        }
    }
}
