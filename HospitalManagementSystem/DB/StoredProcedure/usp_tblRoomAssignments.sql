-- Assign a patient to a room (creates open assignment + marks room Occupied)
CREATE PROCEDURE dbo.usp_tblRoomAssignments_Assign
    @PatientID  INT,
    @RoomNo     NVARCHAR(20),
    @AdmitDate  DATETIME2(0)  = NULL,
    @Notes      NVARCHAR(500) = NULL,
    @CreatedBy  NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        -- Validate patient
        IF NOT EXISTS (SELECT 1 FROM dbo.tblPatients WHERE PatientID = @PatientID AND IsActive = 1)
        BEGIN
            RAISERROR(N'Patient %d does not exist or is inactive.', 16, 1, @PatientID);
            RETURN;
        END

        -- Validate room is available
        IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = @RoomNo AND IsActive = 1 AND Status = N'Available')
        BEGIN
            RAISERROR(N'Room ''%s'' is not available.', 16, 1, @RoomNo);
            RETURN;
        END

        -- Guard: no open assignment already exists (unique index also enforces this)
        IF EXISTS (SELECT 1 FROM dbo.tblRoomAssignments WHERE RoomNo = @RoomNo AND DischargeDate IS NULL)
        BEGIN
            RAISERROR(N'Room ''%s'' already has an open assignment.', 16, 1, @RoomNo);
            RETURN;
        END

        DECLARE @Admit DATETIME2(0) = ISNULL(@AdmitDate, SYSUTCDATETIME());

        INSERT INTO dbo.tblRoomAssignments (PatientID, RoomNo, AdmitDate, Notes, CreatedBy)
        VALUES (@PatientID, @RoomNo, @Admit, @Notes, @CreatedBy);

        -- Mark room occupied
        UPDATE dbo.tblRooms
        SET    Status    = N'Occupied',
               UpdatedAt = SYSUTCDATETIME()
        WHERE  RoomNo = @RoomNo;

        COMMIT TRAN;

        SELECT CAST(SCOPE_IDENTITY() AS INT) AS AssignmentID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- Discharge a patient (closes assignment + frees the room)
CREATE PROCEDURE dbo.usp_tblRoomAssignments_Discharge
    @AssignmentID   INT           = NULL,
    @RoomNo         NVARCHAR(20)  = NULL,   -- alternative: discharge by room
    @DischargeDate  DATETIME2(0)  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        DECLARE @TargetRoom NVARCHAR(20);
        DECLARE @TargetID   INT = @AssignmentID;

        IF @TargetID IS NULL AND @RoomNo IS NOT NULL
        BEGIN
            SELECT TOP 1 @TargetID = AssignmentID, @TargetRoom = RoomNo
            FROM   dbo.tblRoomAssignments
            WHERE  RoomNo = @RoomNo AND DischargeDate IS NULL
            ORDER  BY AdmitDate DESC;
        END
        ELSE IF @TargetID IS NOT NULL
        BEGIN
            SELECT @TargetRoom = RoomNo
            FROM   dbo.tblRoomAssignments
            WHERE  AssignmentID = @TargetID AND DischargeDate IS NULL;
        END

        IF @TargetID IS NULL OR @TargetRoom IS NULL
        BEGIN
            RAISERROR(N'No open assignment found to discharge.', 16, 1);
            RETURN;
        END

        DECLARE @Discharge DATETIME2(0) = ISNULL(@DischargeDate, SYSUTCDATETIME());

        UPDATE dbo.tblRoomAssignments
        SET    DischargeDate = @Discharge
        WHERE  AssignmentID  = @TargetID;

        DECLARE @Affected INT = @@ROWCOUNT;

        -- Free the room
        UPDATE dbo.tblRooms
        SET    Status    = N'Available',
               UpdatedAt = SYSUTCDATETIME()
        WHERE  RoomNo = @TargetRoom;

        COMMIT TRAN;

        SELECT @Affected AS AffectedRows;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- List current (open) assignments
CREATE PROCEDURE dbo.usp_tblRoomAssignments_GetOpen
AS
BEGIN
    SET NOCOUNT ON;
    SELECT  a.AssignmentID,
            a.PatientID,
            p.FirstName + N' ' + p.LastName AS PatientName,
            a.RoomNo,
            r.RoomType,
            a.AdmitDate,
            a.Notes
    FROM    dbo.tblRoomAssignments a
    INNER JOIN dbo.tblPatients p ON p.PatientID = a.PatientID
    INNER JOIN dbo.tblRooms    r ON r.RoomNo    = a.RoomNo
    WHERE   a.DischargeDate IS NULL
    ORDER BY a.AdmitDate;
END
GO

-- History for a patient or room
CREATE PROCEDURE dbo.usp_tblRoomAssignments_GetHistory
    @PatientID INT          = NULL,
    @RoomNo    NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT  a.AssignmentID,
            a.PatientID,
            p.FirstName + N' ' + p.LastName AS PatientName,
            a.RoomNo,
            r.RoomType,
            a.AdmitDate,
            a.DischargeDate,
            a.Notes
    FROM    dbo.tblRoomAssignments a
    INNER JOIN dbo.tblPatients p ON p.PatientID = a.PatientID
    INNER JOIN dbo.tblRooms    r ON r.RoomNo    = a.RoomNo
    WHERE   (@PatientID IS NULL OR a.PatientID = @PatientID)
      AND   (@RoomNo    IS NULL OR a.RoomNo    = @RoomNo)
    ORDER BY a.AdmitDate DESC;
END
GO
