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
