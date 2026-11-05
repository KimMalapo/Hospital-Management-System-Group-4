-- Create (returns new Id)

CREATE PROCEDURE dbo.usp_tblDiagnosisCodes_Create
    @Code NVARCHAR(50), @Name NVARCHAR(400), @Status NVARCHAR(50) = N'Active', @Notes NVARCHAR(400) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status, Notes, LastUpdated)
    VALUES (@Code, @Name, @Status, @Notes, SYSUTCDATETIME());
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
END
GO

CREATE PROCEDURE dbo.usp_tblDrugCategories_Create
    @Code NVARCHAR(50), @Name NVARCHAR(200), @Status NVARCHAR(50) = N'Active', @Notes NVARCHAR(400) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status, Notes, LastUpdated)
    VALUES (@Code, @Name, @Status, @Notes, SYSUTCDATETIME());
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
END
GO

CREATE PROCEDURE dbo.usp_tblRoomTypes_Create
    @Code NVARCHAR(50), @Name NVARCHAR(200), @Status NVARCHAR(50) = N'Active', @Notes NVARCHAR(400) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status, Notes, LastUpdated)
    VALUES (@Code, @Name, @Status, @Notes, SYSUTCDATETIME());
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
END
GO

CREATE PROCEDURE dbo.usp_tblDepartmentTypes_Create
    @Code NVARCHAR(50), @Name NVARCHAR(200), @Status NVARCHAR(50) = N'Active', @Notes NVARCHAR(400) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status, Notes, LastUpdated)
    VALUES (@Code, @Name, @Status, @Notes, SYSUTCDATETIME());
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
END
GO

-- Update

CREATE PROCEDURE dbo.usp_tblDiagnosisCodes_Update
    @Id INT,
    @Name NVARCHAR(400),
    @Status NVARCHAR(50),
    @Notes NVARCHAR(400) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Code is not editable so existing relationships are not broken.
    UPDATE dbo.tblDiagnosisCodes
    SET Description = @Name,
        Status = @Status,
        Notes = @Notes,
        LastUpdated = SYSUTCDATETIME()
    WHERE DiagnosisCodeID = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

CREATE PROCEDURE dbo.usp_tblDrugCategories_Update
    @Id INT,
    @Name NVARCHAR(200),
    @Status NVARCHAR(50),
    @Notes NVARCHAR(400) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Code is not editable so existing relationships are not broken.
    UPDATE dbo.tblDrugCategories
    SET Name = @Name,
        Status = @Status,
        Notes = @Notes,
        LastUpdated = SYSUTCDATETIME()
    WHERE DrugCategoryID = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

CREATE PROCEDURE dbo.usp_tblRoomTypes_Update
    @Id INT,
    @Name NVARCHAR(200),
    @Status NVARCHAR(50),
    @Notes NVARCHAR(400) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Code is not editable so existing relationships are not broken.
    UPDATE dbo.tblRoomTypes
    SET Name = @Name,
        Status = @Status,
        Notes = @Notes,
        LastUpdated = SYSUTCDATETIME()
    WHERE RoomTypeID = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

CREATE PROCEDURE dbo.usp_tblDepartmentTypes_Update
    @Id INT,
    @Name NVARCHAR(200),
    @Status NVARCHAR(50),
    @Notes NVARCHAR(400) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Code is not editable so existing relationships are not broken.
    UPDATE dbo.tblDepartmentTypes
    SET Name = @Name,
        Status = @Status,
        Notes = @Notes,
        LastUpdated = SYSUTCDATETIME()
    WHERE DepartmentTypeID = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- SoftDelete (used by LookupRepository.SoftDelete)

CREATE PROCEDURE dbo.usp_tblDiagnosisCodes_SoftDelete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.tblDiagnosisCodes
    SET IsDeleted = 1, Status = N'Inactive', LastUpdated = SYSUTCDATETIME()
    WHERE DiagnosisCodeID = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

CREATE PROCEDURE dbo.usp_tblDrugCategories_SoftDelete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.tblDrugCategories
    SET IsDeleted = 1, Status = N'Inactive', LastUpdated = SYSUTCDATETIME()
    WHERE DrugCategoryID = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

CREATE PROCEDURE dbo.usp_tblRoomTypes_SoftDelete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.tblRoomTypes
    SET IsDeleted = 1, Status = N'Inactive', LastUpdated = SYSUTCDATETIME()
    WHERE RoomTypeID = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

CREATE PROCEDURE dbo.usp_tblDepartmentTypes_SoftDelete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.tblDepartmentTypes
    SET IsDeleted = 1, Status = N'Inactive', LastUpdated = SYSUTCDATETIME()
    WHERE DepartmentTypeID = @Id AND IsDeleted = 0;

    SELECT @@ROWCOUNT AS AffectedRows;
END
GO

-- CheckCodeExists (validation; active rows only)

CREATE PROCEDURE dbo.usp_tblDiagnosisCodes_CheckCodeExists
    @Code NVARCHAR(50),
    @ExcludeId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Only active rows count: the unique index ignores soft-deleted codes.
    SELECT COUNT(1) AS CodeExists
    FROM dbo.tblDiagnosisCodes
    WHERE Code = @Code AND IsDeleted = 0
      AND (@ExcludeId IS NULL OR DiagnosisCodeID <> @ExcludeId);
END
GO

CREATE PROCEDURE dbo.usp_tblDrugCategories_CheckCodeExists
    @Code NVARCHAR(50),
    @ExcludeId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Only active rows count: the unique index ignores soft-deleted codes.
    SELECT COUNT(1) AS CodeExists
    FROM dbo.tblDrugCategories
    WHERE Code = @Code AND IsDeleted = 0
      AND (@ExcludeId IS NULL OR DrugCategoryID <> @ExcludeId);
END
GO

CREATE PROCEDURE dbo.usp_tblRoomTypes_CheckCodeExists
    @Code NVARCHAR(50),
    @ExcludeId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Only active rows count: the unique index ignores soft-deleted codes.
    SELECT COUNT(1) AS CodeExists
    FROM dbo.tblRoomTypes
    WHERE Code = @Code AND IsDeleted = 0
      AND (@ExcludeId IS NULL OR RoomTypeID <> @ExcludeId);
END
GO

CREATE PROCEDURE dbo.usp_tblDepartmentTypes_CheckCodeExists
    @Code NVARCHAR(50),
    @ExcludeId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Only active rows count: the unique index ignores soft-deleted codes.
    SELECT COUNT(1) AS CodeExists
    FROM dbo.tblDepartmentTypes
    WHERE Code = @Code AND IsDeleted = 0
      AND (@ExcludeId IS NULL OR DepartmentTypeID <> @ExcludeId);
END
GO
