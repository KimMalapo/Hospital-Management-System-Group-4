CREATE TRIGGER dbo.trg_tblAdmissionCharges_Insert ON dbo.tblAdmissionCharges AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'INSERT', N'tblAdmissionCharges',
           N'ChargeID=' + CAST(i.ChargeID AS NVARCHAR(20)),
           N'AdmissionID=' + ISNULL(CAST(i.AdmissionID AS NVARCHAR(100)),N'') + N';' + N'ServiceID=' + ISNULL(CAST(i.ServiceID AS NVARCHAR(100)),N'') + N';' + N'Quantity=' + ISNULL(CAST(i.Quantity AS NVARCHAR(100)),N'') + N';' + N'UnitPrice=' + ISNULL(CAST(i.UnitPrice AS NVARCHAR(100)),N'')
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblAdmissionCharges_Update ON dbo.tblAdmissionCharges AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'UPDATE', N'tblAdmissionCharges',
           N'ChargeID=' + CAST(i.ChargeID AS NVARCHAR(20)),
           N'Quantity: ' + ISNULL(CAST(d.Quantity AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Quantity AS NVARCHAR(100)),N'') + N';' + N'UnitPrice: ' + ISNULL(CAST(d.UnitPrice AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.UnitPrice AS NVARCHAR(100)),N'') + N';' + N'IsVoided: ' + ISNULL(CAST(d.IsVoided AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.IsVoided AS NVARCHAR(100)),N'') + N';' + N'VoidReason: ' + ISNULL(CAST(d.VoidReason AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.VoidReason AS NVARCHAR(100)),N'')
    FROM inserted i INNER JOIN deleted d ON i.ChargeID = d.ChargeID;
END
GO

CREATE TRIGGER dbo.trg_tblAdmissionCharges_Delete ON dbo.tblAdmissionCharges AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'DELETE', N'tblAdmissionCharges',
           N'ChargeID=' + CAST(d.ChargeID AS NVARCHAR(20)),
           N'AdmissionID=' + ISNULL(CAST(d.AdmissionID AS NVARCHAR(100)),N'') + N';' + N'ServiceID=' + ISNULL(CAST(d.ServiceID AS NVARCHAR(100)),N'') + N';' + N'Quantity=' + ISNULL(CAST(d.Quantity AS NVARCHAR(100)),N'')
    FROM deleted d;
END
