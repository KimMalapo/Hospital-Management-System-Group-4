using System.Data;
using Model;

namespace BusinessLogic;

/// <summary>
/// Converts stored-procedure result rows into model objects. Column names are the contract with the
/// database scripts (see README). Used by every service so each mapping exists exactly once.
/// </summary>
public static class Mappers
{
    public static Admission ToAdmission(DataRow r) => new()
    {
        AdmissionID = RowReader.Int(r, "AdmissionID"),
        AdmissionNo = RowReader.Str(r, "AdmissionNo"),
        PatientID = RowReader.Int(r, "PatientID"),
        PatientName = RowReader.Str(r, "PatientName"),
        AssignmentID = RowReader.Int(r, "AssignmentID"),
        RoomNo = RowReader.Str(r, "RoomNo"),
        DepartmentCode = RowReader.Str(r, "DepartmentCode"),
        DiagnosisCode = RowReader.Str(r, "DiagnosisCode"),
        AttendingPhysician = RowReader.Str(r, "AttendingPhysician"),
        Reason = RowReader.Str(r, "Reason"),
        Status = RowReader.Str(r, "Status") ?? AdmissionStatus.Admitted,
        AdmitDate = RowReader.Date(r, "AdmitDate"),
        DischargeDate = RowReader.Date(r, "DischargeDate"),
        DischargeSummary = RowReader.Str(r, "DischargeSummary"),
        CreatedBy = RowReader.Str(r, "CreatedBy")
    };

    public static ServiceItem ToServiceItem(DataRow r) => new()
    {
        ServiceID = RowReader.Int(r, "ServiceID"),
        Code = RowReader.Str(r, "Code"),
        Name = RowReader.Str(r, "Name"),
        Category = RowReader.Str(r, "Category"),
        DrugCategoryCode = RowReader.Str(r, "DrugCategoryCode"),
        UnitPrice = RowReader.Dec(r, "UnitPrice"),
        IsActive = !r.Table.Columns.Contains("IsActive") || RowReader.Bool(r, "IsActive")
    };

    public static Charge ToCharge(DataRow r) => new()
    {
        ChargeID = RowReader.Int(r, "ChargeID"),
        AdmissionID = RowReader.Int(r, "AdmissionID"),
        ServiceID = RowReader.Int(r, "ServiceID"),
        Description = RowReader.Str(r, "Description"),
        Quantity = RowReader.Int(r, "Quantity"),
        UnitPrice = RowReader.Dec(r, "UnitPrice"),
        LineTotal = RowReader.Dec(r, "LineTotal"),
        ChargedAt = RowReader.Date(r, "ChargedAt"),
        ChargedBy = RowReader.Str(r, "ChargedBy"),
        IsVoided = RowReader.Bool(r, "IsVoided"),
        VoidReason = RowReader.Str(r, "VoidReason")
    };

    public static Bill ToBill(DataRow r) => new()
    {
        BillID = RowReader.Int(r, "BillID"),
        BillNo = RowReader.Str(r, "BillNo"),
        AdmissionID = RowReader.Int(r, "AdmissionID"),
        RoomCharges = RowReader.Dec(r, "RoomCharges"),
        ServiceCharges = RowReader.Dec(r, "ServiceCharges"),
        Discount = RowReader.Dec(r, "Discount"),
        DiscountReason = RowReader.Str(r, "DiscountReason"),
        TotalAmount = RowReader.Dec(r, "TotalAmount"),
        AmountPaid = RowReader.Dec(r, "AmountPaid"),
        Balance = RowReader.Dec(r, "Balance"),
        Status = RowReader.Str(r, "Status") ?? BillStatus.Open,
        GeneratedAt = RowReader.Date(r, "GeneratedAt"),
        GeneratedBy = RowReader.Str(r, "GeneratedBy")
    };

    public static Payment ToPayment(DataRow r) => new()
    {
        PaymentID = RowReader.Int(r, "PaymentID"),
        ReceiptNo = RowReader.Str(r, "ReceiptNo"),
        BillID = RowReader.Int(r, "BillID"),
        PaymentDate = RowReader.Date(r, "PaymentDate"),
        Amount = RowReader.Dec(r, "Amount"),
        Method = RowReader.Str(r, "Method"),
        ReferenceNo = RowReader.Str(r, "ReferenceNo"),
        ReceivedBy = RowReader.Str(r, "ReceivedBy"),
        Status = RowReader.Str(r, "Status") ?? "Posted",
        VoidReason = RowReader.Str(r, "VoidReason")
    };

    public static ReceiptData ToReceipt(DataRow r) => new()
    {
        PaymentID = RowReader.Int(r, "PaymentID"),
        ReceiptNo = RowReader.Str(r, "ReceiptNo"),
        PaymentDate = RowReader.Date(r, "PaymentDate"),
        PatientName = RowReader.Str(r, "PatientName"),
        BillNo = RowReader.Str(r, "BillNo"),
        AdmissionNo = RowReader.Str(r, "AdmissionNo"),
        Amount = RowReader.Dec(r, "Amount"),
        Method = RowReader.Str(r, "Method"),
        ReferenceNo = RowReader.Str(r, "ReferenceNo"),
        TotalAmount = RowReader.Dec(r, "TotalAmount"),
        BalanceAfter = RowReader.Dec(r, "BalanceAfter"),
        ReceivedBy = RowReader.Str(r, "ReceivedBy")
    };
}
