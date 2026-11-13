using System.Data;
using BusinessLogic;
using Model;

namespace HospitalManagementSystem.Tests;

public class AdmissionServiceValidationTests : IDisposable
{
    // The connection string is never used: validation fails before any database call.
    private readonly AdmissionService _svc = new("Server=(localdb)\\none;Database=none;");

    public AdmissionServiceValidationTests() => CurrentUser.Clear();
    public void Dispose() => CurrentUser.Clear();

    [Fact]
    public void Admit_requires_a_patient()
    {
        var ex = Assert.Throws<ValidationException>(() => _svc.Admit(0, "101", "EMR", "A00", "Dr. Cruz", null));
        Assert.Equal("Patient", ex.Field);
    }

    [Theory]
    [InlineData("", "EMR", "A00", "Dr. Cruz", "Room")]
    [InlineData("101", "", "A00", "Dr. Cruz", "Department")]
    [InlineData("101", "EMR", "", "Dr. Cruz", "Diagnosis")]
    [InlineData("101", "EMR", "A00", "  ", "Attending physician")]
    public void Admit_requires_each_field(string room, string dept, string dx, string doctor, string field)
    {
        var ex = Assert.Throws<ValidationException>(() => _svc.Admit(1, room, dept, dx, doctor, null));
        Assert.Equal(field, ex.Field);
    }

    [Fact]
    public void Admit_rejects_a_future_date_and_a_long_reason()
    {
        Assert.Throws<ValidationException>(() =>
            _svc.Admit(1, "101", "EMR", "A00", "Dr. Cruz", null, DateTime.Now.AddDays(2)));
        Assert.Throws<ValidationException>(() =>
            _svc.Admit(1, "101", "EMR", "A00", "Dr. Cruz", new string('x', 501)));
    }

    [Fact]
    public void Valid_input_but_nobody_logged_in_is_refused()
    {
        Assert.Throws<UnauthorizedAccessException>(() =>
            _svc.Admit(1, "101", "EMR", "A00", "Dr. Cruz", "Observation"));
    }

    [Fact]
    public void Discharge_summary_has_a_length_limit()
    {
        Assert.Throws<ValidationException>(() => _svc.Discharge(1, new string('x', 1001)));
    }

    [Fact]
    public void CheckDischarge_without_an_admission_says_so()
    {
        var check = _svc.CheckDischarge(0);
        Assert.False(check.CanDischarge);
        Assert.Contains("Select", check.Message);
    }
}

public class MapperTests
{
    [Fact]
    public void Admission_row_maps_to_model()
    {
        var t = new DataTable();
        foreach (var c in new[] { "AdmissionID", "AdmissionNo", "PatientID", "RoomNo", "Status" }) t.Columns.Add(c);
        t.Columns.Add("AdmitDate", typeof(DateTime));
        t.Rows.Add(7, "ADM-2026-0007", 3, "101", "Admitted", new DateTime(2026, 11, 12));

        var a = Mappers.ToAdmission(t.Rows[0]);

        Assert.Equal(7, a.AdmissionID);
        Assert.Equal("ADM-2026-0007", a.AdmissionNo);
        Assert.Equal("101", a.RoomNo);
        Assert.Equal(new DateTime(2026, 11, 12), a.AdmitDate);
        Assert.Null(a.DischargeDate);
    }

    [Fact]
    public void Missing_columns_and_nulls_do_not_crash()
    {
        var t = new DataTable();
        t.Columns.Add("BillID");
        t.Columns.Add("Discount");
        t.Rows.Add(5, DBNull.Value);

        var bill = Mappers.ToBill(t.Rows[0]);

        Assert.Equal(5, bill.BillID);
        Assert.Equal(0m, bill.Discount);
        Assert.Equal(BillStatus.Open, bill.Status);
    }

    [Fact]
    public void Bill_subtotal_is_room_plus_services()
    {
        var bill = new Bill { RoomCharges = 4500m, ServiceCharges = 2000m, Discount = 500m };
        Assert.Equal(6500m, bill.Subtotal);
    }

    [Theory]
    [InlineData("Cash", false)]
    [InlineData("cash", false)]
    [InlineData("Card", true)]
    [InlineData("GCash", true)]
    [InlineData("Bank Transfer", true)]
    public void Only_cash_skips_the_reference_number(string method, bool needsReference)
    {
        Assert.Equal(needsReference, PaymentMethod.RequiresReference(method));
    }
}
