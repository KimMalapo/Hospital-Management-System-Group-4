-- Master Data Search Stored Procedures

CREATE OR ALTER PROCEDURE usp_tblRoomTypes_Search
	@SearchTerm NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, Code, Name, Status, Notes, LastUpdated
	FROM tblRoomTypes
	WHERE Code LIKE '%' + @SearchTerm + '%' OR Name LIKE '%' + @SearchTerm + '%'
	ORDER BY Code, Name;
END;

CREATE OR ALTER PROCEDURE usp_tblDiagnosisCodes_Search
	@SearchTerm NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, Code, Name, Status, Notes, LastUpdated
	FROM tblDiagnosisCodes
	WHERE Code LIKE '%' + @SearchTerm + '%' OR Name LIKE '%' + @SearchTerm + '%'
	ORDER BY Code, Name;
END;

CREATE OR ALTER PROCEDURE usp_tblDrugCategories_Search
	@SearchTerm NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, Code, Name, Status, Notes, LastUpdated
	FROM tblDrugCategories
	WHERE Code LIKE '%' + @SearchTerm + '%' OR Name LIKE '%' + @SearchTerm + '%'
	ORDER BY Code, Name;
END;

CREATE OR ALTER PROCEDURE usp_tblDepartmentTypes_Search
	@SearchTerm NVARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, Code, Name, Status, Notes, LastUpdated
	FROM tblDepartmentTypes
	WHERE Code LIKE '%' + @SearchTerm + '%' OR Name LIKE '%' + @SearchTerm + '%'
	ORDER BY Code, Name;
END;
