CREATE TRIGGER dbo.trg_tblDrugCategories_Insert ON dbo.tblDrugCategories AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'INSERT', N'tblDrugCategories',
           N'DrugCategoryID=' + CAST(i.DrugCategoryID AS NVARCHAR(50)),
           N'Code=' + ISNULL(i.Code,N'') + N';Name=' + ISNULL(i.Name,N'') + N';Status=' + ISNULL(i.Status,N'')
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblDrugCategories_Update ON dbo.tblDrugCategories AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'UPDATE', N'tblDrugCategories',
           N'DrugCategoryID=' + CAST(i.DrugCategoryID AS NVARCHAR(50)),
           N'Name: ' + ISNULL(d.Name,N'') + N' -> ' + ISNULL(i.Name,N'') +
           N';Status: ' + ISNULL(d.Status,N'') + N' -> ' + ISNULL(i.Status,N'') +
           N';IsDeleted: ' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10)) + N' -> ' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
    FROM inserted i INNER JOIN deleted d ON i.DrugCategoryID = d.DrugCategoryID;
END
GO

CREATE TRIGGER dbo.trg_tblDrugCategories_Delete ON dbo.tblDrugCategories AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'DELETE', N'tblDrugCategories',
           N'DrugCategoryID=' + CAST(d.DrugCategoryID AS NVARCHAR(50)),
           N'Code=' + ISNULL(d.Code,N'') + N';Name=' + ISNULL(d.Name,N'')
    FROM deleted d;
END
GO
