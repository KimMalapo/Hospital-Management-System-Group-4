CREATE TRIGGER dbo.trg_tblPayments_Insert ON dbo.tblPayments AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'INSERT', N'tblPayments',
           N'PaymentID=' + CAST(i.PaymentID AS NVARCHAR(20)),
           N'BillID=' + ISNULL(CAST(i.BillID AS NVARCHAR(100)),N'') + N';' + N'Amount=' + ISNULL(CAST(i.Amount AS NVARCHAR(100)),N'') + N';' + N'Method=' + ISNULL(CAST(i.Method AS NVARCHAR(100)),N'') + N';' + N'ReferenceNo=' + ISNULL(CAST(i.ReferenceNo AS NVARCHAR(100)),N'') + N';' + N'Status=' + ISNULL(CAST(i.Status AS NVARCHAR(100)),N'')
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblPayments_Update ON dbo.tblPayments AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'UPDATE', N'tblPayments',
           N'PaymentID=' + CAST(i.PaymentID AS NVARCHAR(20)),
           N'Amount: ' + ISNULL(CAST(d.Amount AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Amount AS NVARCHAR(100)),N'') + N';' + N'Method: ' + ISNULL(CAST(d.Method AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Method AS NVARCHAR(100)),N'') + N';' + N'ReferenceNo: ' + ISNULL(CAST(d.ReferenceNo AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.ReferenceNo AS NVARCHAR(100)),N'') + N';' + N'Status: ' + ISNULL(CAST(d.Status AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Status AS NVARCHAR(100)),N'') + N';' + N'VoidReason: ' + ISNULL(CAST(d.VoidReason AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.VoidReason AS NVARCHAR(100)),N'')
    FROM inserted i INNER JOIN deleted d ON i.PaymentID = d.PaymentID;
END
GO

CREATE TRIGGER dbo.trg_tblPayments_Delete ON dbo.tblPayments AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'DELETE', N'tblPayments',
           N'PaymentID=' + CAST(d.PaymentID AS NVARCHAR(20)),
           N'BillID=' + ISNULL(CAST(d.BillID AS NVARCHAR(100)),N'') + N';' + N'Amount=' + ISNULL(CAST(d.Amount AS NVARCHAR(100)),N'') + N';' + N'Status=' + ISNULL(CAST(d.Status AS NVARCHAR(100)),N'')
    FROM deleted d;
END
