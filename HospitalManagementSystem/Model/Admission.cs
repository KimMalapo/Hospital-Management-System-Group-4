namespace Model;

public class Admission
{
    public int AdmissionID { get; set; }
    public string? AdmissionNo { get; set; }
    public int PatientID { get; set; }
    public string? PatientName { get; set; }
    public int AssignmentID { get; set; }
    public string? RoomNo { get; set; }
    public string? DepartmentCode { get; set; }
    public string? DiagnosisCode { get; set; }
    public string? AttendingPhysician { get; set; }
    public string? Reason { get; set; }
    public string Status { get; set; } = AdmissionStatus.Admitted;
    public DateTime? AdmitDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    public string? DischargeSummary { get; set; }
    public string? CreatedBy { get; set; }
}
