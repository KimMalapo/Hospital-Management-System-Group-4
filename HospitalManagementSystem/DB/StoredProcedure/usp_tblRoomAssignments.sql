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

        IF EXISTS (SELECT 1 FROM dbo.tblAdmissions WHERE PatientID = @PatientID AND Status = N'Admitted')
        BEGIN
            RAISERROR(N'This patient is already admitted.', 16, 1);
        END

        IF EXISTS (SELECT 1 FROM dbo.tblRoomAssignments WHERE PatientID = @PatientID AND DischargeDate IS NULL)
            RAISERROR(N'This patient already has an active room assignment.', 16, 1);

        -- Lock the room row before counting assignments to serialize capacity checks.
        DECLARE @Capacity INT, @RoomStatus NVARCHAR(30);
        SELECT @Capacity = Capacity, @RoomStatus = Status
        FROM dbo.tblRooms WITH (UPDLOCK, HOLDLOCK)
        WHERE RoomNo = @RoomNo AND IsActive = 1;

        IF @Capacity IS NULL OR @RoomStatus IN (N'Maintenance', N'Reserved')
        BEGIN
            RAISERROR(N'The room is not available.', 16, 1);
        END

        DECLARE @Occupied INT =
            (SELECT COUNT(*) FROM dbo.tblRoomAssignments WITH (UPDLOCK, HOLDLOCK)
             WHERE RoomNo = @RoomNo AND DischargeDate IS NULL);
        IF @Occupied >= @Capacity
        BEGIN
            RAISERROR(N'The room is at full capacity.', 16, 1);
        END

        DECLARE @Admit DATETIME2(0) = ISNULL(@AdmitDate, SYSUTCDATETIME());

        INSERT INTO dbo.tblRoomAssignments (PatientID, RoomNo, AdmitDate, Notes, CreatedBy)
        VALUES (@PatientID, @RoomNo, @Admit, @Notes, @CreatedBy);

        -- Occupied means every bed is currently assigned; otherwise the room remains available.
        UPDATE dbo.tblRooms
        SET    Status    = CASE WHEN @Occupied + 1 >= Capacity THEN N'Occupied' ELSE N'Available' END,
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

        -- Serialize recalculation with admissions to the same room.
        SELECT @TargetRoom = RoomNo
        FROM dbo.tblRooms WITH (UPDLOCK, HOLDLOCK)
        WHERE RoomNo = @TargetRoom;

        DECLARE @Discharge DATETIME2(0) = ISNULL(@DischargeDate, SYSUTCDATETIME());

        UPDATE dbo.tblRoomAssignments
        SET    DischargeDate = @Discharge
        WHERE  AssignmentID  = @TargetID;

        DECLARE @Affected INT = @@ROWCOUNT;

        -- Recalculate room status after closing this patient's assignment.
        UPDATE dbo.tblRooms
        SET    Status    = CASE WHEN (SELECT COUNT(*) FROM dbo.tblRoomAssignments
                                      WHERE RoomNo = @TargetRoom AND DischargeDate IS NULL) >= Capacity
                                THEN N'Occupied' ELSE N'Available' END,
               UpdatedAt = SYSUTCDATETIME()
        WHERE  RoomNo = @TargetRoom AND Status IN (N'Available', N'Occupied');

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
