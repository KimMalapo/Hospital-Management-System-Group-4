CREATE TABLE dbo.tblServices
(
    ServiceID        INT            IDENTITY(1,1) NOT NULL,
    Code             NVARCHAR(50)   NOT NULL,
    Name             NVARCHAR(200)  NOT NULL,
    Category         NVARCHAR(50)   NOT NULL,
    DrugCategoryCode NVARCHAR(50)   NULL,          -- only for Medication; logical link to tblDrugCategories.Code
    UnitPrice        DECIMAL(10,2)  NOT NULL,
    IsActive         BIT            NOT NULL CONSTRAINT DF_tblServices_IsActive DEFAULT (1),
    CreatedAt        DATETIME2(0)   NOT NULL CONSTRAINT DF_tblServices_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt        DATETIME2(0)   NULL,
    CONSTRAINT PK_tblServices PRIMARY KEY CLUSTERED (ServiceID),
    CONSTRAINT CK_tblServices_Price CHECK (UnitPrice >= 0),
    CONSTRAINT CK_tblServices_Category CHECK (Category IN
        (N'Consultation', N'Laboratory', N'Imaging', N'Procedure', N'Medication', N'Supply', N'Other'))
);
GO

-- Codes are unique among active services so a retired code can be reused
CREATE UNIQUE NONCLUSTERED INDEX UQ_tblServices_Code_Active
    ON dbo.tblServices (Code) WHERE IsActive = 1;
GO
CREATE NONCLUSTERED INDEX IX_tblServices_Name ON dbo.tblServices (Name);
