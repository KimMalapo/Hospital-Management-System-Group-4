CREATE PROCEDURE dbo.usp_tblAdmissionCharges_Add
    @AdmissionID INT,
    @ServiceID   INT,
    @Quantity    INT,
    @ChargedBy   NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @Quantity IS NULL OR @Quantity <= 0 RAISERROR(N'The quantity must be at least 1.', 16, 1);

        DECLARE @Status NVARCHAR(20) = (SELECT Status FROM dbo.tblAdmissions WITH (UPDLOCK) WHERE AdmissionID = @AdmissionID);
        IF @Status IS NULL RAISERROR(N'The admission was not found.', 16, 1);
        IF @Status <> N'Admitted' RAISERROR(N'Charges can only be added to an active admission.', 16, 1);

        -- A bill that already exists (not voided) freezes the charges
        IF OBJECT_ID(N'dbo.tblBills', N'U') IS NOT NULL
        BEGIN
            DECLARE @b INT = 0;
            EXEC sp_executesql
                N'SELECT @b = COUNT(*) FROM dbo.tblBills WHERE AdmissionID = @a AND Status <> N''Voided''',
                N'@a INT, @b INT OUTPUT', @a = @AdmissionID, @b = @b OUTPUT;
            IF @b > 0 RAISERROR(N'A bill was already generated. Void it before adding charges.', 16, 1);
        END

        DECLARE @Name NVARCHAR(200), @Price DECIMAL(10,2), @Active BIT;
        SELECT @Name = Name, @Price = UnitPrice, @Active = IsActive FROM dbo.tblServices WHERE ServiceID = @ServiceID;
        IF @Name IS NULL RAISERROR(N'The service was not found.', 16, 1);
        IF @Active = 0 RAISERROR(N'This service is no longer active.', 16, 1);

        INSERT INTO dbo.tblAdmissionCharges (AdmissionID, ServiceID, Description, Quantity, UnitPrice, ChargedBy)
        VALUES (@AdmissionID, @ServiceID, @Name, @Quantity, @Price, @ChargedBy);
        DECLARE @ChargeID INT = CAST(SCOPE_IDENTITY() AS INT);

        COMMIT TRANSACTION;
        SELECT @ChargeID AS ChargeID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblAdmissionCharges_Void
    @ChargeID INT,
    @Reason   NVARCHAR(300),
    @VoidedBy NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF @Reason IS NULL OR LTRIM(RTRIM(@Reason)) = N'' RAISERROR(N'A reason is required to void a charge.', 16, 1);

        DECLARE @Adm INT, @Voided BIT;
        SELECT @Adm = AdmissionID, @Voided = IsVoided FROM dbo.tblAdmissionCharges WITH (UPDLOCK) WHERE ChargeID = @ChargeID;
        IF @Adm IS NULL RAISERROR(N'The charge was not found.', 16, 1);
        IF @Voided = 1 RAISERROR(N'This charge is already voided.', 16, 1);

        IF OBJECT_ID(N'dbo.tblBills', N'U') IS NOT NULL
        BEGIN
            DECLARE @b INT = 0;
            EXEC sp_executesql
                N'SELECT @b = COUNT(*) FROM dbo.tblBills WHERE AdmissionID = @a AND Status <> N''Voided''',
                N'@a INT, @b INT OUTPUT', @a = @Adm, @b = @b OUTPUT;
            IF @b > 0 RAISERROR(N'A bill was already generated. Void it before voiding charges.', 16, 1);
        END

        UPDATE dbo.tblAdmissionCharges
        SET    IsVoided = 1, VoidReason = LTRIM(RTRIM(@Reason)), VoidedBy = @VoidedBy, VoidedAt = SYSUTCDATETIME()
        WHERE  ChargeID = @ChargeID;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_tblAdmissionCharges_GetByAdmission
    @AdmissionID   INT,
    @IncludeVoided BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ChargeID, AdmissionID, ServiceID, Description, Quantity, UnitPrice, LineTotal,
           ChargedAt, ChargedBy, IsVoided, VoidReason
    FROM   dbo.tblAdmissionCharges
    WHERE  AdmissionID = @AdmissionID AND (@IncludeVoided = 1 OR IsVoided = 0)
    ORDER BY ChargedAt, ChargeID;
END
