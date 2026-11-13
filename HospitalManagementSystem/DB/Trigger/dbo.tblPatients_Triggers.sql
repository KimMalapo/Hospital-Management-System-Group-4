CREATE TRIGGER dbo.trg_tblPatients_Insert ON dbo.tblPatients AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'INSERT', N'tblPatients',
           N'PatientID=' + CAST(i.PatientID AS NVARCHAR(50)),
           N'Name=' + ISNULL(i.FirstName,N'') + N' ' + ISNULL(i.LastName,N'') +
           N';Gender=' + ISNULL(i.Gender,N'') + N';IsActive=' + CAST(ISNULL(i.IsActive,1) AS NVARCHAR(10))
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblPatients_Update ON dbo.tblPatients AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'UPDATE', N'tblPatients',
           N'PatientID=' + CAST(i.PatientID AS NVARCHAR(50)),
           N'Name: ' + ISNULL(d.FirstName,N'') + N' ' + ISNULL(d.LastName,N'') +
           N' -> ' + ISNULL(i.FirstName,N'') + N' ' + ISNULL(i.LastName,N'') +
           N';IsActive: ' + CAST(ISNULL(d.IsActive,1) AS NVARCHAR(10)) + N' -> ' + CAST(ISNULL(i.IsActive,1) AS NVARCHAR(10))
    FROM inserted i INNER JOIN deleted d ON i.PatientID = d.PatientID;
END
GO

CREATE TRIGGER dbo.trg_tblPatients_Delete ON dbo.tblPatients AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'DELETE', N'tblPatients',
           N'PatientID=' + CAST(d.PatientID AS NVARCHAR(50)),
           N'Name=' + ISNULL(d.FirstName,N'') + N' ' + ISNULL(d.LastName,N'')
    FROM deleted d;
END
GO
