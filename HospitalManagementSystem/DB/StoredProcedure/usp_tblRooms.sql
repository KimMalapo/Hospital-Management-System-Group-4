-- Stored procedures used by RoomService

CREATE PROCEDURE dbo.usp_tblRooms_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT r.RoomNo, r.RoomType, r.Rate, r.Capacity,
           (SELECT COUNT(*) FROM dbo.tblRoomAssignments a WHERE a.RoomNo = r.RoomNo AND a.DischargeDate IS NULL) AS OccupiedBeds,
           CASE WHEN r.Status IN (N'Maintenance', N'Reserved') THEN r.Status
                WHEN (SELECT COUNT(*) FROM dbo.tblRoomAssignments a WHERE a.RoomNo = r.RoomNo AND a.DischargeDate IS NULL) >= r.Capacity
                THEN N'Occupied' ELSE N'Available' END AS Status,
           r.IsActive,
           r.CreatedAt, r.UpdatedAt,
           rt.Name AS RoomTypeName
    FROM   dbo.tblRooms r
    LEFT JOIN dbo.tblRoomTypes rt
           ON rt.Code = r.RoomType AND rt.IsDeleted = 0
    WHERE  r.IsActive = 1
    ORDER  BY r.RoomNo;
END
GO

CREATE PROCEDURE dbo.usp_tblRooms_GetById
    @RoomNo NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT r.RoomNo, r.RoomType, r.Rate, r.Capacity,
           (SELECT COUNT(*) FROM dbo.tblRoomAssignments a WHERE a.RoomNo = r.RoomNo AND a.DischargeDate IS NULL) AS OccupiedBeds,
           CASE WHEN r.Status IN (N'Maintenance', N'Reserved') THEN r.Status
                WHEN (SELECT COUNT(*) FROM dbo.tblRoomAssignments a WHERE a.RoomNo = r.RoomNo AND a.DischargeDate IS NULL) >= r.Capacity
                THEN N'Occupied' ELSE N'Available' END AS Status,
           r.IsActive,
           r.CreatedAt, r.UpdatedAt,
           rt.Name AS RoomTypeName
    FROM   dbo.tblRooms r
    LEFT JOIN dbo.tblRoomTypes rt
           ON rt.Code = r.RoomType AND rt.IsDeleted = 0
    WHERE  r.RoomNo = @RoomNo;
END
GO

CREATE PROCEDURE dbo.usp_tblRooms_Create
    @RoomNo   NVARCHAR(20),
    @RoomType NVARCHAR(50),
    @Rate     DECIMAL(10,2),
    @Capacity INT = 1
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Validate room type exists and is active
        IF NOT EXISTS (
            SELECT 1 FROM dbo.tblRoomTypes
            WHERE Code = @RoomType AND IsDeleted = 0 AND Status = N'Active')
        BEGIN
            THROW 50001, N'The selected room type does not exist or is not active.', 1;
        END

        IF @Capacity < 1
            THROW 50002, N'Room capacity must be at least one bed.', 1;

        INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity, Status)
        VALUES (@RoomNo, @RoomType, @Rate, @Capacity, N'Available');

        SELECT @@ROWCOUNT AS AffectedRows;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblRooms_Update
    @RoomNo   NVARCHAR(20),
    @RoomType NVARCHAR(50),
    @Rate     DECIMAL(10,2),
    @Capacity INT = 1
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @CurrentStatus NVARCHAR(30);
        SELECT @CurrentStatus = Status
        FROM dbo.tblRooms WITH (UPDLOCK, HOLDLOCK)
        WHERE RoomNo = @RoomNo AND IsActive = 1;

        IF @CurrentStatus IS NULL
            THROW 50003, N'The selected room was not found or is inactive.', 1;

        IF NOT EXISTS (
            SELECT 1 FROM dbo.tblRoomTypes
            WHERE Code = @RoomType AND IsDeleted = 0 AND Status = N'Active')
        BEGIN
            THROW 50001, N'The selected room type does not exist or is not active.', 1;
        END

        IF @Capacity < 1
            THROW 50002, N'Room capacity must be at least one bed.', 1;

        IF (SELECT COUNT(*) FROM dbo.tblRoomAssignments WITH (UPDLOCK, HOLDLOCK)
            WHERE RoomNo = @RoomNo AND DischargeDate IS NULL) > @Capacity
            THROW 50004, N'Room capacity cannot be less than the number of admitted patients.', 1;

        -- An occupied room keeps its type and rate until the patient leaves
        IF EXISTS (SELECT 1 FROM dbo.tblRoomAssignments WHERE RoomNo = @RoomNo AND DischargeDate IS NULL)
           AND EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = @RoomNo AND (RoomType <> @RoomType OR Rate <> @Rate))
        BEGIN
            THROW 50005, N'The room is in use. Its type and rate cannot be changed while a patient is admitted.', 1;
        END

        UPDATE dbo.tblRooms
        SET    RoomType  = @RoomType,
               Rate      = @Rate,
               Capacity  = @Capacity,
               Status    = CASE WHEN @CurrentStatus IN (N'Maintenance', N'Reserved') THEN @CurrentStatus
                                WHEN (SELECT COUNT(*) FROM dbo.tblRoomAssignments
                                      WHERE RoomNo = @RoomNo AND DischargeDate IS NULL) >= @Capacity
                                THEN N'Occupied' ELSE N'Available' END,
               UpdatedAt = SYSUTCDATETIME()
        WHERE  RoomNo = @RoomNo AND IsActive = 1;

        SELECT @@ROWCOUNT AS AffectedRows;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblRooms_Delete
    @RoomNo NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM dbo.tblRoomAssignments WHERE RoomNo = @RoomNo AND DischargeDate IS NULL)
        BEGIN
            RAISERROR(N'The room is in use. Discharge the patient before deleting the room.', 16, 1);
            RETURN;
        END

        -- Soft delete
        UPDATE dbo.tblRooms
        SET    IsActive  = 0,
               Status    = N'Maintenance',
               UpdatedAt = SYSUTCDATETIME()
        WHERE  RoomNo = @RoomNo;

        SELECT @@ROWCOUNT AS AffectedRows;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
