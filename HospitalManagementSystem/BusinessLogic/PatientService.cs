using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Model;

namespace BusinessLogic
{
    public class PatientService
    {
        private readonly DbExecutor _db;

        public PatientService(string connectionString)
        {
            _db = new DbExecutor(connectionString);
        }

        public DataTable GetAll() =>
            _db.QuerySP("usp_tblPatients_GetAll");

        public DataTable GetPatients(string? searchTerm = null) =>
            string.IsNullOrWhiteSpace(searchTerm) ? GetAll() : Search(searchTerm.Trim());

        public Patient? GetById(int id)
        {
            var dt = _db.QuerySP("usp_tblPatients_GetById",
                new SqlParameter("@PatientID", id));
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        public DataTable Search(string term) =>
            _db.QuerySP("usp_tblPatients_Search",
                new SqlParameter("@SearchTerm", term ?? ""));

        public int Create(Patient p)
        {
            PatientValidator.Validate(p);
            try
            {
                var result = _db.ExecuteScalarSP("usp_tblPatients_Create",
                    new SqlParameter("@FirstName", p.FirstName ?? (object)DBNull.Value),
                    new SqlParameter("@LastName", p.LastName ?? (object)DBNull.Value),
                    new SqlParameter("@DateOfBirth", p.DateOfBirth ?? (object)DBNull.Value),
                    new SqlParameter("@Gender", p.Gender ?? (object)DBNull.Value),
                    new SqlParameter("@ContactNumber", p.ContactNumber ?? (object)DBNull.Value),
                    new SqlParameter("@Address", p.Address ?? (object)DBNull.Value),
                    new SqlParameter("@Email", p.Email ?? (object)DBNull.Value));

                return result == null ? 0 : Convert.ToInt32(result);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Duplicate PatientID. A patient with this ID already exists.");
            }
        }

        public int Update(Patient p)
        {
            PatientValidator.Validate(p);
            var result = _db.QuerySP("usp_tblPatients_Update",
                new SqlParameter("@PatientID", p.PatientID),
                new SqlParameter("@FirstName", p.FirstName ?? (object)DBNull.Value),
                new SqlParameter("@LastName", p.LastName ?? (object)DBNull.Value),
                new SqlParameter("@DateOfBirth", p.DateOfBirth ?? (object)DBNull.Value),
                new SqlParameter("@Gender", p.Gender ?? (object)DBNull.Value),
                new SqlParameter("@ContactNumber", p.ContactNumber ?? (object)DBNull.Value),
                new SqlParameter("@Address", p.Address ?? (object)DBNull.Value),
                new SqlParameter("@Email", p.Email ?? (object)DBNull.Value));

            return result.Rows.Count > 0 ? Convert.ToInt32(result.Rows[0]["AffectedRows"]) : 0;
        }

        public int Delete(int id)
        {
            var result = _db.QuerySP("usp_tblPatients_Delete",
                new SqlParameter("@PatientID", id));

            return result.Rows.Count > 0 ? Convert.ToInt32(result.Rows[0]["AffectedRows"]) : 0;
        }

        private static Patient Map(DataRow row) => new Patient
        {
            PatientID = Convert.ToInt32(row["PatientID"]),
            FirstName = row["FirstName"] as string,
            LastName = row["LastName"] as string,
            DateOfBirth = row["DateOfBirth"] as DateTime?,
            Gender = row["Gender"] as string,
            ContactNumber = row["ContactNumber"] as string,
            Address = row["Address"] as string,
            Email = row["Email"] as string,
            IsActive = row.Table.Columns.Contains("IsActive") && row["IsActive"] != DBNull.Value
                        ? Convert.ToBoolean(row["IsActive"]) : true
        };
    }
}