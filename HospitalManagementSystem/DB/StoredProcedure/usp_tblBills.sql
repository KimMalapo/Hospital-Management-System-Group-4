CREATE PROCEDURE dbo.usp_tblBills_Generate
    @AdmissionID INT,
    @GeneratedBy NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @Status NVARCHAR(20), @AssignmentID INT, @AdmitDate DATETIME2(0);
        SELECT @Status = Status, @AssignmentID = AssignmentID, @AdmitDate = AdmitDate
        FROM   dbo.tblAdmissions WITH (UPDLOCK) WHERE AdmissionID = @AdmissionID;

        IF @Status IS NULL RAISERROR(N'The admission was not found.', 16, 1);
        IF @Status <> N'Admitted' RAISERROR(N'A bill can only be generated for an active admission.', 16, 1);
        IF EXISTS (SELECT 1 FROM dbo.tblBills WHERE AdmissionID = @AdmissionID AND Status <> N'Voided')
            RAISERROR(N'A bill already exists for this admission.', 16, 1);

        -- Room charge: rate x days, a started day counts as a full day, minimum 1 day
        DECLARE @RoomNo NVARCHAR(20), @Rate DECIMAL(10,2), @Days INT;
        SELECT @RoomNo = r.RoomNo, @Rate = r.Rate
        FROM   dbo.tblRoomAssignments ra JOIN dbo.tblRooms r ON r.RoomNo = ra.RoomNo
        WHERE  ra.AssignmentID = @AssignmentID;

        SET @Days = CASE WHEN SYSUTCDATETIME() <= @AdmitDate THEN 1
                         ELSE (DATEDIFF(HOUR, @AdmitDate, SYSUTCDATETIME()) + 23) / 24 END;
        IF @Days < 1 SET @Days = 1;

        DECLARE @RoomTotal DECIMAL(12,2) = ISNULL(@Rate, 0) * @Days;
        DECLARE @SvcTotal  DECIMAL(12,2) = ISNULL((SELECT SUM(LineTotal) FROM dbo.tblAdmissionCharges
                                                   WHERE AdmissionID = @AdmissionID AND IsVoided = 0), 0);

        INSERT INTO dbo.tblBills (AdmissionID, RoomCharges, ServiceCharges, Status, GeneratedBy)
        VALUES (@AdmissionID, @RoomTotal, @SvcTotal,
                CASE WHEN @RoomTotal + @SvcTotal = 0 THEN N'Paid' ELSE N'Open' END, @GeneratedBy);
        DECLARE @BillID INT = CAST(SCOPE_IDENTITY() AS INT);

        INSERT INTO dbo.tblBillItems (BillID, ItemType, Description, Quantity, UnitPrice, Amount)
        VALUES (@BillID, N'Room', N'Room ' + ISNULL(@RoomNo, N'?') + N' (per day)', @Days, ISNULL(@Rate, 0), @RoomTotal);

        INSERT INTO dbo.tblBillItems (BillID, ItemType, Description, Quantity, UnitPrice, Amount)
        SELECT @BillID, N'Service', Description, Quantity, UnitPrice, LineTotal
        FROM   dbo.tblAdmissionCharges
        WHERE  AdmissionID = @AdmissionID AND IsVoided = 0
        ORDER BY ChargedAt, ChargeID;

        COMMIT TRANSACTION;
        SELECT @BillID AS BillID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblBills_GetByAdmission
    @AdmissionID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT BillID, BillNo, AdmissionID, RoomCharges, ServiceCharges, Discount, DiscountReason,
           TotalAmount, AmountPaid, Balance, Status, GeneratedAt, GeneratedBy
    FROM   dbo.tblBills
    WHERE  AdmissionID = @AdmissionID AND Status <> N'Voided';
END
GO

CREATE PROCEDURE dbo.usp_tblBills_GetById
    @BillID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT BillID, BillNo, AdmissionID, RoomCharges, ServiceCharges, Discount, DiscountReason,
           TotalAmount, AmountPaid, Balance, Status, GeneratedAt, GeneratedBy
    FROM   dbo.tblBills WHERE BillID = @BillID;
END
GO

CREATE PROCEDURE dbo.usp_tblBills_GetItems
    @BillID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT BillItemID, BillID, ItemType, Description, Quantity, UnitPrice, Amount
    FROM   dbo.tblBillItems WHERE BillID = @BillID
    ORDER BY CASE ItemType WHEN N'Room' THEN 0 ELSE 1 END, BillItemID;
END
GO

-- Sets (not adds to) the discount of a bill. Pass 0 to remove it.
CREATE PROCEDURE dbo.usp_tblBills_ApplyDiscount
    @BillID    INT,
    @Amount    DECIMAL(12,2),
    @Reason    NVARCHAR(300) = NULL,
    @AppliedBy NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Status NVARCHAR(20), @Gross DECIMAL(12,2), @Paid DECIMAL(12,2);
        SELECT @Status = Status, @Gross = RoomCharges + ServiceCharges, @Paid = AmountPaid
        FROM   dbo.tblBills WITH (UPDLOCK) WHERE BillID = @BillID;

        IF @Status IS NULL RAISERROR(N'The bill was not found.', 16, 1);
        IF @Status = N'Voided' RAISERROR(N'A voided bill cannot be changed.', 16, 1);
        IF @Status = N'Paid' RAISERROR(N'A fully paid bill cannot be changed.', 16, 1);
        IF @Amount < 0 RAISERROR(N'The discount cannot be negative.', 16, 1);
        IF @Amount > @Gross RAISERROR(N'The discount cannot be more than the bill total.', 16, 1);
        IF @Amount > 0 AND (@Reason IS NULL OR LTRIM(RTRIM(@Reason)) = N'')
            RAISERROR(N'A reason is required for a discount.', 16, 1);
        IF @Gross - @Amount < @Paid
            RAISERROR(N'The discount would be more than the unpaid amount.', 16, 1);

        UPDATE dbo.tblBills
        SET    Discount = @Amount,
               DiscountReason = CASE WHEN @Amount > 0 THEN LTRIM(RTRIM(@Reason)) END,
               Status = CASE WHEN @Gross - @Amount - @Paid = 0 THEN N'Paid'
                             WHEN @Paid > 0 THEN N'PartiallyPaid' ELSE N'Open' END
        WHERE  BillID = @BillID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblBills_Void
    @BillID   INT,
    @Reason   NVARCHAR(300),
    @VoidedBy NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @Reason IS NULL OR LTRIM(RTRIM(@Reason)) = N'' RAISERROR(N'A reason is required to void a bill.', 16, 1);

        DECLARE @Status NVARCHAR(20), @Paid DECIMAL(12,2);
        SELECT @Status = Status, @Paid = AmountPaid FROM dbo.tblBills WITH (UPDLOCK) WHERE BillID = @BillID;

        IF @Status IS NULL RAISERROR(N'The bill was not found.', 16, 1);
        IF @Status = N'Voided' RAISERROR(N'This bill is already voided.', 16, 1);
        IF @Paid > 0 RAISERROR(N'This bill has payments. Void the payments first.', 16, 1);

        UPDATE dbo.tblBills
        SET    Status = N'Voided', VoidReason = LTRIM(RTRIM(@Reason)), VoidedBy = @VoidedBy, VoidedAt = SYSUTCDATETIME()
        WHERE  BillID = @BillID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
