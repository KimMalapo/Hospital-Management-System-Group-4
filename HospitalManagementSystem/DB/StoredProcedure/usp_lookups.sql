-- Lookup procedures: list / detail (soft-delete aware)

CREATE PROCEDURE dbo.usp_tblDiagnosisCodes_Get
    @Id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- No @Id: list active rows only. With @Id: return that row even if soft-deleted.
    IF @Id IS NULL
        SELECT DiagnosisCodeID AS Id, Code, Description AS Name, Status, Notes, IsDeleted, LastUpdated
        FROM dbo.tblDiagnosisCodes WHERE IsDeleted = 0 ORDER BY Code;
    ELSE
        SELECT DiagnosisCodeID AS Id, Code, Description AS Name, Status, Notes, IsDeleted, LastUpdated
        FROM dbo.tblDiagnosisCodes WHERE DiagnosisCodeID = @Id;
END
GO

CREATE PROCEDURE dbo.usp_tblDrugCategories_Get
    @Id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- No @Id: list active rows only. With @Id: return that row even if soft-deleted.
    IF @Id IS NULL
        SELECT DrugCategoryID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
        FROM dbo.tblDrugCategories WHERE IsDeleted = 0 ORDER BY Code;
    ELSE
        SELECT DrugCategoryID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
        FROM dbo.tblDrugCategories WHERE DrugCategoryID = @Id;
END
GO

CREATE PROCEDURE dbo.usp_tblRoomTypes_Get
    @Id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- No @Id: list active rows only. With @Id: return that row even if soft-deleted.
    IF @Id IS NULL
        SELECT RoomTypeID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
        FROM dbo.tblRoomTypes WHERE IsDeleted = 0 ORDER BY Code;
    ELSE
        SELECT RoomTypeID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
        FROM dbo.tblRoomTypes WHERE RoomTypeID = @Id;
END
GO

CREATE PROCEDURE dbo.usp_tblDepartmentTypes_Get
    @Id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- No @Id: list active rows only. With @Id: return that row even if soft-deleted.
    IF @Id IS NULL
        SELECT DepartmentTypeID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
        FROM dbo.tblDepartmentTypes WHERE IsDeleted = 0 ORDER BY Code;
    ELSE
        SELECT DepartmentTypeID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
        FROM dbo.tblDepartmentTypes WHERE DepartmentTypeID = @Id;
END
GO

-- GetById

CREATE PROCEDURE dbo.usp_tblDiagnosisCodes_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DiagnosisCodeID AS Id, Code, Description AS Name, Status, Notes, IsDeleted, LastUpdated
    FROM dbo.tblDiagnosisCodes WHERE DiagnosisCodeID = @Id;
END
GO

CREATE PROCEDURE dbo.usp_tblDrugCategories_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DrugCategoryID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
    FROM dbo.tblDrugCategories WHERE DrugCategoryID = @Id;
END
GO

CREATE PROCEDURE dbo.usp_tblRoomTypes_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT RoomTypeID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
    FROM dbo.tblRoomTypes WHERE RoomTypeID = @Id;
END
GO

CREATE PROCEDURE dbo.usp_tblDepartmentTypes_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DepartmentTypeID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
    FROM dbo.tblDepartmentTypes WHERE DepartmentTypeID = @Id;
END
GO
