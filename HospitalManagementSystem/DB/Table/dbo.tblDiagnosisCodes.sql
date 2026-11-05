CREATE TABLE dbo.tblDiagnosisCodes
(
    DiagnosisCodeID INT IDENTITY(1,1) NOT NULL,
    Code NVARCHAR(50) NOT NULL,
    Description NVARCHAR(400) NOT NULL,
    Status NVARCHAR(50) NOT NULL CONSTRAINT DF_tblDiagnosisCodes_Status DEFAULT (N'Active'),
    Notes NVARCHAR(400) NULL,
    IsDeleted BIT NOT NULL CONSTRAINT DF_tblDiagnosisCodes_IsDeleted DEFAULT (0),
    LastUpdated DATETIME2(0) NULL,
    CONSTRAINT PK_tblDiagnosisCodes PRIMARY KEY CLUSTERED (DiagnosisCodeID)
);
GO

-- Code must be unique among active (non-deleted) rows so soft-deleted codes can be retained
CREATE UNIQUE NONCLUSTERED INDEX UQ_tblDiagnosisCodes_Code_Active
    ON dbo.tblDiagnosisCodes(Code) WHERE IsDeleted = 0;
