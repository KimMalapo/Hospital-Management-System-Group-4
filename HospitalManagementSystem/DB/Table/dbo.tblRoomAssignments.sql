CREATE TABLE dbo.tblRoomAssignments
(
    AssignmentID  INT           IDENTITY(1,1) NOT NULL,
    PatientID     INT           NOT NULL,
    RoomNo        NVARCHAR(20)  NOT NULL,
    AdmitDate     DATETIME2(0)  NOT NULL CONSTRAINT DF_tblRoomAssignments_AdmitDate DEFAULT (SYSUTCDATETIME()),
    DischargeDate DATETIME2(0)  NULL,          -- NULL = still open / occupied
    Notes         NVARCHAR(500) NULL,
    CreatedAt     DATETIME2(0)  NOT NULL CONSTRAINT DF_tblRoomAssignments_CreatedAt DEFAULT (SYSUTCDATETIME()),
    CreatedBy     NVARCHAR(200) NULL,
    CONSTRAINT PK_tblRoomAssignments PRIMARY KEY CLUSTERED (AssignmentID),
    CONSTRAINT FK_tblRoomAssignments_Patient FOREIGN KEY (PatientID) REFERENCES dbo.tblPatients (PatientID),
    CONSTRAINT FK_tblRoomAssignments_Room FOREIGN KEY (RoomNo) REFERENCES dbo.tblRooms (RoomNo),
    -- Business rule: discharge must not be before admit
    CONSTRAINT CK_tblRoomAssignments_Dates CHECK (DischargeDate IS NULL OR DischargeDate >= AdmitDate)
);
GO

-- Only one open assignment per room
CREATE UNIQUE NONCLUSTERED INDEX UQ_tblRoomAssignments_OpenRoom
    ON dbo.tblRoomAssignments (RoomNo)
    WHERE DischargeDate IS NULL;
GO

CREATE NONCLUSTERED INDEX IX_tblRoomAssignments_Patient
    ON dbo.tblRoomAssignments (PatientID, AdmitDate DESC);
GO

CREATE NONCLUSTERED INDEX IX_tblRoomAssignments_Room
    ON dbo.tblRoomAssignments (RoomNo, AdmitDate DESC);
