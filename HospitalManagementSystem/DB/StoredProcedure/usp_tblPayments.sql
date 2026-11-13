-- Payment procedures (DB-04). Posting and voiding keep tblBills.AmountPaid and Status in step, inside one transaction.

CREATE PROCEDURE dbo.usp_tblPayments_Post
    @BillID      INT,
    @Amount      DECIMAL(12,2),
    @Method      NVARCHAR(30),
    @ReferenceNo NVARCHAR(100) = NULL,
    @ReceivedBy  NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Status NVARCHAR(20), @Balance DECIMAL(12,2), @Total DECIMAL(12,2), @Paid DECIMAL(12,2);
        SELECT @Status = Status, @Balance = Balance, @Total = TotalAmount, @Paid = AmountPaid
        FROM   dbo.tblBills WITH (UPDLOCK, HOLDLOCK) WHERE BillID = @BillID;

        IF @Status IS NULL RAISERROR(N'The bill was not found.', 16, 1);
        IF @Status = N'Voided' RAISERROR(N'The bill was voided. No payment can be posted.', 16, 1);
        IF @Status = N'Paid' RAISERROR(N'This bill is already fully paid.', 16, 1);
        IF @Amount IS NULL OR @Amount <= 0 RAISERROR(N'The payment amount must be more than zero.', 16, 1);
        IF @Amount > @Balance RAISERROR(N'The payment is more than the remaining balance.', 16, 1);
        IF @Method <> N'Cash' AND (@ReferenceNo IS NULL OR LTRIM(RTRIM(@ReferenceNo)) = N'')
            RAISERROR(N'A reference number is required for this payment method.', 16, 1);

        INSERT INTO dbo.tblPayments (BillID, Amount, Method, ReferenceNo, ReceivedBy)
        VALUES (@BillID, @Amount, @Method, NULLIF(LTRIM(RTRIM(@ReferenceNo)), N''), @ReceivedBy);
        DECLARE @PaymentID INT = CAST(SCOPE_IDENTITY() AS INT);

        UPDATE dbo.tblBills
        SET    AmountPaid = @Paid + @Amount,
               Status = CASE WHEN @Paid + @Amount >= @Total THEN N'Paid' ELSE N'PartiallyPaid' END
        WHERE  BillID = @BillID;

        COMMIT TRANSACTION;

        SELECT PaymentID, ReceiptNo, BillID, PaymentDate, Amount, Method, ReferenceNo, ReceivedBy, Status, VoidReason
        FROM   dbo.tblPayments WHERE PaymentID = @PaymentID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblPayments_Void
    @PaymentID INT,
    @Reason    NVARCHAR(300),
    @VoidedBy  NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @Reason IS NULL OR LTRIM(RTRIM(@Reason)) = N'' RAISERROR(N'A reason is required to void a payment.', 16, 1);

        DECLARE @BillID INT, @PStatus NVARCHAR(20), @Amount DECIMAL(12,2);
        SELECT @BillID = BillID, @PStatus = Status, @Amount = Amount
        FROM   dbo.tblPayments WITH (UPDLOCK) WHERE PaymentID = @PaymentID;

        IF @BillID IS NULL RAISERROR(N'The payment was not found.', 16, 1);
        IF @PStatus = N'Voided' RAISERROR(N'This payment is already voided.', 16, 1);

        DECLARE @Total DECIMAL(12,2), @Paid DECIMAL(12,2), @BStatus NVARCHAR(20), @AdmStatus NVARCHAR(20);
        SELECT @Total = b.TotalAmount, @Paid = b.AmountPaid, @BStatus = b.Status, @AdmStatus = a.Status
        FROM   dbo.tblBills b WITH (UPDLOCK) JOIN dbo.tblAdmissions a ON a.AdmissionID = b.AdmissionID
        WHERE  b.BillID = @BillID;

        IF @AdmStatus = N'Discharged' RAISERROR(N'The patient is already discharged. Payments can no longer be voided.', 16, 1);
        IF @BStatus = N'Voided' RAISERROR(N'The bill was voided.', 16, 1);

        UPDATE dbo.tblPayments
        SET    Status = N'Voided', VoidReason = LTRIM(RTRIM(@Reason)), VoidedBy = @VoidedBy, VoidedAt = SYSUTCDATETIME()
        WHERE  PaymentID = @PaymentID;

        UPDATE dbo.tblBills
        SET    AmountPaid = @Paid - @Amount,
               Status = CASE WHEN @Paid - @Amount <= 0 THEN N'Open' ELSE N'PartiallyPaid' END
        WHERE  BillID = @BillID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblPayments_GetByBill
    @BillID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PaymentID, ReceiptNo, BillID, PaymentDate, Amount, Method, ReferenceNo, ReceivedBy, Status, VoidReason
    FROM   dbo.tblPayments WHERE BillID = @BillID
    ORDER BY PaymentDate, PaymentID;
END
GO

-- @From and @To are inclusive dates. @PatientID NULL = every patient.
CREATE PROCEDURE dbo.usp_tblPayments_GetHistory
    @From      DATE,
    @To        DATE,
    @PatientID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.PaymentID, p.ReceiptNo, p.BillID, p.PaymentDate, p.Amount, p.Method, p.ReferenceNo,
           p.ReceivedBy, p.Status, p.VoidReason,
           b.BillNo, pt.LastName + N', ' + pt.FirstName AS PatientName
    FROM   dbo.tblPayments p
    JOIN   dbo.tblBills b ON b.BillID = p.BillID
    JOIN   dbo.tblAdmissions a ON a.AdmissionID = b.AdmissionID
    JOIN   dbo.tblPatients pt ON pt.PatientID = a.PatientID
    WHERE  p.PaymentDate >= @From AND p.PaymentDate < DATEADD(DAY, 1, @To)
      AND  (@PatientID IS NULL OR a.PatientID = @PatientID)
    ORDER BY p.PaymentDate DESC, p.PaymentID DESC;
END
GO

CREATE PROCEDURE dbo.usp_Receipt_GetByPayment
    @PaymentID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.PaymentID, p.ReceiptNo, p.PaymentDate,
           pt.LastName + N', ' + pt.FirstName AS PatientName,
           b.BillNo, a.AdmissionNo, p.Amount, p.Method, p.ReferenceNo, b.TotalAmount,
           b.TotalAmount - ISNULL((SELECT SUM(x.Amount) FROM dbo.tblPayments x
                                   WHERE x.BillID = p.BillID AND x.Status = N'Posted' AND x.PaymentID <= p.PaymentID), 0)
               AS BalanceAfter,
           p.ReceivedBy
    FROM   dbo.tblPayments p
    JOIN   dbo.tblBills b ON b.BillID = p.BillID
    JOIN   dbo.tblAdmissions a ON a.AdmissionID = b.AdmissionID
    JOIN   dbo.tblPatients pt ON pt.PatientID = a.PatientID
    WHERE  p.PaymentID = @PaymentID;
END
