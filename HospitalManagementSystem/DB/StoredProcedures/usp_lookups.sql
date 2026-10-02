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

-- UPDATE stored procedures for editing master data records

CREATE OR ALTER PROCEDURE dbo.usp_tblDiagnosisCodes_Update
	@Id INT,
	@Description NVARCHAR(400),
	@Status NVARCHAR(50),
	@Notes NVARCHAR(400) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE dbo.tblDiagnosisCodes
		SET Description = @Description,
			Status = @Status,
			Notes = @Notes
		WHERE DiagnosisCodeID = @Id;

		SELECT @@ROWCOUNT AS AffectedRows;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_tblDrugCategories_Update
	@Id INT,
	@Name NVARCHAR(200),
	@Status NVARCHAR(50),
	@Notes NVARCHAR(400) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE dbo.tblDrugCategories
		SET Name = @Name,
			Status = @Status,
			Notes = @Notes
		WHERE DrugCategoryID = @Id;

		SELECT @@ROWCOUNT AS AffectedRows;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_tblRoomTypes_Update
	@Id INT,
	@Name NVARCHAR(200),
	@Status NVARCHAR(50),
	@Notes NVARCHAR(400) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE dbo.tblRoomTypes
		SET Name = @Name,
			Status = @Status,
			Notes = @Notes
		WHERE RoomTypeID = @Id;

		SELECT @@ROWCOUNT AS AffectedRows;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_tblDepartmentTypes_Update
	@Id INT,
	@Name NVARCHAR(200),
	@Status NVARCHAR(50),
	@Notes NVARCHAR(400) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		UPDATE dbo.tblDepartmentTypes
		SET Name = @Name,
			Status = @Status,
			Notes = @Notes
		WHERE DepartmentTypeID = @Id;

		SELECT @@ROWCOUNT AS AffectedRows;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO
