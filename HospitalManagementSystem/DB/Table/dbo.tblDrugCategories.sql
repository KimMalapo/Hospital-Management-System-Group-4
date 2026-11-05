CREATE TABLE dbo.tblDrugCategories
(
    DrugCategoryID INT IDENTITY(1,1) NOT NULL,
    Code NVARCHAR(50) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Status NVARCHAR(50) NOT NULL CONSTRAINT DF_tblDrugCategories_Status DEFAULT (N'Active'),
    Notes NVARCHAR(400) NULL,
    IsDeleted BIT NOT NULL CONSTRAINT DF_tblDrugCategories_IsDeleted DEFAULT (0),
    LastUpdated DATETIME2(0) NULL,
    CONSTRAINT PK_tblDrugCategories PRIMARY KEY CLUSTERED (DrugCategoryID)
);
GO

-- Code must be unique among active (non-deleted) rows so soft-deleted codes can be retained
CREATE UNIQUE NONCLUSTERED INDEX UQ_tblDrugCategories_Code_Active
    ON dbo.tblDrugCategories(Code) WHERE IsDeleted = 0;
