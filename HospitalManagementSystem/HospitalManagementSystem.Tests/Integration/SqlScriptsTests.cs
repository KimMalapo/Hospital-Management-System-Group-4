using Microsoft.Data.SqlClient;
using Xunit;

namespace HospitalManagementSystem.Tests.Integration;

[Collection("LocalDb")]
public class SqlScriptsTests
{
    private readonly LocalDbFixture _db;
    public SqlScriptsTests(LocalDbFixture db) => _db = db;

    private static readonly string[] ExpectedTables =
    {
        "tblPatients", "tblUsers", "tblRooms", "tblRoomTypes", "tblRoomAssignments", "tblDiagnosisCodes",
        "tblDrugCategories", "tblDepartmentTypes", "tblAuditLog",
        "tblAdmissions", "tblServices", "tblAdmissionCharges", "tblBills", "tblBillItems", "tblPayments"
    };

    private static readonly string[] ExpectedProcedures =
    {
        "usp_tblPatients_Create", "usp_tblRooms_Create", "usp_tblRoomAssignments_Assign",
        "usp_tblAdmissions_Admit", "usp_tblAdmissions_Discharge", "usp_tblAdmissions_GetActive",
        "usp_tblServices_GetAll", "usp_tblAdmissionCharges_Add", "usp_tblAdmissionCharges_Void",
        "usp_tblBills_Generate", "usp_tblBills_GetByAdmission", "usp_tblPayments_Post", "usp_tblPayments_Void",
        "usp_Receipt_GetByPayment", "usp_Report_BillingSummary", "usp_Dashboard_Counts",
        "usp_tblDiagnosisCodes_Get", "usp_tblDiagnosisCodes_GetById", "usp_tblDiagnosisCodes_Search",
        "usp_tblDiagnosisCodes_Create", "usp_tblDiagnosisCodes_Update", "usp_tblDiagnosisCodes_SoftDelete",
        "usp_tblDiagnosisCodes_CheckCodeExists", "usp_tblDrugCategories_Get", "usp_tblDrugCategories_GetById",
        "usp_tblDrugCategories_Search", "usp_tblDrugCategories_Create", "usp_tblDrugCategories_Update",
        "usp_tblDrugCategories_SoftDelete", "usp_tblDrugCategories_CheckCodeExists", "usp_tblRoomTypes_Get",
        "usp_tblRoomTypes_GetById", "usp_tblRoomTypes_Search", "usp_tblRoomTypes_Create",
        "usp_tblRoomTypes_Update", "usp_tblRoomTypes_SoftDelete", "usp_tblRoomTypes_CheckCodeExists",
        "usp_tblDepartmentTypes_Get", "usp_tblDepartmentTypes_GetById", "usp_tblDepartmentTypes_Search",
        "usp_tblDepartmentTypes_Create", "usp_tblDepartmentTypes_Update", "usp_tblDepartmentTypes_SoftDelete",
        "usp_tblDepartmentTypes_CheckCodeExists"
    };

    [SkippableFact]
    public void All_scripts_run_without_errors()
    {
        Skip.IfNot(_db.LocalDbAvailable, _db.SkipReason);
        Assert.True(_db.ScriptErrors.Count == 0, "Script errors:" + Environment.NewLine + string.Join(Environment.NewLine, _db.ScriptErrors));
    }

    [SkippableFact]
    public void Expected_tables_and_procedures_exist()
    {
        Skip.IfNot(_db.LocalDbAvailable, _db.SkipReason);

        var tables = Names("SELECT name FROM sys.tables");
        var procs = Names("SELECT name FROM sys.procedures");

        var missing = ExpectedTables.Where(t => !tables.Contains(t)).Select(t => "table " + t)
            .Concat(ExpectedProcedures.Where(p => !procs.Contains(p)).Select(p => "procedure " + p))
            .ToList();

        Assert.True(missing.Count == 0, "Missing objects: " + string.Join(", ", missing));
    }

    [SkippableFact]
    public void Post_deployment_script_can_run_twice()
    {
        Skip.IfNot(_db.LocalDbAvailable, _db.SkipReason);
        var before = _db.ScriptErrors.Count;

        _db.RunPostDeployment();   // second run: must not fail or create duplicates

        Assert.Equal(before, _db.ScriptErrors.Count);
        Assert.Equal(1, Scalar("SELECT COUNT(*) FROM dbo.tblRoomTypes WHERE Code = 'GEN'"));
        Assert.Equal(1, Scalar("SELECT COUNT(*) FROM dbo.tblRoomTypes WHERE Code = 'ICU'"));
        Assert.Equal(1, Scalar("SELECT COUNT(*) FROM dbo.tblRoomTypes WHERE Code = 'PRV'"));
        Assert.Equal(1, Scalar("SELECT COUNT(*) FROM dbo.tblDepartmentTypes WHERE Code = 'EMR'"));
        Assert.Equal(1, Scalar("SELECT COUNT(*) FROM dbo.tblDiagnosisCodes WHERE Code = 'A00'"));
        Assert.Equal(1, Scalar("SELECT COUNT(*) FROM dbo.tblDrugCategories WHERE Code = 'ANTIB'"));
    }

    private HashSet<string> Names(string sql)
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        using var conn = new SqlConnection(_db.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        using var r = cmd.ExecuteReader();
        while (r.Read()) set.Add(r.GetString(0));
        return set;
    }

    private int Scalar(string sql)
    {
        using var conn = new SqlConnection(_db.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}
