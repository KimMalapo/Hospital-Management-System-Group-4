CREATE TRIGGER dbo.trg_tblAdmissions_Insert ON dbo.tblAdmissions AFTER INSERT AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'INSERT', N'tblAdmissions',
           N'AdmissionID=' + CAST(i.AdmissionID AS NVARCHAR(20)),
           N'PatientID=' + ISNULL(CAST(i.PatientID AS NVARCHAR(100)),N'') + N';' + N'AssignmentID=' + ISNULL(CAST(i.AssignmentID AS NVARCHAR(100)),N'') + N';' + N'DepartmentCode=' + ISNULL(CAST(i.DepartmentCode AS NVARCHAR(100)),N'') + N';' + N'DiagnosisCode=' + ISNULL(CAST(i.DiagnosisCode AS NVARCHAR(100)),N'') + N';' + N'Status=' + ISNULL(CAST(i.Status AS NVARCHAR(100)),N'')
    FROM inserted i;
END
GO

CREATE TRIGGER dbo.trg_tblAdmissions_Update ON dbo.tblAdmissions AFTER UPDATE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'UPDATE', N'tblAdmissions',
           N'AdmissionID=' + CAST(i.AdmissionID AS NVARCHAR(20)),
           N'Status: ' + ISNULL(CAST(d.Status AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.Status AS NVARCHAR(100)),N'') + N';' + N'DepartmentCode: ' + ISNULL(CAST(d.DepartmentCode AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.DepartmentCode AS NVARCHAR(100)),N'') + N';' + N'DiagnosisCode: ' + ISNULL(CAST(d.DiagnosisCode AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.DiagnosisCode AS NVARCHAR(100)),N'') + N';' + N'AttendingPhysician: ' + ISNULL(CAST(d.AttendingPhysician AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.AttendingPhysician AS NVARCHAR(100)),N'') + N';' + N'DischargeSummary: ' + ISNULL(CAST(d.DischargeSummary AS NVARCHAR(100)),N'') + N' -> ' + ISNULL(CAST(i.DischargeSummary AS NVARCHAR(100)),N'')
    FROM inserted i INNER JOIN deleted d ON i.AdmissionID = d.AdmissionID;
END
GO

CREATE TRIGGER dbo.trg_tblAdmissions_Delete ON dbo.tblAdmissions AFTER DELETE AS
BEGIN SET NOCOUNT ON;
    INSERT INTO dbo.tblAuditLog (Username, Action, TableName, KeyValues, ChangedValues)
    SELECT COALESCE(CAST(SESSION_CONTEXT(N'AppUser') AS NVARCHAR(200)), SUSER_SNAME()), N'DELETE', N'tblAdmissions',
           N'AdmissionID=' + CAST(d.AdmissionID AS NVARCHAR(20)),
           N'PatientID=' + ISNULL(CAST(d.PatientID AS NVARCHAR(100)),N'') + N';' + N'Status=' + ISNULL(CAST(d.Status AS NVARCHAR(100)),N'')
    FROM deleted d;
END
