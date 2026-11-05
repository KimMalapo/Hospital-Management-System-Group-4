CREATE TABLE dbo.tblAuditLog
(
    AuditLogID    INT IDENTITY(1,1) NOT NULL,
    Username      NVARCHAR(200)  NULL,
    Action        NVARCHAR(50)   NOT NULL,
    TableName     NVARCHAR(200)  NOT NULL,
    KeyValues     NVARCHAR(1000) NULL,
    ChangedValues NVARCHAR(MAX)  NULL,
    CreatedAt     DATETIME2      NOT NULL CONSTRAINT DF_tblAuditLog_CreatedAt DEFAULT SYSUTCDATETIME(),
    CONSTRAINT PK_tblAuditLog PRIMARY KEY CLUSTERED (AuditLogID)
);
GO

CREATE NONCLUSTERED INDEX IX_tblAuditLog_Table_Created
    ON dbo.tblAuditLog (TableName, CreatedAt DESC);
