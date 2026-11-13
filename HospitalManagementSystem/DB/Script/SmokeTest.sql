-- Not part of the DB project build. Run manually against HospitalDB after publishing.
SET NOCOUNT ON;
DECLARE @r TABLE (Check_ NVARCHAR(200), Result NVARCHAR(10));

DECLARE @t TABLE (n SYSNAME);
INSERT @t VALUES (N'tblUsers'),(N'tblPatients'),(N'tblRooms'),(N'tblRoomAssignments'),(N'tblRoomTypes'),(N'tblDepartmentTypes'),
 (N'tblDiagnosisCodes'),(N'tblDrugCategories'),(N'tblAuditLog'),(N'tblAdmissions'),(N'tblServices'),(N'tblAdmissionCharges'),
 (N'tblBills'),(N'tblBillItems'),(N'tblPayments');
INSERT @r SELECT N'table dbo.' + n, CASE WHEN OBJECT_ID(N'dbo.' + n, N'U') IS NOT NULL THEN N'OK' ELSE N'MISSING' END FROM @t;

DECLARE @p TABLE (n SYSNAME);
INSERT @p VALUES (N'usp_tblAdmissions_Admit'),(N'usp_tblAdmissions_Discharge'),(N'usp_tblAdmissions_Cancel'),(N'usp_tblServices_GetAll'),
 (N'usp_tblAdmissionCharges_Add'),(N'usp_tblAdmissionCharges_Void'),(N'usp_tblBills_Generate'),(N'usp_tblBills_ApplyDiscount'),
 (N'usp_tblBills_Void'),(N'usp_tblPayments_Post'),(N'usp_tblPayments_Void'),(N'usp_Receipt_GetByPayment'),(N'usp_Report_BillingSummary'),
 (N'usp_Report_PaymentHistory'),(N'usp_Report_DailyCollections'),(N'usp_Report_OutstandingBalances'),(N'usp_Report_Admissions'),
 (N'usp_Report_RoomOccupancy'),(N'usp_Report_DischargeSummary'),(N'usp_Dashboard_Counts');
INSERT @r SELECT N'procedure dbo.' + n, CASE WHEN OBJECT_ID(N'dbo.' + n, N'P') IS NOT NULL THEN N'OK' ELSE N'MISSING' END FROM @p;

INSERT @r SELECT N'audit triggers on new tables (expect 15)', CASE WHEN
    (SELECT COUNT(*) FROM sys.triggers WHERE name LIKE N'trg_tblAdmission%' OR name LIKE N'trg_tblServices%'
        OR name LIKE N'trg_tblBills%' OR name LIKE N'trg_tblPayments%') = 15 THEN N'OK' ELSE N'CHECK' END;
INSERT @r SELECT N'demo patients (expect 10)', CASE WHEN (SELECT COUNT(*) FROM dbo.tblPatients WHERE Email LIKE N'%@demo.hms') = 10 THEN N'OK' ELSE N'CHECK' END;
INSERT @r SELECT N'a discharged admission with a paid bill', CASE WHEN EXISTS
    (SELECT 1 FROM dbo.tblAdmissions a JOIN dbo.tblBills b ON b.AdmissionID = a.AdmissionID
     WHERE a.Status = N'Discharged' AND b.Status = N'Paid') THEN N'OK' ELSE N'CHECK' END;

SELECT Check_, Result FROM @r ORDER BY CASE Result WHEN N'OK' THEN 1 ELSE 0 END, Check_;
