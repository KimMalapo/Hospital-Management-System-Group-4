CREATE TABLE dbo.tblAdmissions
(
    AdmissionID        INT           IDENTITY(1,1) NOT NULL,
    AdmissionNo        AS (N'ADM-' + RIGHT(N'000000' + CAST(AdmissionID AS NVARCHAR(10)), 6)) PERSISTED,
    PatientID          INT           NOT NULL,
    AssignmentID       INT           NULL,          -- the room assignment created when the patient was admitted
    DepartmentCode     NVARCHAR(50)  NULL,          -- logical link to tblDepartmentTypes.Code
    DiagnosisCode      NVARCHAR(50)  NULL,          -- logical link to tblDiagnosisCodes.Code
    AttendingPhysician NVARCHAR(200) NULL,
    Reason             NVARCHAR(500) NULL,
    Status             NVARCHAR(20)  NOT NULL CONSTRAINT DF_tblAdmissions_Status DEFAULT (N'Admitted'),
    AdmitDate          DATETIME2(0)  NOT NULL CONSTRAINT DF_tblAdmissions_AdmitDate DEFAULT (SYSUTCDATETIME()),
    DischargeDate      DATETIME2(0)  NULL,
    DischargeSummary   NVARCHAR(1000) NULL,
    CancelledBy        NVARCHAR(200) NULL,
    CreatedBy          NVARCHAR(200) NULL,
    CreatedAt          DATETIME2(0)  NOT NULL CONSTRAINT DF_tblAdmissions_CreatedAt DEFAULT (SYSUTCDATETIME()),
    UpdatedAt          DATETIME2(0)  NULL,
    CONSTRAINT PK_tblAdmissions PRIMARY KEY CLUSTERED (AdmissionID),
    CONSTRAINT FK_tblAdmissions_Patient FOREIGN KEY (PatientID) REFERENCES dbo.tblPatients (PatientID),
    CONSTRAINT FK_tblAdmissions_Assignment FOREIGN KEY (AssignmentID) REFERENCES dbo.tblRoomAssignments (AssignmentID),
    CONSTRAINT CK_tblAdmissions_Status CHECK (Status IN (N'Admitted', N'Discharged', N'Cancelled')),
    CONSTRAINT CK_tblAdmissions_Dates CHECK (DischargeDate IS NULL OR DischargeDate >= AdmitDate)
);
GO

-- A patient can have only one active admission at a time
CREATE UNIQUE NONCLUSTERED INDEX UQ_tblAdmissions_OneActivePerPatient
    ON dbo.tblAdmissions (PatientID) WHERE Status = N'Admitted';
GO

CREATE NONCLUSTERED INDEX IX_tblAdmissions_Patient ON dbo.tblAdmissions (PatientID, AdmitDate DESC);
GO
CREATE NONCLUSTERED INDEX IX_tblAdmissions_Status ON dbo.tblAdmissions (Status, AdmitDate DESC);
