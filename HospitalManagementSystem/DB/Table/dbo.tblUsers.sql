CREATE TABLE dbo.tblUsers
(
    UserID    INT           IDENTITY(1,1) NOT NULL,
    Email     NVARCHAR(200) NOT NULL,
    Password  NVARCHAR(200) NOT NULL,   -- BCrypt hash
    Role      NVARCHAR(50)  NOT NULL CONSTRAINT DF_tblUsers_Role DEFAULT (N'Staff'),
    IsActive  BIT           NOT NULL CONSTRAINT DF_tblUsers_IsActive DEFAULT (1),
    CreatedAt DATETIME2(0)  NOT NULL CONSTRAINT DF_tblUsers_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt DATETIME2(0)  NULL,
    CONSTRAINT PK_tblUsers PRIMARY KEY CLUSTERED (UserID),
    CONSTRAINT UQ_tblUsers_Email UNIQUE (Email),
    CONSTRAINT CK_tblUsers_Role CHECK (Role IN (N'Admin', N'Staff'))
);
