CREATE TRIGGER dbo.trg_tblDiagnosisCodes_Insert ON dbo.tblDiagnosisCodes AFTER INSERT AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'INSERT', N'tblDiagnosisCodes',
           N'DiagnosisCodeID=' + CAST(i.DiagnosisCodeID AS NVARCHAR(50)),
           N'Code=' + ISNULL(i.Code,N'') + N';Description=' + ISNULL(i.Description,N'') +
           N';Status=' + ISNULL(i.Status,N'') + N';IsDeleted=' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblDiagnosisCodes_Update ON dbo.tblDiagnosisCodes AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'UPDATE', N'tblDiagnosisCodes',
           N'DiagnosisCodeID=' + CAST(i.DiagnosisCodeID AS NVARCHAR(50)),
           N'Description: ' + ISNULL(d.Description,N'') + N' -> ' + ISNULL(i.Description,N'') +
           N';Status: ' + ISNULL(d.Status,N'') + N' -> ' + ISNULL(i.Status,N'') +
           N';IsDeleted: ' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10)) + N' -> ' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
    FROM inserted i INNER JOIN deleted d ON i.DiagnosisCodeID = d.DiagnosisCodeID;
END
GO

CREATE TRIGGER dbo.trg_tblDiagnosisCodes_Delete ON dbo.tblDiagnosisCodes AFTER DELETE AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'DELETE', N'tblDiagnosisCodes',
           N'DiagnosisCodeID=' + CAST(d.DiagnosisCodeID AS NVARCHAR(50)),
           N'Code=' + ISNULL(d.Code,N'') + N';Description=' + ISNULL(d.Description,N'')
    FROM deleted d;
END
GO
