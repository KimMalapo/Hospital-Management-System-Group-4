CREATE TRIGGER dbo.trg_tblRoomAssignments_Insert ON dbo.tblRoomAssignments AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'INSERT', N'tblRoomAssignments',
           N'AssignmentID=' + CAST(i.AssignmentID AS NVARCHAR(50)),
           N'PatientID=' + CAST(i.PatientID AS NVARCHAR(50)) + N';RoomNo=' + ISNULL(i.RoomNo,N'') +
           N';AdmitDate=' + CONVERT(NVARCHAR(30), i.AdmitDate, 126)
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblRoomAssignments_Update ON dbo.tblRoomAssignments AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'UPDATE', N'tblRoomAssignments',
           N'AssignmentID=' + CAST(i.AssignmentID AS NVARCHAR(50)),
           N'DischargeDate: ' + ISNULL(CONVERT(NVARCHAR(30), d.DischargeDate, 126), N'NULL') +
           N' -> ' + ISNULL(CONVERT(NVARCHAR(30), i.DischargeDate, 126), N'NULL')
    FROM inserted i INNER JOIN deleted d ON i.AssignmentID = d.AssignmentID;
END
GO

CREATE TRIGGER dbo.trg_tblRoomAssignments_Delete ON dbo.tblRoomAssignments AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'DELETE', N'tblRoomAssignments',
           N'AssignmentID=' + CAST(d.AssignmentID AS NVARCHAR(50)),
           N'PatientID=' + CAST(d.PatientID AS NVARCHAR(50)) + N';RoomNo=' + ISNULL(d.RoomNo,N'')
    FROM deleted d;
END
GO
