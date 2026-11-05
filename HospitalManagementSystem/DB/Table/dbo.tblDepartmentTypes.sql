CREATE TABLE dbo.tblDepartmentTypes
(
    DepartmentTypeID INT IDENTITY(1,1) NOT NULL,
    Code NVARCHAR(50) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Status NVARCHAR(50) NOT NULL CONSTRAINT DF_tblDepartmentTypes_Status DEFAULT (N'Active'),
    Notes NVARCHAR(400) NULL,
    IsDeleted BIT NOT NULL CONSTRAINT DF_tblDepartmentTypes_IsDeleted DEFAULT (0),
    LastUpdated DATETIME2(0) NULL,
    CONSTRAINT PK_tblDepartmentTypes PRIMARY KEY CLUSTERED (DepartmentTypeID)
);
GO

-- Code must be unique among active (non-deleted) rows so soft-deleted codes can be retained
CREATE UNIQUE NONCLUSTERED INDEX UQ_tblDepartmentTypes_Code_Active
    ON dbo.tblDepartmentTypes(Code) WHERE IsDeleted = 0;
