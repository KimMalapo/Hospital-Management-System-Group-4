-- Create audit log table (if missing)
IF OBJECT_ID('dbo.tblAuditLog', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.tblAuditLog
	(
		AuditLogID    INT IDENTITY(1,1) PRIMARY KEY,
		Username      NVARCHAR(200) NULL,
		Action        NVARCHAR(50)  NOT NULL,
		TableName     NVARCHAR(200) NOT NULL,
		KeyValues     NVARCHAR(1000) NULL,
		ChangedValues NVARCHAR(MAX)  NULL,
		CreatedAt     DATETIME2      NOT NULL DEFAULT SYSUTCDATETIME()
	);
END
GO

-- Lookup: Diagnosis Codes
IF OBJECT_ID('dbo.tblDiagnosisCodes', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.tblDiagnosisCodes
	(
		DiagnosisCodeID INT IDENTITY(1,1) PRIMARY KEY,
		Code            NVARCHAR(50)  NOT NULL,
		Description     NVARCHAR(400) NOT NULL,
		Status          NVARCHAR(50)  NOT NULL,
		Notes           NVARCHAR(400) NULL,
		CONSTRAINT UQ_tblDiagnosisCodes_Code UNIQUE (Code)
	);
END
GO

-- Lookup: Drug Categories
IF OBJECT_ID('dbo.tblDrugCategories', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.tblDrugCategories
	(
		DrugCategoryID INT IDENTITY(1,1) PRIMARY KEY,
		Code           NVARCHAR(50)  NOT NULL,
		Name           NVARCHAR(200) NOT NULL,
		Status         NVARCHAR(50)  NOT NULL,
		Notes          NVARCHAR(400) NULL,
		CONSTRAINT UQ_tblDrugCategories_Code UNIQUE (Code)
	);
END
GO

-- Lookup: Room Types
IF OBJECT_ID('dbo.tblRoomTypes', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.tblRoomTypes
	(
		RoomTypeID INT IDENTITY(1,1) PRIMARY KEY,
		Code       NVARCHAR(50)  NOT NULL,
		Name       NVARCHAR(200) NOT NULL,
		Status     NVARCHAR(50)  NOT NULL,
		Notes      NVARCHAR(400) NULL,
		CONSTRAINT UQ_tblRoomTypes_Code UNIQUE (Code)
	);
END
GO

-- Lookup: Department Types
IF OBJECT_ID('dbo.tblDepartmentTypes', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.tblDepartmentTypes
	(
		DepartmentTypeID INT IDENTITY(1,1) PRIMARY KEY,
		Code             NVARCHAR(50)  NOT NULL,
		Name             NVARCHAR(200) NOT NULL,
		Status           NVARCHAR(50)  NOT NULL,
		Notes            NVARCHAR(400) NULL,
		CONSTRAINT UQ_tblDepartmentTypes_Code UNIQUE (Code)
	);
END
GO

-- Triggers: ensure up-to-date definitions by recreating triggers (DROP IF EXISTS + CREATE)
-- DiagnosisCodes: INSERT
DROP TRIGGER IF EXISTS dbo.trg_tblDiagnosisCodes_Insert;
GO
CREATE TRIGGER dbo.trg_tblDiagnosisCodes_Insert
ON dbo.tblDiagnosisCodes
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'INSERT', 'tblDiagnosisCodes',
		   'DiagnosisCodeID=' + CAST(i.DiagnosisCodeID AS NVARCHAR(50)),
		   'Code=' + ISNULL(i.Code,'') + ';Description=' + ISNULL(i.Description,'') + ';Status=' + ISNULL(i.Status,'')
	FROM inserted i;
END
GO

-- DrugCategories: INSERT
DROP TRIGGER IF EXISTS dbo.trg_tblDrugCategories_Insert;
GO
CREATE TRIGGER dbo.trg_tblDrugCategories_Insert
ON dbo.tblDrugCategories
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'INSERT', 'tblDrugCategories',
		   'DrugCategoryID=' + CAST(i.DrugCategoryID AS NVARCHAR(50)),
		   'Code=' + ISNULL(i.Code,'') + ';Name=' + ISNULL(i.Name,'') + ';Status=' + ISNULL(i.Status,'')
	FROM inserted i;
END
GO

-- RoomTypes: INSERT
DROP TRIGGER IF EXISTS dbo.trg_tblRoomTypes_Insert;
GO
CREATE TRIGGER dbo.trg_tblRoomTypes_Insert
ON dbo.tblRoomTypes
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'INSERT', 'tblRoomTypes',
		   'RoomTypeID=' + CAST(i.RoomTypeID AS NVARCHAR(50)),
		   'Code=' + ISNULL(i.Code,'') + ';Name=' + ISNULL(i.Name,'') + ';Status=' + ISNULL(i.Status,'')
	FROM inserted i;
END
GO

-- DepartmentTypes: INSERT
DROP TRIGGER IF EXISTS dbo.trg_tblDepartmentTypes_Insert;
GO
CREATE TRIGGER dbo.trg_tblDepartmentTypes_Insert
ON dbo.tblDepartmentTypes
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'INSERT', 'tblDepartmentTypes',
		   'DepartmentTypeID=' + CAST(i.DepartmentTypeID AS NVARCHAR(50)),
		   'Code=' + ISNULL(i.Code,'') + ';Name=' + ISNULL(i.Name,'') + ';Status=' + ISNULL(i.Status,'')
	FROM inserted i;
END
GO

-- Seed: insert example master data only if missing
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = 'A00')
	INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status, Notes) VALUES ('A00', 'Cholera', 'Active', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = 'ANTIB')
	INSERT INTO dbo.tblDrugCategories (Code, Name, Status, Notes) VALUES ('ANTIB', 'Antibiotics', 'Active', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = 'GEN')
	INSERT INTO dbo.tblRoomTypes (Code, Name, Status, Notes) VALUES ('GEN', 'General Ward', 'Active', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = 'EMR')
	INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status, Notes) VALUES ('EMR', 'Emergency', 'Active', NULL);
GO


