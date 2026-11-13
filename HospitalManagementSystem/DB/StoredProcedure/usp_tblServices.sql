CREATE PROCEDURE dbo.usp_tblServices_GetAll
    @ActiveOnly BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ServiceID, Code, Name, Category, DrugCategoryCode, UnitPrice, IsActive
    FROM   dbo.tblServices
    WHERE  (@ActiveOnly = 0 OR IsActive = 1)
    ORDER BY Category, Name;
END
GO

CREATE PROCEDURE dbo.usp_tblServices_Search
    @SearchTerm NVARCHAR(200) = NULL,
    @ActiveOnly BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @t NVARCHAR(202) = N'%' + ISNULL(LTRIM(RTRIM(@SearchTerm)), N'') + N'%';
    SELECT ServiceID, Code, Name, Category, DrugCategoryCode, UnitPrice, IsActive
    FROM   dbo.tblServices
    WHERE  (@ActiveOnly = 0 OR IsActive = 1)
      AND  (Code LIKE @t OR Name LIKE @t OR Category LIKE @t)
    ORDER BY Category, Name;
END
GO

CREATE PROCEDURE dbo.usp_tblServices_GetById
    @ServiceID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ServiceID, Code, Name, Category, DrugCategoryCode, UnitPrice, IsActive
    FROM   dbo.tblServices WHERE ServiceID = @ServiceID;
END
GO

CREATE PROCEDURE dbo.usp_tblServices_Create
    @Code             NVARCHAR(50),
    @Name             NVARCHAR(200),
    @Category         NVARCHAR(50),
    @DrugCategoryCode NVARCHAR(50) = NULL,
    @UnitPrice        DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF @UnitPrice < 0 RAISERROR(N'The price cannot be negative.', 16, 1);
        IF EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = @Code AND IsActive = 1)
            RAISERROR(N'A service with this code already exists.', 16, 1);
        IF @Category = N'Medication' AND @DrugCategoryCode IS NOT NULL AND NOT EXISTS
           (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = @DrugCategoryCode AND IsDeleted = 0)
            RAISERROR(N'The drug category was not found.', 16, 1);

        INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
        VALUES (@Code, @Name, @Category,
                CASE WHEN @Category = N'Medication' THEN @DrugCategoryCode END, @UnitPrice);

        SELECT CAST(SCOPE_IDENTITY() AS INT) AS ServiceID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblServices_Update
    @ServiceID        INT,
    @Code             NVARCHAR(50),
    @Name             NVARCHAR(200),
    @Category         NVARCHAR(50),
    @DrugCategoryCode NVARCHAR(50) = NULL,
    @UnitPrice        DECIMAL(10,2),
    @IsActive         BIT = 1
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE ServiceID = @ServiceID)
            RAISERROR(N'The service was not found.', 16, 1);
        IF @UnitPrice < 0 RAISERROR(N'The price cannot be negative.', 16, 1);
        IF @IsActive = 1 AND EXISTS
           (SELECT 1 FROM dbo.tblServices WHERE Code = @Code AND IsActive = 1 AND ServiceID <> @ServiceID)
            RAISERROR(N'A service with this code already exists.', 16, 1);

        -- Old charges keep their own copy of name and price, so this edit does not change past bills
        UPDATE dbo.tblServices
        SET    Code = @Code, Name = @Name, Category = @Category,
               DrugCategoryCode = CASE WHEN @Category = N'Medication' THEN @DrugCategoryCode END,
               UnitPrice = @UnitPrice, IsActive = @IsActive, UpdatedAt = SYSUTCDATETIME()
        WHERE  ServiceID = @ServiceID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- Services are never hard-deleted: charges refer to them. Deactivate hides them from new charges.
CREATE PROCEDURE dbo.usp_tblServices_Deactivate
    @ServiceID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE ServiceID = @ServiceID)
            RAISERROR(N'The service was not found.', 16, 1);
        UPDATE dbo.tblServices SET IsActive = 0, UpdatedAt = SYSUTCDATETIME() WHERE ServiceID = @ServiceID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
