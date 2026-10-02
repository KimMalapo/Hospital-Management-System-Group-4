-- Stored procedures for lookup tables: list and detail retrieval
-- Each proc returns columns: Id, Code, Name, Status, Notes

SET NOCOUNT ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_tblDiagnosisCodes_Get
	@Id INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF @Id IS NULL
	BEGIN
		SELECT
			DiagnosisCodeID AS Id,
			Code,
			Description AS Name,
			Status,
			Notes
		FROM dbo.tblDiagnosisCodes
		ORDER BY Code;
	END
	ELSE
	BEGIN
		SELECT
			DiagnosisCodeID AS Id,
			Code,
			Description AS Name,
			Status,
			Notes
		FROM dbo.tblDiagnosisCodes
		WHERE DiagnosisCodeID = @Id;
	END
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_tblDrugCategories_Get
	@Id INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF @Id IS NULL
	BEGIN
		SELECT
			DrugCategoryID AS Id,
			Code,
			Name,
			Status,
			Notes
		FROM dbo.tblDrugCategories
		ORDER BY Code;
	END
	ELSE
	BEGIN
		SELECT
			DrugCategoryID AS Id,
			Code,
			Name,
			Status,
			Notes
		FROM dbo.tblDrugCategories
		WHERE DrugCategoryID = @Id;
	END
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_tblRoomTypes_Get
	@Id INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF @Id IS NULL
	BEGIN
		SELECT
			RoomTypeID AS Id,
			Code,
			Name,
			Status,
			Notes
		FROM dbo.tblRoomTypes
		ORDER BY Code;
	END
	ELSE
	BEGIN
		SELECT
			RoomTypeID AS Id,
			Code,
			Name,
			Status,
			Notes
		FROM dbo.tblRoomTypes
		WHERE RoomTypeID = @Id;
	END
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_tblDepartmentTypes_Get
	@Id INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	IF @Id IS NULL
	BEGIN
		SELECT
			DepartmentTypeID AS Id,
			Code,
			Name,
			Status,
			Notes
		FROM dbo.tblDepartmentTypes
		ORDER BY Code;
	END
	ELSE
	BEGIN
		SELECT
			DepartmentTypeID AS Id,
			Code,
			Name,
			Status,
			Notes
		FROM dbo.tblDepartmentTypes
		WHERE DepartmentTypeID = @Id;
	END
END
GO
