-- Stored procedures used by PatientService

CREATE PROCEDURE dbo.usp_tblPatients_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PatientID, FirstName, LastName, DateOfBirth, Gender,
           ContactNumber, Address, Email, IsActive, CreatedAt, UpdatedAt
    FROM   dbo.tblPatients
    WHERE  IsActive = 1
    ORDER  BY LastName, FirstName;
END
GO

CREATE PROCEDURE dbo.usp_tblPatients_GetById
    @PatientID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PatientID, FirstName, LastName, DateOfBirth, Gender,
           ContactNumber, Address, Email, IsActive, CreatedAt, UpdatedAt
    FROM   dbo.tblPatients
    WHERE  PatientID = @PatientID;
END
GO

CREATE PROCEDURE dbo.usp_tblPatients_Search
    @SearchTerm NVARCHAR(200) = N''
AS
BEGIN
    SET NOCOUNT ON;
    SET @SearchTerm = ISNULL(@SearchTerm, N'');
    SELECT PatientID, FirstName, LastName, DateOfBirth, Gender,
           ContactNumber, Address, Email, IsActive, CreatedAt, UpdatedAt
    FROM   dbo.tblPatients
    WHERE  IsActive = 1
      AND  (
            FirstName      LIKE N'%' + @SearchTerm + N'%'
         OR LastName       LIKE N'%' + @SearchTerm + N'%'
         OR ContactNumber  LIKE N'%' + @SearchTerm + N'%'
         OR Email          LIKE N'%' + @SearchTerm + N'%'
         OR CAST(PatientID AS NVARCHAR(20)) = @SearchTerm
          )
    ORDER BY LastName, FirstName;
END
GO

CREATE PROCEDURE dbo.usp_tblPatients_Create
    @FirstName     NVARCHAR(100),
    @LastName      NVARCHAR(100),
    @DateOfBirth   DATE           = NULL,
    @Gender        NVARCHAR(20)   = NULL,
    @ContactNumber NVARCHAR(30)   = NULL,
    @Address       NVARCHAR(400)  = NULL,
    @Email         NVARCHAR(200)  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DECLARE @Existing INT = (SELECT TOP (1) PatientID FROM dbo.tblPatients
                                 WHERE IsActive = 1 AND FirstName = @FirstName AND LastName = @LastName
                                   AND (DateOfBirth = @DateOfBirth OR (DateOfBirth IS NULL AND @DateOfBirth IS NULL)));
        IF @Existing IS NOT NULL
        BEGIN
            DECLARE @m1 NVARCHAR(200) = N'A patient with the same name and date of birth already exists (PatientID ' + CAST(@Existing AS NVARCHAR(20)) + N').';
            RAISERROR(@m1, 16, 1);
        END

        INSERT INTO dbo.tblPatients
            (FirstName, LastName, DateOfBirth, Gender, ContactNumber, Address, Email)
        VALUES
            (@FirstName, @LastName, @DateOfBirth, @Gender, @ContactNumber, @Address, @Email);

        SELECT CAST(SCOPE_IDENTITY() AS INT) AS PatientID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblPatients_Update
    @PatientID     INT,
    @FirstName     NVARCHAR(100),
    @LastName      NVARCHAR(100),
    @DateOfBirth   DATE           = NULL,
    @Gender        NVARCHAR(20)   = NULL,
    @ContactNumber NVARCHAR(30)   = NULL,
    @Address       NVARCHAR(400)  = NULL,
    @Email         NVARCHAR(200)  = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DECLARE @Existing INT = (SELECT TOP (1) PatientID FROM dbo.tblPatients
                                 WHERE IsActive = 1 AND PatientID <> @PatientID
                                   AND FirstName = @FirstName AND LastName = @LastName
                                   AND (DateOfBirth = @DateOfBirth OR (DateOfBirth IS NULL AND @DateOfBirth IS NULL)));
        IF @Existing IS NOT NULL
        BEGIN
            DECLARE @m1 NVARCHAR(200) = N'Another patient with the same name and date of birth already exists (PatientID ' + CAST(@Existing AS NVARCHAR(20)) + N').';
            RAISERROR(@m1, 16, 1);
        END

        UPDATE dbo.tblPatients
        SET    FirstName     = @FirstName,
               LastName      = @LastName,
               DateOfBirth   = @DateOfBirth,
               Gender        = @Gender,
               ContactNumber = @ContactNumber,
               Address       = @Address,
               Email         = @Email,
               UpdatedAt     = SYSUTCDATETIME()
        WHERE  PatientID = @PatientID;

        SELECT @@ROWCOUNT AS AffectedRows;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblPatients_Delete
    @PatientID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- tblAdmissions (DB-01) and tblBills (DB-03) must exist; install those zips first
        IF EXISTS (SELECT 1 FROM dbo.tblAdmissions WHERE PatientID = @PatientID AND Status = N'Admitted')
            RAISERROR(N'This patient is currently admitted. Discharge the patient before deleting the record.', 16, 1);

        IF EXISTS (SELECT 1 FROM dbo.tblBills b JOIN dbo.tblAdmissions a ON a.AdmissionID = b.AdmissionID
                   WHERE a.PatientID = @PatientID AND b.Status IN (N'Open', N'PartiallyPaid'))
            RAISERROR(N'This patient has an unpaid bill. Settle or void the bill before deleting the record.', 16, 1);

        -- Soft delete (history is kept for reports and audit)
        UPDATE dbo.tblPatients
        SET    IsActive  = 0,
               UpdatedAt = SYSUTCDATETIME()
        WHERE  PatientID = @PatientID;

        SELECT @@ROWCOUNT AS AffectedRows;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

GO

-- Deactivated (soft-deleted) patients, newest change first. Optional search by name, phone, email or ID.
CREATE PROCEDURE dbo.usp_tblPatients_GetDeleted
    @SearchTerm NVARCHAR(200) = N''
AS
BEGIN
    SET NOCOUNT ON;
    SET @SearchTerm = ISNULL(@SearchTerm, N'');
    SELECT PatientID, FirstName, LastName, DateOfBirth, Gender,
           ContactNumber, Address, Email, IsActive, CreatedAt, UpdatedAt
    FROM   dbo.tblPatients
    WHERE  IsActive = 0
      AND  (
            FirstName      LIKE N'%' + @SearchTerm + N'%'
         OR LastName       LIKE N'%' + @SearchTerm + N'%'
         OR ContactNumber  LIKE N'%' + @SearchTerm + N'%'
         OR Email          LIKE N'%' + @SearchTerm + N'%'
         OR CAST(PatientID AS NVARCHAR(20)) = @SearchTerm
          )
    ORDER BY UpdatedAt DESC, LastName, FirstName;
END
GO

-- Brings a deactivated patient back. Returns the number of rows changed (0 when the patient is not deactivated).
CREATE PROCEDURE dbo.usp_tblPatients_Restore
    @PatientID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.tblPatients
        SET    IsActive  = 1,
               UpdatedAt = SYSUTCDATETIME()
        WHERE  PatientID = @PatientID
          AND  IsActive  = 0;

        SELECT @@ROWCOUNT AS AffectedRows;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
