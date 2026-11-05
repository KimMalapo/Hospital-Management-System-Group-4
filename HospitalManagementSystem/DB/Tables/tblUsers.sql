-- ============================================================
-- Issue #50: Correct tblUsers schema
-- Branch: feature-50-correct-tblusers-schema
-- ============================================================
-- HOW TO RUN:
--   1. Open this file in SSMS / Azure Data Studio / VS Code
--   2. Connect to your SQL Server or (localdb)\MSSQLLocalDB
--   3. Press F5 (or Execute)
-- ============================================================

SET NOCOUNT ON;
GO

-- Use HospitalDB if it exists, otherwise stay on current DB
IF DB_ID(N'HospitalDB') IS NOT NULL
    USE HospitalDB;
GO

-- ============================================================
-- Branch: feature/50-correct-tblusers-schema
-- Issue  #50 : Correct tblUsers schema
-- ============================================================
-- Aligns tblUsers with UserService / AuthService expectations:
--   - Proper column lengths
--   - Role column (Admin / Staff)
--   - Unique Email
--   - Soft-delete support + audit columns
-- ============================================================

SET NOCOUNT ON;
GO

-- 1. Create table if it does not exist (clean install)
IF OBJECT_ID(N'dbo.tblUsers', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.tblUsers
    (
        UserID       INT            IDENTITY(1,1) NOT NULL,
        Email        NVARCHAR(200)  NOT NULL,
        Password     NVARCHAR(200)  NOT NULL,   -- BCrypt hash
        Role         NVARCHAR(50)   NOT NULL CONSTRAINT DF_tblUsers_Role DEFAULT (N'Staff'),
        IsActive     BIT            NOT NULL CONSTRAINT DF_tblUsers_IsActive DEFAULT (1),
        CreatedAt    DATETIME2(0)   NOT NULL CONSTRAINT DF_tblUsers_CreatedAt DEFAULT (SYSUTCDATETIME()),
        UpdatedAt    DATETIME2(0)   NULL,
        CONSTRAINT PK_tblUsers PRIMARY KEY CLUSTERED (UserID),
        CONSTRAINT UQ_tblUsers_Email UNIQUE (Email),
        CONSTRAINT CK_tblUsers_Role CHECK (Role IN (N'Admin', N'Staff'))
    );
END
GO

-- 2. Upgrade path – add missing columns / constraints on existing table
IF OBJECT_ID(N'dbo.tblUsers', N'U') IS NOT NULL
BEGIN
    -- Email length
    IF EXISTS (
        SELECT 1 FROM sys.columns
        WHERE object_id = OBJECT_ID(N'dbo.tblUsers') AND name = N'Email'
          AND max_length < 400)   -- NVARCHAR(200) = 400 bytes
    BEGIN
        ALTER TABLE dbo.tblUsers ALTER COLUMN Email NVARCHAR(200) NOT NULL;
    END

    -- Password length (BCrypt needs ~60 chars)
    IF EXISTS (
        SELECT 1 FROM sys.columns
        WHERE object_id = OBJECT_ID(N'dbo.tblUsers') AND name = N'Password'
          AND max_length < 400)
    BEGIN
        ALTER TABLE dbo.tblUsers ALTER COLUMN Password NVARCHAR(200) NOT NULL;
    END

    -- Role
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.tblUsers') AND name = N'Role')
    BEGIN
        ALTER TABLE dbo.tblUsers ADD Role NVARCHAR(50) NOT NULL
            CONSTRAINT DF_tblUsers_Role DEFAULT (N'Staff');
    END

    -- IsActive
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.tblUsers') AND name = N'IsActive')
    BEGIN
        ALTER TABLE dbo.tblUsers ADD IsActive BIT NOT NULL
            CONSTRAINT DF_tblUsers_IsActive DEFAULT (1);
    END

    -- CreatedAt
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.tblUsers') AND name = N'CreatedAt')
    BEGIN
        ALTER TABLE dbo.tblUsers ADD CreatedAt DATETIME2(0) NOT NULL
            CONSTRAINT DF_tblUsers_CreatedAt DEFAULT (SYSUTCDATETIME());
    END

    -- UpdatedAt
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.tblUsers') AND name = N'UpdatedAt')
    BEGIN
        ALTER TABLE dbo.tblUsers ADD UpdatedAt DATETIME2(0) NULL;
    END

    -- Unique Email
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UQ_tblUsers_Email' AND object_id = OBJECT_ID(N'dbo.tblUsers'))
    BEGIN
        -- Remove any existing non-unique index on Email first if needed
        CREATE UNIQUE NONCLUSTERED INDEX UQ_tblUsers_Email ON dbo.tblUsers(Email);
    END

    -- Role check constraint
    IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = N'CK_tblUsers_Role')
    BEGIN
        ALTER TABLE dbo.tblUsers WITH NOCHECK
            ADD CONSTRAINT CK_tblUsers_Role CHECK (Role IN (N'Admin', N'Staff'));
    END
END
GO

-- 3. Stored procedures expected by UserService

CREATE OR ALTER PROCEDURE dbo.usp_tblUsers_GetByEmail
    @Email NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT UserID, Email, Password, Role, IsActive, CreatedAt, UpdatedAt
    FROM   dbo.tblUsers
    WHERE  Email = @Email
      AND  IsActive = 1;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_tblUsers_Create
    @Email    NVARCHAR(200),
    @Password NVARCHAR(200),
    @Role     NVARCHAR(50) = N'Staff'
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.tblUsers (Email, Password, Role)
        VALUES (@Email, @Password, @Role);

        SELECT CAST(SCOPE_IDENTITY() AS INT) AS UserID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

PRINT 'tblUsers schema corrected successfully.';
GO

PRINT 'Issue #50 (Correct tblUsers schema) completed successfully.';
GO
