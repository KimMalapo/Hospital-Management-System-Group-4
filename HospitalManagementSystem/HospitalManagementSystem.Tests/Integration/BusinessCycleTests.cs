using BusinessLogic;
using Microsoft.Data.SqlClient;
using Model;
using Xunit;

namespace HospitalManagementSystem.Tests.Integration;

/// <summary>
/// The main transaction through the services, against a real LocalDB database:
/// Patient -> Admission -> Charges -> Bill -> Payment -> Discharge, with the invalid cases along the way.
/// Needs the seed data (room type GEN, department EMR, diagnosis A00 and at least one active service).
/// </summary>
[Collection("LocalDb")]
public class BusinessCycleTests : IDisposable
{
    private readonly LocalDbFixture _db;

    public BusinessCycleTests(LocalDbFixture db)
    {
        _db = db;
        CurrentUser.Clear();
    }

    [SkippableFact]
    public void Room_capacity_allows_multiple_admissions_and_rejects_overflow()
    {
        Skip.IfNot(_db.LocalDbAvailable, _db.SkipReason);
        Assert.True(_db.ScriptErrors.Count == 0, "Fix the script errors first: " + string.Join("; ", _db.ScriptErrors));

        var cs = _db.ConnectionString;
        var patients = new PatientService(cs);
        var rooms = new RoomService(cs);
        var admissions = new AdmissionService(cs);
        var suffix = Guid.NewGuid().ToString("N")[..8];
        var roomNo = "CAP-" + suffix;

        var patientIds = Enumerable.Range(1, 3).Select(i => patients.Create(new Patient
        {
            FirstName = "Capacity" + i,
            LastName = "Test" + suffix,
            DateOfBirth = new DateTime(1980 + i, 1, 1),
            Gender = "Other"
        })).ToArray();
        rooms.Create(roomNo, "GEN", 800m, capacity: 2);

        admissions.Admit(patientIds[0], roomNo, "EMR", "A00", "Dr. Test", "Capacity check");
        admissions.Admit(patientIds[1], roomNo, "EMR", "A00", "Dr. Test", "Capacity check");

        var room = rooms.GetById(roomNo).Rows[0];
        Assert.Equal(2, Convert.ToInt32(room["OccupiedBeds"]));
        Assert.Equal(2, Convert.ToInt32(room["Capacity"]));
        Assert.Equal("Occupied", Convert.ToString(room["Status"]));
        Assert.Throws<ValidationException>(() =>
            admissions.Admit(patientIds[2], roomNo, "EMR", "A00", "Dr. Test", "Over capacity"));

        var assignmentId = Scalar(
            "SELECT MIN(AssignmentID) FROM dbo.tblRoomAssignments WHERE RoomNo = @RoomNo AND DischargeDate IS NULL",
            new SqlParameter("@RoomNo", roomNo));
        using (var connection = new SqlConnection(cs))
        {
            connection.Open();
            using var command = new SqlCommand("EXEC dbo.usp_tblRoomAssignments_Discharge @AssignmentID", connection);
            command.Parameters.AddWithValue("@AssignmentID", assignmentId);
            command.ExecuteNonQuery();
        }

        room = rooms.GetById(roomNo).Rows[0];
        Assert.Equal(1, Convert.ToInt32(room["OccupiedBeds"]));
        Assert.Equal("Available", Convert.ToString(room["Status"]));
    }

    public void Dispose() => CurrentUser.Clear();

    [SkippableFact]
    public void Patient_to_discharge_full_cycle()
    {
        Skip.IfNot(_db.LocalDbAvailable, _db.SkipReason);
        Assert.True(_db.ScriptErrors.Count == 0, "Fix the script errors first: " + string.Join("; ", _db.ScriptErrors));

        CurrentUser.Set("admin@test.hms", "Admin");
        var cs = _db.ConnectionString;
        var patients = new PatientService(cs);
        var rooms = new RoomService(cs);
        var catalog = new ServiceCatalogService(cs);
        var admissions = new AdmissionService(cs);
        var charges = new ChargeService(cs);
        var billing = new BillingService(cs);
        var payments = new PaymentService(cs);

        // --- Patient and room ---
        var patientId = patients.Create(new Patient
        {
            FirstName = "Cycle",
            LastName = "Test" + Guid.NewGuid().ToString("N")[..6],
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = "Female"
        });
        Assert.True(patientId > 0);
        rooms.Create("T-101", "GEN", 1500m);

        // --- Admission ---
        var admission = admissions.Admit(patientId, "T-101", "EMR", "A00", "Dr. Test", "Observation");
        Assert.Equal(AdmissionStatus.Admitted, admission.Status);
        Assert.False(string.IsNullOrWhiteSpace(admission.AdmissionNo));
        Assert.Equal("Occupied", rooms.GetById("T-101").Rows[0]["Status"]);

        // Invalid: the same patient again.
        var activeAdmissionsBeforeDuplicate = Scalar(
            "SELECT COUNT(*) FROM dbo.tblAdmissions WHERE PatientID = @PatientID AND Status = N'Admitted'",
            new SqlParameter("@PatientID", patientId));
        Assert.Throws<ValidationException>(() =>
            admissions.Admit(patientId, "T-101", "EMR", "A00", "Dr. Test", "Again"));
        Assert.Equal(activeAdmissionsBeforeDuplicate, Scalar(
            "SELECT COUNT(*) FROM dbo.tblAdmissions WHERE PatientID = @PatientID AND Status = N'Admitted'",
            new SqlParameter("@PatientID", patientId)));
        Assert.Equal("Occupied", rooms.GetById("T-101").Rows[0]["Status"]);

        // --- Charges ---
        var services = catalog.GetAll(activeOnly: true);
        Assert.True(services.Rows.Count > 0, "No services were seeded.");
        var serviceId = Convert.ToInt32(services.Rows[0]["ServiceID"]);
        charges.Add(admission.AdmissionID, serviceId, 2);
        Assert.True(charges.GetSubtotal(admission.AdmissionID) > 0m);

        // Invalid: discharge before there is a bill.
        Assert.Throws<ValidationException>(() => admissions.Discharge(admission.AdmissionID, "Too early"));
        Assert.Equal(AdmissionStatus.Admitted, admissions.GetById(admission.AdmissionID)!.Status);
        Assert.Equal("Occupied", rooms.GetById("T-101").Rows[0]["Status"]);

        // --- Bill ---
        var bill = billing.Generate(admission.AdmissionID);
        Assert.Equal(BillStatus.Open, bill.Status);
        Assert.Equal(bill.Subtotal - bill.Discount, bill.TotalAmount);
        Assert.Equal(bill.TotalAmount, bill.Balance);

        // --- Payment ---
        var paymentsBeforeOverpayment = PaymentCount(bill.BillID);
        Assert.Throws<ValidationException>(() => payments.Post(bill.BillID, bill.Balance + 1m, "Cash", null)); // overpay
        bill = billing.GetById(bill.BillID)!;
        Assert.Equal(0m, bill.AmountPaid);
        Assert.Equal(bill.TotalAmount, bill.Balance);
        Assert.Equal(paymentsBeforeOverpayment, PaymentCount(bill.BillID));

        var firstPaymentAmount = Math.Min(100m, Math.Floor(bill.Balance / 2m * 100m) / 100m);
        Assert.True(firstPaymentAmount > 0m, "The test bill must have a positive partial-payment amount.");
        var first = payments.Post(bill.BillID, firstPaymentAmount, "Cash", null);
        Assert.False(string.IsNullOrWhiteSpace(first.ReceiptNo));
        bill = billing.GetById(bill.BillID)!;
        Assert.Equal(BillStatus.PartiallyPaid, bill.Status);
        Assert.Equal(firstPaymentAmount, bill.AmountPaid);

        // Invalid: discharge with a balance.
        var blocked = Assert.Throws<ValidationException>(() => admissions.Discharge(admission.AdmissionID, "Not yet"));
        Assert.Contains("unpaid", blocked.Message);
        Assert.Equal(AdmissionStatus.Admitted, admissions.GetById(admission.AdmissionID)!.Status);
        Assert.Equal("Occupied", rooms.GetById("T-101").Rows[0]["Status"]);

        payments.Post(bill.BillID, bill.Balance, "Card", "SLIP-0001");
        bill = billing.GetById(bill.BillID)!;
        Assert.Equal(BillStatus.Paid, bill.Status);
        Assert.Equal(0m, bill.Balance);

        // --- Discharge ---
        Assert.True(admissions.CheckDischarge(admission.AdmissionID).CanDischarge);
        admissions.Discharge(admission.AdmissionID, "Recovered");

        Assert.Equal(AdmissionStatus.Discharged, admissions.GetById(admission.AdmissionID)!.Status);
        Assert.Equal("Available", rooms.GetById("T-101").Rows[0]["Status"]);

        // Invalid: a charge after discharge.
        var chargesBeforeDischargedAttempt = ChargeCount(admission.AdmissionID);
        Assert.Throws<ValidationException>(() => charges.Add(admission.AdmissionID, serviceId, 1));
        Assert.Equal(chargesBeforeDischargedAttempt, ChargeCount(admission.AdmissionID));

        // --- Final record matches the bill ---
        var record = admissions.GetDischargeRecord(admission.AdmissionID);
        Assert.Equal(bill.TotalAmount, record.Bill!.TotalAmount);
        Assert.Equal(2, record.Payments.Rows.Count);
    }

    private int PaymentCount(int billId) => Scalar(
        "SELECT COUNT(*) FROM dbo.tblPayments WHERE BillID = @BillID AND Status = N'Posted'",
        new SqlParameter("@BillID", billId));

    private int ChargeCount(int admissionId) => Scalar(
        "SELECT COUNT(*) FROM dbo.tblAdmissionCharges WHERE AdmissionID = @AdmissionID",
        new SqlParameter("@AdmissionID", admissionId));

    private int Scalar(string sql, params SqlParameter[] parameters)
    {
        using var conn = new SqlConnection(_db.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddRange(parameters);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}
