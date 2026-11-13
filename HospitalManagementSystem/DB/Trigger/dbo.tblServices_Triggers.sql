CREATE TRIGGER dbo.trg_tblServices_Insert ON dbo.tblServices AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'INSERT', N'tblServices',
           N'ServiceID=' + CAST(i.ServiceID AS NVARCHAR(20)),
           N'Code=' + ISNULL(CAST(i.Code AS NVARCHAR(100)),N'') + N';' + N'Name=' + ISNULL(CAST(i.Name AS NVARCHAR(100)),N'') + N';' + N'Category=' + ISNULL(CAST(i.Category AS NVARCHAR(100)),N'') + N';' + N'UnitPrice=' + ISNULL(CAST(i.UnitPrice AS NVARCHAR(100)),N'') + N';' + N'IsActive=' + ISNULL(CAST(i.IsActive AS NVARCHAR(100)),N'')
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblServices_Update ON dbo.tblServices AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'UPDATE', N'tblServices',
           N'ServiceID=' + CAST(i.ServiceID AS NVARCHAR(20)),
           N'Code: ' + ISNULL(CAST(d.Code AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Code AS NVARCHAR(100)),N'') + N';' + N'Name: ' + ISNULL(CAST(d.Name AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Name AS NVARCHAR(100)),N'') + N';' + N'Category: ' + ISNULL(CAST(d.Category AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Category AS NVARCHAR(100)),N'') + N';' + N'UnitPrice: ' + ISNULL(CAST(d.UnitPrice AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.UnitPrice AS NVARCHAR(100)),N'') + N';' + N'IsActive: ' + ISNULL(CAST(d.IsActive AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.IsActive AS NVARCHAR(100)),N'')
    FROM inserted i INNER JOIN deleted d ON i.ServiceID = d.ServiceID;
END
GO

CREATE TRIGGER dbo.trg_tblServices_Delete ON dbo.tblServices AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'DELETE', N'tblServices',
           N'ServiceID=' + CAST(d.ServiceID AS NVARCHAR(20)),
           N'Code=' + ISNULL(CAST(d.Code AS NVARCHAR(100)),N'') + N';' + N'Name=' + ISNULL(CAST(d.Name AS NVARCHAR(100)),N'') + N';' + N'UnitPrice=' + ISNULL(CAST(d.UnitPrice AS NVARCHAR(100)),N'')
    FROM deleted d;
END
