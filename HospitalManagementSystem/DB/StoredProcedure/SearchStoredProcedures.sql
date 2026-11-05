-- Master data search procedures (active rows only)

CREATE PROCEDURE dbo.usp_tblDiagnosisCodes_Search
    @SearchTerm NVARCHAR(200) = N''
AS
BEGIN
    SET NOCOUNT ON;
    SET @SearchTerm = ISNULL(@SearchTerm, N'');

    SELECT DiagnosisCodeID AS Id, Code, Description AS Name, Status, Notes, IsDeleted, LastUpdated
    FROM dbo.tblDiagnosisCodes
    WHERE IsDeleted = 0
      AND (Code LIKE N'%' + @SearchTerm + N'%' OR Description LIKE N'%' + @SearchTerm + N'%')
    ORDER BY Code, Description;
END
GO

CREATE PROCEDURE dbo.usp_tblDrugCategories_Search
    @SearchTerm NVARCHAR(200) = N''
AS
BEGIN
    SET NOCOUNT ON;
    SET @SearchTerm = ISNULL(@SearchTerm, N'');

    SELECT DrugCategoryID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
    FROM dbo.tblDrugCategories
    WHERE IsDeleted = 0
      AND (Code LIKE N'%' + @SearchTerm + N'%' OR Name LIKE N'%' + @SearchTerm + N'%')
    ORDER BY Code, Name;
END
GO

CREATE PROCEDURE dbo.usp_tblRoomTypes_Search
    @SearchTerm NVARCHAR(200) = N''
AS
BEGIN
    SET NOCOUNT ON;
    SET @SearchTerm = ISNULL(@SearchTerm, N'');

    SELECT RoomTypeID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
    FROM dbo.tblRoomTypes
    WHERE IsDeleted = 0
      AND (Code LIKE N'%' + @SearchTerm + N'%' OR Name LIKE N'%' + @SearchTerm + N'%')
    ORDER BY Code, Name;
END
GO

CREATE PROCEDURE dbo.usp_tblDepartmentTypes_Search
    @SearchTerm NVARCHAR(200) = N''
AS
BEGIN
    SET NOCOUNT ON;
    SET @SearchTerm = ISNULL(@SearchTerm, N'');

    SELECT DepartmentTypeID AS Id, Code, Name, Status, Notes, IsDeleted, LastUpdated
    FROM dbo.tblDepartmentTypes
    WHERE IsDeleted = 0
      AND (Code LIKE N'%' + @SearchTerm + N'%' OR Name LIKE N'%' + @SearchTerm + N'%')
    ORDER BY Code, Name;
END
GO
