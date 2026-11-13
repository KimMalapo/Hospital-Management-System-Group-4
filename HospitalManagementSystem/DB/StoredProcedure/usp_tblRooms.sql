-- Stored procedures used by RoomService

CREATE PROCEDURE dbo.usp_tblRooms_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT r.RoomNo, r.RoomType, r.Rate, r.Status, r.IsActive,
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
    SELECT r.RoomNo, r.RoomType, r.Rate, r.Status, r.IsActive,
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
    @Rate     DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Validate room type exists and is active
        IF NOT EXISTS (
            SELECT 1 FROM dbo.tblRoomTypes
            WHERE Code = @RoomType AND IsDeleted = 0 AND Status = N'Active')
        BEGIN
            RAISERROR(N'Room type ''%s'' does not exist or is not active.', 16, 1, @RoomType);
            RETURN;
        END

        INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Status)
        VALUES (@RoomNo, @RoomType, @Rate, N'Available');

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
    @Rate     DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (
            SELECT 1 FROM dbo.tblRoomTypes
            WHERE Code = @RoomType AND IsDeleted = 0 AND Status = N'Active')
        BEGIN
            RAISERROR(N'Room type ''%s'' does not exist or is not active.', 16, 1, @RoomType);
            RETURN;
        END

        -- An occupied room keeps its type and rate until the patient leaves
        IF EXISTS (SELECT 1 FROM dbo.tblRoomAssignments WHERE RoomNo = @RoomNo AND DischargeDate IS NULL)
           AND EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = @RoomNo AND (RoomType <> @RoomType OR Rate <> @Rate))
        BEGIN
            RAISERROR(N'The room is in use. Its type and rate cannot be changed while a patient is admitted.', 16, 1);
            RETURN;
        END

        UPDATE dbo.tblRooms
        SET    RoomType  = @RoomType,
               Rate      = @Rate,
               UpdatedAt = SYSUTCDATETIME()
        WHERE  RoomNo = @RoomNo AND IsActive = 1;

        SELECT @@ROWCOUNT AS AffectedRows;
    END TRY
    BEGIN CATCH
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
