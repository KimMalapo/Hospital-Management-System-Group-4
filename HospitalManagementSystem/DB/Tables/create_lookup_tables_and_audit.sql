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

-- Lookup: Diagnosis Codes (adds IsDeleted for soft-delete and filtered unique index on Code for active rows)
IF OBJECT_ID('dbo.tblDiagnosisCodes', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.tblDiagnosisCodes
	(
		DiagnosisCodeID INT IDENTITY(1,1) PRIMARY KEY,
		Code            NVARCHAR(50)  NOT NULL,
		Description     NVARCHAR(400) NOT NULL,
		Status          NVARCHAR(50)  NOT NULL,
		Notes           NVARCHAR(400) NULL,
		IsDeleted       BIT            NOT NULL DEFAULT (0)
	);
	-- enforce Code uniqueness only for non-deleted (active) rows so soft-deleted codes can be retained
	IF NOT EXISTS (SELECT 1 FROM sys.indexes i WHERE i.name = 'UQ_tblDiagnosisCodes_Code_Active')
		CREATE UNIQUE INDEX UQ_tblDiagnosisCodes_Code_Active ON dbo.tblDiagnosisCodes(Code) WHERE IsDeleted = 0;
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
		IsDeleted      BIT            NOT NULL DEFAULT (0)
	);
	IF NOT EXISTS (SELECT 1 FROM sys.indexes i WHERE i.name = 'UQ_tblDrugCategories_Code_Active')
		CREATE UNIQUE INDEX UQ_tblDrugCategories_Code_Active ON dbo.tblDrugCategories(Code) WHERE IsDeleted = 0;
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
		IsDeleted  BIT            NOT NULL DEFAULT (0)
	);
	IF NOT EXISTS (SELECT 1 FROM sys.indexes i WHERE i.name = 'UQ_tblRoomTypes_Code_Active')
		CREATE UNIQUE INDEX UQ_tblRoomTypes_Code_Active ON dbo.tblRoomTypes(Code) WHERE IsDeleted = 0;
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
		IsDeleted        BIT            NOT NULL DEFAULT (0)
	);
	IF NOT EXISTS (SELECT 1 FROM sys.indexes i WHERE i.name = 'UQ_tblDepartmentTypes_Code_Active')
		CREATE UNIQUE INDEX UQ_tblDepartmentTypes_Code_Active ON dbo.tblDepartmentTypes(Code) WHERE IsDeleted = 0;
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
		   'Code=' + ISNULL(i.Code,'') + ';Description=' + ISNULL(i.Description,'') + ';Status=' + ISNULL(i.Status,'') + ';IsDeleted=' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
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
		   'Code=' + ISNULL(i.Code,'') + ';Name=' + ISNULL(i.Name,'') + ';Status=' + ISNULL(i.Status,'') + ';IsDeleted=' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
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
		   'Code=' + ISNULL(i.Code,'') + ';Name=' + ISNULL(i.Name,'') + ';Status=' + ISNULL(i.Status,'') + ';IsDeleted=' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
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
		   'Code=' + ISNULL(i.Code,'') + ';Name=' + ISNULL(i.Name,'') + ';Status=' + ISNULL(i.Status,'') + ';IsDeleted=' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
	FROM inserted i;
END
GO

-- DiagnosisCodes: UPDATE
DROP TRIGGER IF EXISTS dbo.trg_tblDiagnosisCodes_Update;
GO
CREATE TRIGGER dbo.trg_tblDiagnosisCodes_Update
ON dbo.tblDiagnosisCodes
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'UPDATE', 'tblDiagnosisCodes',
		   'DiagnosisCodeID=' + CAST(d.DiagnosisCodeID AS NVARCHAR(50)),
		   'Description: ' + ISNULL(d.Description,'') + ' -> ' + ISNULL(i.Description,'') +
		   ';Status: ' + ISNULL(d.Status,'') + ' -> ' + ISNULL(i.Status,'') +
		   ';Notes: ' + ISNULL(d.Notes,'') + ' -> ' + ISNULL(i.Notes,'') +
		   ';IsDeleted: ' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10)) + ' -> ' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
	FROM inserted i
	INNER JOIN deleted d ON i.DiagnosisCodeID = d.DiagnosisCodeID;
END
GO

-- DrugCategories: UPDATE
DROP TRIGGER IF EXISTS dbo.trg_tblDrugCategories_Update;
GO
CREATE TRIGGER dbo.trg_tblDrugCategories_Update
ON dbo.tblDrugCategories
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'UPDATE', 'tblDrugCategories',
		   'DrugCategoryID=' + CAST(d.DrugCategoryID AS NVARCHAR(50)),
		   'Name: ' + ISNULL(d.Name,'') + ' -> ' + ISNULL(i.Name,'') +
		   ';Status: ' + ISNULL(d.Status,'') + ' -> ' + ISNULL(i.Status,'') +
		   ';Notes: ' + ISNULL(d.Notes,'') + ' -> ' + ISNULL(i.Notes,'') +
		   ';IsDeleted: ' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10)) + ' -> ' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
	FROM inserted i
	INNER JOIN deleted d ON i.DrugCategoryID = d.DrugCategoryID;
END
GO

-- RoomTypes: UPDATE
DROP TRIGGER IF EXISTS dbo.trg_tblRoomTypes_Update;
GO
CREATE TRIGGER dbo.trg_tblRoomTypes_Update
ON dbo.tblRoomTypes
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'UPDATE', 'tblRoomTypes',
		   'RoomTypeID=' + CAST(d.RoomTypeID AS NVARCHAR(50)),
		   'Name: ' + ISNULL(d.Name,'') + ' -> ' + ISNULL(i.Name,'') +
		   ';Status: ' + ISNULL(d.Status,'') + ' -> ' + ISNULL(i.Status,'') +
		   ';Notes: ' + ISNULL(d.Notes,'') + ' -> ' + ISNULL(i.Notes,'') +
		   ';IsDeleted: ' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10)) + ' -> ' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
	FROM inserted i
	INNER JOIN deleted d ON i.RoomTypeID = d.RoomTypeID;
END
GO

-- DepartmentTypes: UPDATE
DROP TRIGGER IF EXISTS dbo.trg_tblDepartmentTypes_Update;
GO
CREATE TRIGGER dbo.trg_tblDepartmentTypes_Update
ON dbo.tblDepartmentTypes
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'UPDATE', 'tblDepartmentTypes',
		   'DepartmentTypeID=' + CAST(d.DepartmentTypeID AS NVARCHAR(50)),
		   'Name: ' + ISNULL(d.Name,'') + ' -> ' + ISNULL(i.Name,'') +
		   ';Status: ' + ISNULL(d.Status,'') + ' -> ' + ISNULL(i.Status,'') +
		   ';Notes: ' + ISNULL(d.Notes,'') + ' -> ' + ISNULL(i.Notes,'') +
		   ';IsDeleted: ' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10)) + ' -> ' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
	FROM inserted i
	INNER JOIN deleted d ON i.DepartmentTypeID = d.DepartmentTypeID;
END
GO

-- Add DELETE triggers to capture hard deletes (backend should prefer soft-delete by updating IsDeleted)
-- DiagnosisCodes: DELETE
DROP TRIGGER IF EXISTS dbo.trg_tblDiagnosisCodes_Delete;
GO
CREATE TRIGGER dbo.trg_tblDiagnosisCodes_Delete
ON dbo.tblDiagnosisCodes
AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'DELETE', 'tblDiagnosisCodes',
		   'DiagnosisCodeID=' + CAST(d.DiagnosisCodeID AS NVARCHAR(50)),
		   'Code=' + ISNULL(d.Code,'') + ';Description=' + ISNULL(d.Description,'') + ';Status=' + ISNULL(d.Status,'') + ';IsDeleted=' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10))
	FROM deleted d;
END
GO

-- DrugCategories: DELETE
DROP TRIGGER IF EXISTS dbo.trg_tblDrugCategories_Delete;
GO
CREATE TRIGGER dbo.trg_tblDrugCategories_Delete
ON dbo.tblDrugCategories
AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'DELETE', 'tblDrugCategories',
		   'DrugCategoryID=' + CAST(d.DrugCategoryID AS NVARCHAR(50)),
		   'Code=' + ISNULL(d.Code,'') + ';Name=' + ISNULL(d.Name,'') + ';Status=' + ISNULL(d.Status,'') + ';IsDeleted=' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10))
	FROM deleted d;
END
GO

-- RoomTypes: DELETE
DROP TRIGGER IF EXISTS dbo.trg_tblRoomTypes_Delete;
GO
CREATE TRIGGER dbo.trg_tblRoomTypes_Delete
ON dbo.tblRoomTypes
AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'DELETE', 'tblRoomTypes',
		   'RoomTypeID=' + CAST(d.RoomTypeID AS NVARCHAR(50)),
		   'Code=' + ISNULL(d.Code,'') + ';Name=' + ISNULL(d.Name,'') + ';Status=' + ISNULL(d.Status,'') + ';IsDeleted=' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10))
	FROM deleted d;
END
GO

-- DepartmentTypes: DELETE
DROP TRIGGER IF EXISTS dbo.trg_tblDepartmentTypes_Delete;
GO
CREATE TRIGGER dbo.trg_tblDepartmentTypes_Delete
ON dbo.tblDepartmentTypes
AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'DELETE', 'tblDepartmentTypes',
		   'DepartmentTypeID=' + CAST(d.DepartmentTypeID AS NVARCHAR(50)),
		   'Code=' + ISNULL(d.Code,'') + ';Name=' + ISNULL(d.Name,'') + ';Status=' + ISNULL(d.Status,'') + ';IsDeleted=' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10))
	FROM deleted d;
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



IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = 'A00')
	INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status, Notes) VALUES ('A00', 'Cholera', 'Active', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = 'ANTIB')
	INSERT INTO dbo.tblDrugCategories (Code, Name, Status, Notes) VALUES ('ANTIB', 'Antibiotics', 'Active', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = 'GEN')
	INSERT INTO dbo.tblRoomTypes (Code, Name, Status, Notes) VALUES ('GEN', 'General Ward', 'Active', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = 'EMR')
	INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status, Notes) VALUES ('EMR', 'Emergency', 'Active', NULL);
GO


