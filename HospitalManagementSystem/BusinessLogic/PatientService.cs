using System.Data;
using Microsoft.Data.SqlClient;

namespace BusinessLogic
{
    public class PatientService
    {
        private readonly DbExecutor _db;

        public PatientService(string connectionString)
        {
            _db = new DbExecutor(connectionString);
        }

        // Simple read; caller can provide WHERE clause via sql parameter if desired
        public DataTable GetPatients(string sql = "SELECT * FROM dbo.tblPatients")
        {
            return _db.Query(sql);
        }

        // Insert using parameterized SQL
        public int CreatePatient(string insertSql, params SqlParameter[] parameters)
        {
            return _db.Execute(insertSql, parameters);
        }
    }
}
