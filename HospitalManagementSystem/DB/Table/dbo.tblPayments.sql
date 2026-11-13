CREATE TABLE dbo.tblPayments
(
    PaymentID   INT            IDENTITY(1,1) NOT NULL,
    ReceiptNo   AS (N'RCPT-' + RIGHT(N'000000' + CAST(PaymentID AS NVARCHAR(10)), 6)) PERSISTED,
    BillID      INT            NOT NULL,
    PaymentDate DATETIME2(0)   NOT NULL CONSTRAINT DF_tblPayments_PaymentDate DEFAULT (SYSUTCDATETIME()),
    Amount      DECIMAL(12,2)  NOT NULL,
    Method      NVARCHAR(30)   NOT NULL,
    ReferenceNo NVARCHAR(100)  NULL,
    ReceivedBy  NVARCHAR(200)  NULL,
    Status      NVARCHAR(20)   NOT NULL CONSTRAINT DF_tblPayments_Status DEFAULT (N'Posted'),
    VoidReason  NVARCHAR(300)  NULL,
    VoidedBy    NVARCHAR(200)  NULL,
    VoidedAt    DATETIME2(0)   NULL,
    CONSTRAINT PK_tblPayments PRIMARY KEY CLUSTERED (PaymentID),
    CONSTRAINT FK_tblPayments_Bill FOREIGN KEY (BillID) REFERENCES dbo.tblBills (BillID),
    CONSTRAINT CK_tblPayments_Amount CHECK (Amount > 0),
    CONSTRAINT CK_tblPayments_Method CHECK (Method IN (N'Cash', N'Card', N'GCash', N'Bank Transfer', N'Insurance')),
    CONSTRAINT CK_tblPayments_Status CHECK (Status IN (N'Posted', N'Voided')),
    CONSTRAINT CK_tblPayments_Reference CHECK (Method = N'Cash' OR (ReferenceNo IS NOT NULL AND LEN(LTRIM(RTRIM(ReferenceNo))) > 0)),
    CONSTRAINT CK_tblPayments_Void CHECK (Status <> N'Voided' OR VoidReason IS NOT NULL)
);
GO
CREATE NONCLUSTERED INDEX IX_tblPayments_Bill ON dbo.tblPayments (BillID, Status);
GO
CREATE NONCLUSTERED INDEX IX_tblPayments_Date ON dbo.tblPayments (PaymentDate);
