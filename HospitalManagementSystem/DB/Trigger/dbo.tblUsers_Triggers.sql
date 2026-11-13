CREATE TRIGGER dbo.trg_tblUsers_Insert ON dbo.tblUsers AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'INSERT', N'tblUsers',
           N'UserID=' + CAST(i.UserID AS NVARCHAR(50)),
           N'Email=' + ISNULL(i.Email,N'') + N';Role=' + ISNULL(i.Role,N'')
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblUsers_Update ON dbo.tblUsers AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'UPDATE', N'tblUsers',
           N'UserID=' + CAST(i.UserID AS NVARCHAR(50)),
           N'Role: ' + ISNULL(d.Role,N'') + N' -> ' + ISNULL(i.Role,N'') +
           N';IsActive: ' + CAST(ISNULL(d.IsActive,1) AS NVARCHAR(10)) + N' -> ' + CAST(ISNULL(i.IsActive,1) AS NVARCHAR(10))
    FROM inserted i INNER JOIN deleted d ON i.UserID = d.UserID;
END
GO
