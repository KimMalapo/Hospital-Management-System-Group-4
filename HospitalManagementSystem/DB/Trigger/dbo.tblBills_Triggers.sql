CREATE TRIGGER dbo.trg_tblBills_Insert ON dbo.tblBills AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'INSERT', N'tblBills',
           N'BillID=' + CAST(i.BillID AS NVARCHAR(20)),
           N'AdmissionID=' + ISNULL(CAST(i.AdmissionID AS NVARCHAR(100)),N'') + N';' + N'RoomCharges=' + ISNULL(CAST(i.RoomCharges AS NVARCHAR(100)),N'') + N';' + N'ServiceCharges=' + ISNULL(CAST(i.ServiceCharges AS NVARCHAR(100)),N'') + N';' + N'Discount=' + ISNULL(CAST(i.Discount AS NVARCHAR(100)),N'') + N';' + N'Status=' + ISNULL(CAST(i.Status AS NVARCHAR(100)),N'')
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblBills_Update ON dbo.tblBills AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'UPDATE', N'tblBills',
           N'BillID=' + CAST(i.BillID AS NVARCHAR(20)),
           N'RoomCharges: ' + ISNULL(CAST(d.RoomCharges AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.RoomCharges AS NVARCHAR(100)),N'') + N';' + N'ServiceCharges: ' + ISNULL(CAST(d.ServiceCharges AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.ServiceCharges AS NVARCHAR(100)),N'') + N';' + N'Discount: ' + ISNULL(CAST(d.Discount AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Discount AS NVARCHAR(100)),N'') + N';' + N'DiscountReason: ' + ISNULL(CAST(d.DiscountReason AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.DiscountReason AS NVARCHAR(100)),N'') + N';' + N'AmountPaid: ' + ISNULL(CAST(d.AmountPaid AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.AmountPaid AS NVARCHAR(100)),N'') + N';' + N'Status: ' + ISNULL(CAST(d.Status AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Status AS NVARCHAR(100)),N'') + N';' + N'VoidReason: ' + ISNULL(CAST(d.VoidReason AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.VoidReason AS NVARCHAR(100)),N'')
    FROM inserted i INNER JOIN deleted d ON i.BillID = d.BillID;
END
GO

CREATE TRIGGER dbo.trg_tblBills_Delete ON dbo.tblBills AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'DELETE', N'tblBills',
           N'BillID=' + CAST(d.BillID AS NVARCHAR(20)),
           N'AdmissionID=' + ISNULL(CAST(d.AdmissionID AS NVARCHAR(100)),N'') + N';' + N'Status=' + ISNULL(CAST(d.Status AS NVARCHAR(100)),N'')
    FROM deleted d;
END
