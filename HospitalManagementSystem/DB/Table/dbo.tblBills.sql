CREATE TABLE dbo.tblBills
(
    BillID          INT            IDENTITY(1,1) NOT NULL,
    BillNo          AS (N'BILL-' + RIGHT(N'000000' + CAST(BillID AS NVARCHAR(10)), 6)) PERSISTED,
    AdmissionID     INT            NOT NULL,
    RoomCharges     DECIMAL(12,2)  NOT NULL CONSTRAINT DF_tblBills_RoomCharges DEFAULT (0),
    ServiceCharges  DECIMAL(12,2)  NOT NULL CONSTRAINT DF_tblBills_ServiceCharges DEFAULT (0),
    Discount        DECIMAL(12,2)  NOT NULL CONSTRAINT DF_tblBills_Discount DEFAULT (0),
    DiscountReason  NVARCHAR(300)  NULL,
    TotalAmount     AS (RoomCharges + ServiceCharges - Discount) PERSISTED,
    AmountPaid      DECIMAL(12,2)  NOT NULL CONSTRAINT DF_tblBills_AmountPaid DEFAULT (0),   -- kept in step by the payment procedures
    Balance         AS (RoomCharges + ServiceCharges - Discount - AmountPaid) PERSISTED,
    Status          NVARCHAR(20)   NOT NULL CONSTRAINT DF_tblBills_Status DEFAULT (N'Open'),
    GeneratedAt     DATETIME2(0)   NOT NULL CONSTRAINT DF_tblBills_GeneratedAt DEFAULT (SYSUTCDATETIME()),
    GeneratedBy     NVARCHAR(200)  NULL,
    VoidReason      NVARCHAR(300)  NULL,
    VoidedBy        NVARCHAR(200)  NULL,
    VoidedAt        DATETIME2(0)   NULL,
    CONSTRAINT PK_tblBills PRIMARY KEY CLUSTERED (BillID),
    CONSTRAINT FK_tblBills_Admission FOREIGN KEY (AdmissionID) REFERENCES dbo.tblAdmissions (AdmissionID),
    CONSTRAINT CK_tblBills_Amounts CHECK (RoomCharges >= 0 AND ServiceCharges >= 0 AND Discount >= 0 AND AmountPaid >= 0),
    CONSTRAINT CK_tblBills_DiscountMax CHECK (Discount <= RoomCharges + ServiceCharges),
    CONSTRAINT CK_tblBills_NoOverpay CHECK (AmountPaid <= RoomCharges + ServiceCharges - Discount),
    CONSTRAINT CK_tblBills_Status CHECK (Status IN (N'Open', N'PartiallyPaid', N'Paid', N'Voided'))
);
GO

-- Only one live (not voided) bill per admission
CREATE UNIQUE NONCLUSTERED INDEX UQ_tblBills_OneLivePerAdmission
    ON dbo.tblBills (AdmissionID) WHERE Status <> N'Voided';
GO
CREATE NONCLUSTERED INDEX IX_tblBills_Status ON dbo.tblBills (Status);
GO

CREATE TABLE dbo.tblBillItems
(
    BillItemID  INT            IDENTITY(1,1) NOT NULL,
    BillID      INT            NOT NULL,
    ItemType    NVARCHAR(20)   NOT NULL,          -- Room or Service
    Description NVARCHAR(300)  NOT NULL,
    Quantity    INT            NOT NULL,
    UnitPrice   DECIMAL(10,2)  NOT NULL,
    Amount      DECIMAL(12,2)  NOT NULL,
    CONSTRAINT PK_tblBillItems PRIMARY KEY CLUSTERED (BillItemID),
    CONSTRAINT FK_tblBillItems_Bill FOREIGN KEY (BillID) REFERENCES dbo.tblBills (BillID),
    CONSTRAINT CK_tblBillItems_Type CHECK (ItemType IN (N'Room', N'Service')),
    CONSTRAINT CK_tblBillItems_Qty CHECK (Quantity > 0)
);
GO
CREATE NONCLUSTERED INDEX IX_tblBillItems_Bill ON dbo.tblBillItems (BillID);
