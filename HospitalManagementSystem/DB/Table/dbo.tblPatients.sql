CREATE TABLE dbo.tblPatients
(
    PatientID     INT           IDENTITY(1,1) NOT NULL,
    FirstName     NVARCHAR(100) NOT NULL,
    LastName      NVARCHAR(100) NOT NULL,
    DateOfBirth   DATE          NULL,
    Gender        NVARCHAR(20)  NULL,
    ContactNumber NVARCHAR(30)  NULL,
    Address       NVARCHAR(400) NULL,
    Email         NVARCHAR(200) NULL,
    IsActive      BIT           NOT NULL CONSTRAINT DF_tblPatients_IsActive DEFAULT (1),
    CreatedAt     DATETIME2(0)  NOT NULL CONSTRAINT DF_tblPatients_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt     DATETIME2(0)  NULL,
    CONSTRAINT PK_tblPatients PRIMARY KEY CLUSTERED (PatientID)
);
GO

CREATE NONCLUSTERED INDEX IX_tblPatients_Name
    ON dbo.tblPatients (LastName, FirstName)
    WHERE IsActive = 1;
GO

-- No two active patients with the same name and date of birth (NULL dates count as equal).
-- If this fails on an old database, run DB/Script/CheckDuplicatePatients.sql first and fix the rows it lists.
CREATE UNIQUE NONCLUSTERED INDEX UQ_tblPatients_Identity_Active
    ON dbo.tblPatients (LastName, FirstName, DateOfBirth)
    WHERE IsActive = 1;
