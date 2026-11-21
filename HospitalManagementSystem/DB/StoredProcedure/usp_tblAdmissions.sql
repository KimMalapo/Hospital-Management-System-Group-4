-- Admission procedures (DB-01). Every row-returning procedure returns the same column set so the
-- C# Mappers.ToAdmission works for all of them.

CREATE PROCEDURE dbo.usp_tblAdmissions_Admit
    @PatientID          INT,
    @RoomNo             NVARCHAR(20),
    @DepartmentCode     NVARCHAR(50)  = NULL,
    @DiagnosisCode      NVARCHAR(50)  = NULL,
    @AttendingPhysician NVARCHAR(200) = NULL,
    @Reason             NVARCHAR(500) = NULL,
    @AdmitDate          DATETIME2(0)  = NULL,
    @CreatedBy          NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM dbo.tblPatients WHERE PatientID = @PatientID AND IsActive = 1)
            RAISERROR(N'The patient was not found or is inactive.', 16, 1);

        IF EXISTS (SELECT 1 FROM dbo.tblAdmissions WHERE PatientID = @PatientID AND Status = N'Admitted')
            RAISERROR(N'This patient is already admitted. Discharge the current admission first.', 16, 1);

        -- Serialize capacity checks on the room row so concurrent admits cannot overfill it.
        DECLARE @Capacity INT, @RoomStatus NVARCHAR(30);
        SELECT @Capacity = Capacity, @RoomStatus = Status
        FROM dbo.tblRooms WITH (UPDLOCK, HOLDLOCK)
        WHERE RoomNo = @RoomNo AND IsActive = 1;

        IF @Capacity IS NULL
            RAISERROR(N'The room was not found or is inactive.', 16, 1);

        IF @RoomStatus IN (N'Maintenance', N'Reserved')
            RAISERROR(N'The room is not available for admission.', 16, 1);

        DECLARE @Occupied INT =
            (SELECT COUNT(*) FROM dbo.tblRoomAssignments WITH (UPDLOCK, HOLDLOCK)
             WHERE RoomNo = @RoomNo AND DischargeDate IS NULL);
        IF @Occupied >= @Capacity
            RAISERROR(N'The room is at full capacity. Select another room.', 16, 1);

        IF @DepartmentCode IS NOT NULL AND NOT EXISTS
           (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = @DepartmentCode AND IsDeleted = 0)
            RAISERROR(N'The department was not found.', 16, 1);

        IF @DiagnosisCode IS NOT NULL AND NOT EXISTS
           (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = @DiagnosisCode AND IsDeleted = 0)
            RAISERROR(N'The diagnosis code was not found.', 16, 1);

        DECLARE @When DATETIME2(0) = ISNULL(@AdmitDate, SYSUTCDATETIME());

        INSERT INTO dbo.tblRoomAssignments (PatientID, RoomNo, AdmitDate, Notes, CreatedBy)
        VALUES (@PatientID, @RoomNo, @When, @Reason, @CreatedBy);
        DECLARE @AssignmentID INT = CAST(SCOPE_IDENTITY() AS INT);

        UPDATE dbo.tblRooms
        SET Status = CASE WHEN @Occupied + 1 >= @Capacity THEN N'Occupied' ELSE N'Available' END,
            UpdatedAt = SYSUTCDATETIME()
        WHERE RoomNo = @RoomNo;

        INSERT INTO dbo.tblAdmissions
            (PatientID, AssignmentID, DepartmentCode, DiagnosisCode, AttendingPhysician, Reason, AdmitDate, CreatedBy)
        VALUES
            (@PatientID, @AssignmentID, @DepartmentCode, @DiagnosisCode, @AttendingPhysician, @Reason, @When, @CreatedBy);

        DECLARE @AdmissionID INT = CAST(SCOPE_IDENTITY() AS INT);
        COMMIT TRANSACTION;

        SELECT @AdmissionID AS AdmissionID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- Shared column set (kept identical in each SELECT below)
CREATE PROCEDURE dbo.usp_tblAdmissions_GetActive
AS
BEGIN
    SET NOCOUNT ON;
    SELECT a.AdmissionID, a.AdmissionNo, a.PatientID, p.LastName + N', ' + p.FirstName AS PatientName,
           a.AssignmentID, ra.RoomNo, a.DepartmentCode, a.DiagnosisCode,
           COALESCE(dc.Description, a.DiagnosisCode) AS DiagnosisName,
           a.AttendingPhysician, a.Reason, a.Status, a.AdmitDate, a.DischargeDate,
           a.DischargeSummary, a.CreatedBy, stay.DaysStayed,
           CONVERT(DECIMAL(12,2), CASE WHEN a.Status = N'Admitted' OR bill.TotalAmount IS NULL
               THEN ISNULL(r.Rate, 0) * stay.DaysStayed + ISNULL(charges.ServiceTotal, 0)
               ELSE bill.TotalAmount END) AS RunningTotal
    FROM   dbo.tblAdmissions a
    JOIN   dbo.tblPatients p ON p.PatientID = a.PatientID
    LEFT JOIN dbo.tblRoomAssignments ra ON ra.AssignmentID = a.AssignmentID
    LEFT JOIN dbo.tblRooms r ON r.RoomNo = ra.RoomNo
    LEFT JOIN dbo.tblDiagnosisCodes dc ON dc.Code = a.DiagnosisCode AND dc.IsDeleted = 0
    OUTER APPLY
    (
        SELECT SUM(c.LineTotal) AS ServiceTotal
        FROM dbo.tblAdmissionCharges c
        WHERE c.AdmissionID = a.AdmissionID AND c.IsVoided = 0
    ) charges
    CROSS APPLY (SELECT COALESCE(a.DischargeDate, SYSUTCDATETIME()) AS EndDate) stayEnd
    CROSS APPLY
    (
        SELECT CASE WHEN stayEnd.EndDate <= a.AdmitDate THEN 1
                    ELSE (DATEDIFF(HOUR, a.AdmitDate, stayEnd.EndDate) + 23) / 24 END AS DaysStayed
    ) stay
    OUTER APPLY
    (
        SELECT TOP (1) b.TotalAmount
        FROM dbo.tblBills b
        WHERE b.AdmissionID = a.AdmissionID AND b.Status <> N'Voided'
        ORDER BY b.GeneratedAt DESC, b.BillID DESC
    ) bill
    WHERE  a.Status = N'Admitted'
    ORDER BY a.AdmitDate DESC;
END
GO

CREATE PROCEDURE dbo.usp_tblAdmissions_GetById
    @AdmissionID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT a.AdmissionID, a.AdmissionNo, a.PatientID, p.LastName + N', ' + p.FirstName AS PatientName,
           a.AssignmentID, ra.RoomNo, a.DepartmentCode, a.DiagnosisCode, a.AttendingPhysician, a.Reason,
           a.Status, a.AdmitDate, a.DischargeDate, a.DischargeSummary, a.CreatedBy
    FROM   dbo.tblAdmissions a
    JOIN   dbo.tblPatients p ON p.PatientID = a.PatientID
    LEFT JOIN dbo.tblRoomAssignments ra ON ra.AssignmentID = a.AssignmentID
    WHERE  a.AdmissionID = @AdmissionID;
END
GO

CREATE PROCEDURE dbo.usp_tblAdmissions_GetByPatient
    @PatientID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT a.AdmissionID, a.AdmissionNo, a.PatientID, p.LastName + N', ' + p.FirstName AS PatientName,
           a.AssignmentID, ra.RoomNo, a.DepartmentCode, a.DiagnosisCode, a.AttendingPhysician, a.Reason,
           a.Status, a.AdmitDate, a.DischargeDate, a.DischargeSummary, a.CreatedBy
    FROM   dbo.tblAdmissions a
    JOIN   dbo.tblPatients p ON p.PatientID = a.PatientID
    LEFT JOIN dbo.tblRoomAssignments ra ON ra.AssignmentID = a.AssignmentID
    WHERE  a.PatientID = @PatientID
    ORDER BY a.AdmitDate DESC;
END
GO

CREATE PROCEDURE dbo.usp_tblAdmissions_Search
    @SearchTerm NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @t NVARCHAR(202) = N'%' + ISNULL(LTRIM(RTRIM(@SearchTerm)), N'') + N'%';
    SELECT a.AdmissionID, a.AdmissionNo, a.PatientID, p.LastName + N', ' + p.FirstName AS PatientName,
           a.AssignmentID, ra.RoomNo, a.DepartmentCode, a.DiagnosisCode,
           COALESCE(dc.Description, a.DiagnosisCode) AS DiagnosisName,
           a.AttendingPhysician, a.Reason, a.Status, a.AdmitDate, a.DischargeDate,
           a.DischargeSummary, a.CreatedBy, stay.DaysStayed,
           CONVERT(DECIMAL(12,2), CASE WHEN a.Status = N'Admitted' OR bill.TotalAmount IS NULL
               THEN ISNULL(r.Rate, 0) * stay.DaysStayed + ISNULL(charges.ServiceTotal, 0)
               ELSE bill.TotalAmount END) AS RunningTotal
    FROM   dbo.tblAdmissions a
    JOIN   dbo.tblPatients p ON p.PatientID = a.PatientID
    LEFT JOIN dbo.tblRoomAssignments ra ON ra.AssignmentID = a.AssignmentID
    LEFT JOIN dbo.tblRooms r ON r.RoomNo = ra.RoomNo
    LEFT JOIN dbo.tblDiagnosisCodes dc ON dc.Code = a.DiagnosisCode AND dc.IsDeleted = 0
    OUTER APPLY
    (
        SELECT SUM(c.LineTotal) AS ServiceTotal
        FROM dbo.tblAdmissionCharges c
        WHERE c.AdmissionID = a.AdmissionID AND c.IsVoided = 0
    ) charges
    CROSS APPLY (SELECT COALESCE(a.DischargeDate, SYSUTCDATETIME()) AS EndDate) stayEnd
    CROSS APPLY
    (
        SELECT CASE WHEN stayEnd.EndDate <= a.AdmitDate THEN 1
                    ELSE (DATEDIFF(HOUR, a.AdmitDate, stayEnd.EndDate) + 23) / 24 END AS DaysStayed
    ) stay
    OUTER APPLY
    (
        SELECT TOP (1) b.TotalAmount
        FROM dbo.tblBills b
        WHERE b.AdmissionID = a.AdmissionID AND b.Status <> N'Voided'
        ORDER BY b.GeneratedAt DESC, b.BillID DESC
    ) bill
    WHERE  p.FirstName LIKE @t OR p.LastName LIKE @t
        OR p.FirstName + N' ' + p.LastName LIKE @t
        OR a.AdmissionNo LIKE @t OR ra.RoomNo LIKE @t
    ORDER BY a.AdmitDate DESC;
END
GO

CREATE PROCEDURE dbo.usp_tblAdmissions_Cancel
    @AdmissionID INT,
    @CancelledBy NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Status NVARCHAR(20), @AssignmentID INT, @RoomNo NVARCHAR(20);

        SELECT @Status = Status, @AssignmentID = AssignmentID
        FROM   dbo.tblAdmissions WITH (UPDLOCK) WHERE AdmissionID = @AdmissionID;

        IF @Status IS NULL RAISERROR(N'The admission was not found.', 16, 1);
        IF @Status <> N'Admitted' RAISERROR(N'Only an active admission can be cancelled.', 16, 1);

        -- Cancel is only for admissions with no charges (tblAdmissionCharges comes from DB-02; the project
        -- shows an unresolved-reference warning for it until DB-02 is added)
        IF EXISTS (SELECT 1 FROM dbo.tblAdmissionCharges WHERE AdmissionID = @AdmissionID AND IsVoided = 0)
            RAISERROR(N'This admission already has charges. Discharge it instead of cancelling.', 16, 1);

        SELECT @RoomNo = RoomNo FROM dbo.tblRoomAssignments WHERE AssignmentID = @AssignmentID;

        -- Serialize status updates with admissions to the same room.
        SELECT @RoomNo = RoomNo
        FROM dbo.tblRooms WITH (UPDLOCK, HOLDLOCK)
        WHERE RoomNo = @RoomNo;

        UPDATE dbo.tblRoomAssignments
        SET    DischargeDate = CASE WHEN SYSUTCDATETIME() < AdmitDate THEN AdmitDate ELSE SYSUTCDATETIME() END
        WHERE  AssignmentID = @AssignmentID AND DischargeDate IS NULL;
        UPDATE dbo.tblRooms
        SET Status = CASE WHEN (SELECT COUNT(*) FROM dbo.tblRoomAssignments
                                WHERE RoomNo = @RoomNo AND DischargeDate IS NULL) >= Capacity
                          THEN N'Occupied' ELSE N'Available' END,
            UpdatedAt = SYSUTCDATETIME()
        WHERE RoomNo = @RoomNo AND Status IN (N'Available', N'Occupied');

        UPDATE dbo.tblAdmissions
        SET    Status = N'Cancelled', CancelledBy = @CancelledBy, UpdatedAt = SYSUTCDATETIME()
        WHERE  AdmissionID = @AdmissionID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblAdmissions_Discharge
    @AdmissionID      INT,
    @DischargeSummary NVARCHAR(1000) = NULL,
    @DischargedBy     NVARCHAR(200)  = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Status NVARCHAR(20), @AssignmentID INT, @RoomNo NVARCHAR(20);

        SELECT @Status = Status, @AssignmentID = AssignmentID
        FROM   dbo.tblAdmissions WITH (UPDLOCK) WHERE AdmissionID = @AdmissionID;

        IF @Status IS NULL RAISERROR(N'The admission was not found.', 16, 1);
        IF @Status <> N'Admitted' RAISERROR(N'This admission is not active.', 16, 1);

        -- Rule: a patient is discharged only when a non-voided bill exists and is fully paid
        -- (tblBills comes from DB-03; unresolved-reference warning until it is added)
        IF NOT EXISTS (SELECT 1 FROM dbo.tblBills WHERE AdmissionID = @AdmissionID AND Status = N'Paid')
            RAISERROR(N'The bill must be generated and fully paid before discharge.', 16, 1);

        SELECT @RoomNo = RoomNo FROM dbo.tblRoomAssignments WHERE AssignmentID = @AssignmentID;

        -- Serialize status updates with admissions to the same room.
        SELECT @RoomNo = RoomNo
        FROM dbo.tblRooms WITH (UPDLOCK, HOLDLOCK)
        WHERE RoomNo = @RoomNo;

        UPDATE dbo.tblRoomAssignments
        SET    DischargeDate = CASE WHEN SYSUTCDATETIME() < AdmitDate THEN AdmitDate ELSE SYSUTCDATETIME() END
        WHERE  AssignmentID = @AssignmentID AND DischargeDate IS NULL;

        UPDATE dbo.tblRooms
        SET Status = CASE WHEN (SELECT COUNT(*) FROM dbo.tblRoomAssignments
                                WHERE RoomNo = @RoomNo AND DischargeDate IS NULL) >= Capacity
                          THEN N'Occupied' ELSE N'Available' END,
            UpdatedAt = SYSUTCDATETIME()
        WHERE RoomNo = @RoomNo AND Status IN (N'Available', N'Occupied');

        UPDATE dbo.tblAdmissions
        SET    Status = N'Discharged', DischargeDate = CASE WHEN SYSUTCDATETIME() < AdmitDate THEN AdmitDate ELSE SYSUTCDATETIME() END,
               DischargeSummary = @DischargeSummary, UpdatedAt = SYSUTCDATETIME()
        WHERE  AdmissionID = @AdmissionID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
