CREATE TRIGGER dbo.trg_tblDepartmentTypes_Insert ON dbo.tblDepartmentTypes AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'INSERT', N'tblDepartmentTypes',
           N'DepartmentTypeID=' + CAST(i.DepartmentTypeID AS NVARCHAR(50)),
           N'Code=' + ISNULL(i.Code,N'') + N';Name=' + ISNULL(i.Name,N'') + N';Status=' + ISNULL(i.Status,N'')
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblDepartmentTypes_Update ON dbo.tblDepartmentTypes AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'UPDATE', N'tblDepartmentTypes',
           N'DepartmentTypeID=' + CAST(i.DepartmentTypeID AS NVARCHAR(50)),
           N'Name: ' + ISNULL(d.Name,N'') + N' -> ' + ISNULL(i.Name,N'') +
           N';Status: ' + ISNULL(d.Status,N'') + N' -> ' + ISNULL(i.Status,N'') +
           N';IsDeleted: ' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10)) + N' -> ' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
    FROM inserted i INNER JOIN deleted d ON i.DepartmentTypeID = d.DepartmentTypeID;
END
GO

CREATE TRIGGER dbo.trg_tblDepartmentTypes_Delete ON dbo.tblDepartmentTypes AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'DELETE', N'tblDepartmentTypes',
           N'DepartmentTypeID=' + CAST(d.DepartmentTypeID AS NVARCHAR(50)),
           N'Code=' + ISNULL(d.Code,N'') + N';Name=' + ISNULL(d.Name,N'')
    FROM deleted d;
END
GO
