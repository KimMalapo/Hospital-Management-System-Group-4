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
END
GO

CREATE PROCEDURE dbo.usp_tblPatients_Delete
    @PatientID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Soft delete
    UPDATE dbo.tblPatients
    SET    IsActive  = 0,
           UpdatedAt = SYSUTCDATETIME()
    WHERE  PatientID = @PatientID;

    SELECT @@ROWCOUNT AS AffectedRows;
END
GO
