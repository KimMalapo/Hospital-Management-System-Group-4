-- Stored procedures expected by UserService

CREATE PROCEDURE dbo.usp_tblUsers_GetByEmail
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

CREATE PROCEDURE dbo.usp_tblUsers_Create
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
