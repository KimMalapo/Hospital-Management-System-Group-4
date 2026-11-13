-- Report procedures (DB-05). Read-only. @From / @To are inclusive dates.

CREATE PROCEDURE dbo.usp_Report_BillingSummary
    @From DATE,
    @To   DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(*)                                   AS BillsGenerated,
           ISNULL(SUM(RoomCharges + ServiceCharges), 0) AS GrossBilled,
           ISNULL(SUM(Discount), 0)                   AS TotalDiscounts,
           ISNULL(SUM(TotalAmount), 0)                AS NetBilled,
           ISNULL(SUM(AmountPaid), 0)                 AS TotalCollected,
           ISNULL(SUM(Balance), 0)                    AS OutstandingBalance
    FROM   dbo.tblBills
    WHERE  Status <> N'Voided'
      AND  GeneratedAt >= @From AND GeneratedAt < DATEADD(DAY, 1, @To);
END
GO

CREATE PROCEDURE dbo.usp_Report_PaymentHistory
    @From      DATE,
    @To        DATE,
    @PatientID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.PaymentDate, p.ReceiptNo, b.BillNo,
           pt.LastName + N', ' + pt.FirstName AS PatientName,
           p.Method, p.ReferenceNo, p.Amount, p.ReceivedBy, p.Status
    FROM   dbo.tblPayments p
    JOIN   dbo.tblBills b ON b.BillID = p.BillID
    JOIN   dbo.tblAdmissions a ON a.AdmissionID = b.AdmissionID
    JOIN   dbo.tblPatients pt ON pt.PatientID = a.PatientID
    WHERE  p.PaymentDate >= @From AND p.PaymentDate < DATEADD(DAY, 1, @To)
      AND  (@PatientID IS NULL OR a.PatientID = @PatientID)
    ORDER BY p.PaymentDate DESC, p.PaymentID DESC;
END
GO

-- One row per payment method for the day, only posted (not voided) payments
CREATE PROCEDURE dbo.usp_Report_DailyCollections
    @Date DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Method, COUNT(*) AS PaymentCount, SUM(Amount) AS TotalAmount
    FROM   dbo.tblPayments
    WHERE  Status = N'Posted'
      AND  PaymentDate >= @Date AND PaymentDate < DATEADD(DAY, 1, @Date)
    GROUP BY Method
    ORDER BY Method;
END
GO

CREATE PROCEDURE dbo.usp_Report_OutstandingBalances
AS
BEGIN
    SET NOCOUNT ON;
    SELECT b.BillNo, a.AdmissionNo, pt.LastName + N', ' + pt.FirstName AS PatientName,
           b.TotalAmount, b.AmountPaid, b.Balance, b.Status, b.GeneratedAt
    FROM   dbo.tblBills b
    JOIN   dbo.tblAdmissions a ON a.AdmissionID = b.AdmissionID
    JOIN   dbo.tblPatients pt ON pt.PatientID = a.PatientID
    WHERE  b.Status <> N'Voided' AND b.Balance > 0
    ORDER BY b.Balance DESC;
END
GO

CREATE PROCEDURE dbo.usp_Report_Admissions
    @From   DATE,
    @To     DATE,
    @Status NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT a.AdmissionNo, pt.LastName + N', ' + pt.FirstName AS PatientName, ra.RoomNo,
           a.DepartmentCode, a.DiagnosisCode, a.AttendingPhysician, a.Status, a.AdmitDate, a.DischargeDate
    FROM   dbo.tblAdmissions a
    JOIN   dbo.tblPatients pt ON pt.PatientID = a.PatientID
    LEFT JOIN dbo.tblRoomAssignments ra ON ra.AssignmentID = a.AssignmentID
    WHERE  a.AdmitDate >= @From AND a.AdmitDate < DATEADD(DAY, 1, @To)
      AND  (@Status IS NULL OR a.Status = @Status)
    ORDER BY a.AdmitDate DESC;
END
GO

CREATE PROCEDURE dbo.usp_Report_RoomOccupancy
AS
BEGIN
    SET NOCOUNT ON;
    SELECT r.RoomNo, r.RoomType, r.Rate, r.Status,
           pt.LastName + N', ' + pt.FirstName AS PatientName,
           ra.AdmitDate,
           CASE WHEN ra.AssignmentID IS NULL THEN NULL
                ELSE DATEDIFF(DAY, ra.AdmitDate, SYSUTCDATETIME()) + 1 END AS DaysStayed
    FROM   dbo.tblRooms r
    LEFT JOIN dbo.tblRoomAssignments ra ON ra.RoomNo = r.RoomNo AND ra.DischargeDate IS NULL
    LEFT JOIN dbo.tblPatients pt ON pt.PatientID = ra.PatientID
    WHERE  r.IsActive = 1
    ORDER BY r.RoomNo;
END
GO

CREATE PROCEDURE dbo.usp_Report_DischargeSummary
    @AdmissionID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT a.AdmissionNo, pt.LastName + N', ' + pt.FirstName AS PatientName, ra.RoomNo,
           a.DepartmentCode, a.DiagnosisCode, a.AttendingPhysician, a.Reason,
           a.AdmitDate, a.DischargeDate, a.DischargeSummary,
           b.BillNo, b.TotalAmount, b.AmountPaid, b.Balance
    FROM   dbo.tblAdmissions a
    JOIN   dbo.tblPatients pt ON pt.PatientID = a.PatientID
    LEFT JOIN dbo.tblRoomAssignments ra ON ra.AssignmentID = a.AssignmentID
    LEFT JOIN dbo.tblBills b ON b.AdmissionID = a.AdmissionID AND b.Status <> N'Voided'
    WHERE  a.AdmissionID = @AdmissionID;
END
GO

CREATE PROCEDURE dbo.usp_Dashboard_Counts
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        (SELECT COUNT(*) FROM dbo.tblAdmissions WHERE Status = N'Admitted')                       AS ActiveAdmissions,
        (SELECT COUNT(*) FROM dbo.tblRooms WHERE IsActive = 1 AND Status = N'Available')           AS AvailableRooms,
        (SELECT COUNT(*) FROM dbo.tblBills WHERE Status IN (N'Open', N'PartiallyPaid'))            AS UnpaidBills,
        (SELECT ISNULL(SUM(Amount), 0) FROM dbo.tblPayments
          WHERE Status = N'Posted' AND PaymentDate >= CAST(SYSUTCDATETIME() AS DATE))              AS TodaysCollections;
END
