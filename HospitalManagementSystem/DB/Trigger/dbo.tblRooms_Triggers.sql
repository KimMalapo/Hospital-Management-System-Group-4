CREATE TRIGGER dbo.trg_tblRooms_Insert ON dbo.tblRooms AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'INSERT', N'tblRooms',
           N'RoomNo=' + ISNULL(i.RoomNo,N''),
           N'RoomType=' + ISNULL(i.RoomType,N'') + N';Rate=' + CAST(i.Rate AS NVARCHAR(30)) +
           N';Status=' + ISNULL(i.Status,N'')
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblRooms_Update ON dbo.tblRooms AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'UPDATE', N'tblRooms',
           N'RoomNo=' + ISNULL(i.RoomNo,N''),
           N'RoomType: ' + ISNULL(d.RoomType,N'') + N' -> ' + ISNULL(i.RoomType,N'') +
           N';Status: ' + ISNULL(d.Status,N'') + N' -> ' + ISNULL(i.Status,N'') +
           N';Rate: ' + CAST(d.Rate AS NVARCHAR(30)) + N' -> ' + CAST(i.Rate AS NVARCHAR(30))
    FROM inserted i INNER JOIN deleted d ON i.RoomNo = d.RoomNo;
END
GO

CREATE TRIGGER dbo.trg_tblRooms_Delete ON dbo.tblRooms AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT SUSER_SNAME(), N'DELETE', N'tblRooms',
           N'RoomNo=' + ISNULL(d.RoomNo,N''),
           N'RoomType=' + ISNULL(d.RoomType,N'') + N';Status=' + ISNULL(d.Status,N'')
    FROM deleted d;
END
GO
