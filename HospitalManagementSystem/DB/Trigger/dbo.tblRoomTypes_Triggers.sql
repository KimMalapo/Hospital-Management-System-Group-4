CREATE TRIGGER dbo.trg_tblRoomTypes_Insert
ON dbo.tblRoomTypes
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'INSERT', 'tblRoomTypes',
		   'RoomTypeID=' + CAST(i.RoomTypeID AS NVARCHAR(50)),
		   'Code=' + ISNULL(i.Code,'') + ';Name=' + ISNULL(i.Name,'') + ';Status=' + ISNULL(i.Status,'') + ';IsDeleted=' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
	FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblRoomTypes_Update
ON dbo.tblRoomTypes
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'UPDATE', 'tblRoomTypes',
		   'RoomTypeID=' + CAST(d.RoomTypeID AS NVARCHAR(50)),
		   'Name: ' + ISNULL(d.Name,'') + ' -> ' + ISNULL(i.Name,'') +
		   ';Status: ' + ISNULL(d.Status,'') + ' -> ' + ISNULL(i.Status,'') +
		   ';Notes: ' + ISNULL(d.Notes,'') + ' -> ' + ISNULL(i.Notes,'') +
		   ';IsDeleted: ' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10)) + ' -> ' + CAST(ISNULL(i.IsDeleted,0) AS NVARCHAR(10))
	FROM inserted i
	INNER JOIN deleted d ON i.RoomTypeID = d.RoomTypeID;
END
GO

CREATE TRIGGER dbo.trg_tblRoomTypes_Delete
ON dbo.tblRoomTypes
AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
	SELECT SUSER_SNAME(), 'DELETE', 'tblRoomTypes',
		   'RoomTypeID=' + CAST(d.RoomTypeID AS NVARCHAR(50)),
		   'Code=' + ISNULL(d.Code,'') + ';Name=' + ISNULL(d.Name,'') + ';Status=' + ISNULL(d.Status,'') + ';IsDeleted=' + CAST(ISNULL(d.IsDeleted,0) AS NVARCHAR(10))
	FROM deleted d;
END
GO
