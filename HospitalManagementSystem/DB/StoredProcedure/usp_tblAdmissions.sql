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

        -- UPDLOCK so two people cannot take the same room at the same moment
        IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WITH (UPDLOCK, HOLDLOCK) WHERE RoomNo = @RoomNo AND IsActive = 1)
            RAISERROR(N'The room was not found or is inactive.', 16, 1);

        IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = @RoomNo AND Status = N'Available')
            RAISERROR(N'The room is not available.', 16, 1);

        IF EXISTS (SELECT 1 FROM dbo.tblRoomAssignments WHERE RoomNo = @RoomNo AND DischargeDate IS NULL)
            RAISERROR(N'The room is already occupied.', 16, 1);

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

        UPDATE dbo.tblRooms SET Status = N'Occupied', UpdatedAt = SYSUTCDATETIME() WHERE RoomNo = @RoomNo;

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
           a.AssignmentID, ra.RoomNo, a.DepartmentCode, a.DiagnosisCode, a.AttendingPhysician, a.Reason,
           a.Status, a.AdmitDate, a.DischargeDate, a.DischargeSummary, a.CreatedBy
    FROM   dbo.tblAdmissions a
    JOIN   dbo.tblPatients p ON p.PatientID = a.PatientID
    LEFT JOIN dbo.tblRoomAssignments ra ON ra.AssignmentID = a.AssignmentID
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
           a.AssignmentID, ra.RoomNo, a.DepartmentCode, a.DiagnosisCode, a.AttendingPhysician, a.Reason,
           a.Status, a.AdmitDate, a.DischargeDate, a.DischargeSummary, a.CreatedBy
    FROM   dbo.tblAdmissions a
    JOIN   dbo.tblPatients p ON p.PatientID = a.PatientID
    LEFT JOIN dbo.tblRoomAssignments ra ON ra.AssignmentID = a.AssignmentID
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

        -- Cancel is only for admissions with no billing activity (DB-02 / DB-03 tables may not exist yet)
        IF OBJECT_ID(N'dbo.tblAdmissionCharges', N'U') IS NOT NULL
        BEGIN
            DECLARE @n INT;
            EXEC sp_executesql
                N'SELECT @n = COUNT(*) FROM dbo.tblAdmissionCharges WHERE AdmissionID = @a AND IsVoided = 0',
                N'@a INT, @n INT OUTPUT', @a = @AdmissionID, @n = @n OUTPUT;
            IF @n > 0 RAISERROR(N'This admission already has charges. Discharge it instead of cancelling.', 16, 1);
        END

        SELECT @RoomNo = RoomNo FROM dbo.tblRoomAssignments WHERE AssignmentID = @AssignmentID;

        UPDATE dbo.tblRoomAssignments
        SET    DischargeDate = CASE WHEN SYSUTCDATETIME() < AdmitDate THEN AdmitDate ELSE SYSUTCDATETIME() END
        WHERE  AssignmentID = @AssignmentID AND DischargeDate IS NULL;
        UPDATE dbo.tblRooms SET Status = N'Available', UpdatedAt = SYSUTCDATETIME()
        WHERE  RoomNo = @RoomNo AND Status = N'Occupied';

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
        IF OBJECT_ID(N'dbo.tblBills', N'U') IS NULL
            RAISERROR(N'Billing is not installed in this database.', 16, 1);
        DECLARE @ok INT = 0;
        EXEC sp_executesql
            N'SELECT @ok = COUNT(*) FROM dbo.tblBills WHERE AdmissionID = @a AND Status = N''Paid''',
            N'@a INT, @ok INT OUTPUT', @a = @AdmissionID, @ok = @ok OUTPUT;
        IF @ok = 0 RAISERROR(N'The bill must be generated and fully paid before discharge.', 16, 1);

        SELECT @RoomNo = RoomNo FROM dbo.tblRoomAssignments WHERE AssignmentID = @AssignmentID;

        UPDATE dbo.tblRoomAssignments
        SET    DischargeDate = CASE WHEN SYSUTCDATETIME() < AdmitDate THEN AdmitDate ELSE SYSUTCDATETIME() END
        WHERE  AssignmentID = @AssignmentID AND DischargeDate IS NULL;

        UPDATE dbo.tblRooms SET Status = N'Available', UpdatedAt = SYSUTCDATETIME()
        WHERE  RoomNo = @RoomNo AND Status = N'Occupied';

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
