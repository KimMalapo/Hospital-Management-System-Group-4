CREATE TABLE dbo.tblAdmissionCharges
(
    ChargeID    INT            IDENTITY(1,1) NOT NULL,
    AdmissionID INT            NOT NULL,
    ServiceID   INT            NOT NULL,
    Description NVARCHAR(200)  NOT NULL,          -- copy of the service name at the time of charging
    Quantity    INT            NOT NULL,
    UnitPrice   DECIMAL(10,2)  NOT NULL,          -- copy of the price at the time of charging
    LineTotal   AS (CONVERT(DECIMAL(12,2), Quantity * UnitPrice)) PERSISTED,
    ChargedAt   DATETIME2(0)   NOT NULL CONSTRAINT DF_tblAdmissionCharges_ChargedAt DEFAULT (SYSUTCDATETIME()),
    ChargedBy   NVARCHAR(200)  NULL,
    IsVoided    BIT            NOT NULL CONSTRAINT DF_tblAdmissionCharges_IsVoided DEFAULT (0),
    VoidReason  NVARCHAR(300)  NULL,
    VoidedBy    NVARCHAR(200)  NULL,
    VoidedAt    DATETIME2(0)   NULL,
    CONSTRAINT PK_tblAdmissionCharges PRIMARY KEY CLUSTERED (ChargeID),
    CONSTRAINT FK_tblAdmissionCharges_Admission FOREIGN KEY (AdmissionID) REFERENCES dbo.tblAdmissions (AdmissionID),
    CONSTRAINT FK_tblAdmissionCharges_Service FOREIGN KEY (ServiceID) REFERENCES dbo.tblServices (ServiceID),
    CONSTRAINT CK_tblAdmissionCharges_Qty CHECK (Quantity > 0),
    CONSTRAINT CK_tblAdmissionCharges_Price CHECK (UnitPrice >= 0),
    CONSTRAINT CK_tblAdmissionCharges_Void CHECK (IsVoided = 0 OR VoidReason IS NOT NULL)
);
GO
CREATE NONCLUSTERED INDEX IX_tblAdmissionCharges_Admission ON dbo.tblAdmissionCharges (AdmissionID, IsVoided);
