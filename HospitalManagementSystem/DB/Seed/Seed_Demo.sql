-- Demo data for the defense. Every demo patient has an e-mail ending in @demo.hms.
-- Re-runnable: the whole block is skipped when demo patients already exist.
-- It is built with the real stored procedures, so it proves the full cycle works.

IF NOT EXISTS (SELECT 1 FROM dbo.tblPatients WHERE Email LIKE N'%@demo.hms')
BEGIN
    INSERT INTO dbo.tblPatients (FirstName, LastName, DateOfBirth, Gender, ContactNumber, Address, Email) VALUES
    (N'Maria',   N'Santos',    '1985-03-12', N'Female', N'0917-100-0001', N'12 Rizal St, Quezon City',      N'maria.santos@demo.hms'),
    (N'Juan',    N'Dela Cruz', '1972-11-02', N'Male',   N'0917-100-0002', N'45 Mabini Ave, Manila',         N'juan.delacruz@demo.hms'),
    (N'Ana',     N'Reyes',     '1990-07-25', N'Female', N'0917-100-0003', N'8 Bonifacio St, Pasig',         N'ana.reyes@demo.hms'),
    (N'Pedro',   N'Garcia',    '1964-01-30', N'Male',   N'0917-100-0004', N'23 Luna St, Makati',            N'pedro.garcia@demo.hms'),
    (N'Liza',    N'Mendoza',   '2001-09-09', N'Female', N'0917-100-0005', N'77 Aguinaldo Hwy, Cavite',      N'liza.mendoza@demo.hms'),
    (N'Carlo',   N'Ramos',     '1978-05-18', N'Male',   N'0917-100-0006', N'5 Katipunan Ave, Quezon City',  N'carlo.ramos@demo.hms'),
    (N'Grace',   N'Villanueva','1955-12-21', N'Female', N'0917-100-0007', N'91 Taft Ave, Manila',           N'grace.villanueva@demo.hms'),
    (N'Mark',    N'Torres',    '1995-04-04', N'Male',   N'0917-100-0008', N'14 Ortigas Ave, Pasig',         N'mark.torres@demo.hms'),
    (N'Joy',     N'Aquino',    '2015-08-16', N'Female', N'0917-100-0009', N'3 Commonwealth Ave, QC',        N'joy.aquino@demo.hms'),
    (N'Ramon',   N'Bautista',  '1949-02-27', N'Male',   N'0917-100-0010', N'60 Espana Blvd, Manila',        N'ramon.bautista@demo.hms');

    DECLARE @Maria INT = (SELECT PatientID FROM dbo.tblPatients WHERE Email = N'maria.santos@demo.hms');
    DECLARE @Juan  INT = (SELECT PatientID FROM dbo.tblPatients WHERE Email = N'juan.delacruz@demo.hms');
    DECLARE @Ana   INT = (SELECT PatientID FROM dbo.tblPatients WHERE Email = N'ana.reyes@demo.hms');
    DECLARE @Pedro INT = (SELECT PatientID FROM dbo.tblPatients WHERE Email = N'pedro.garcia@demo.hms');
    DECLARE @when  DATETIME2(0);
    DECLARE @adm   INT, @bill INT, @svc INT;
    DECLARE @ids TABLE (Id INT);

    -- 1) Maria: admitted only (room 101)
    SET @when = DATEADD(HOUR, -20, SYSUTCDATETIME());
    DELETE FROM @ids; INSERT INTO @ids EXEC dbo.usp_tblAdmissions_Admit
        @PatientID = @Maria, @RoomNo = N'101', @DepartmentCode = N'MED', @DiagnosisCode = N'J18',
        @AttendingPhysician = N'Dr. Cruz', @Reason = N'Fever and cough for 4 days', @AdmitDate = @when, @CreatedBy = N'demo seed';

    -- 2) Juan: admitted, charges added, bill generated but unpaid (room 102)
    SET @when = DATEADD(DAY, -2, SYSUTCDATETIME());
    DELETE FROM @ids; INSERT INTO @ids EXEC dbo.usp_tblAdmissions_Admit
        @PatientID = @Juan, @RoomNo = N'102', @DepartmentCode = N'MED', @DiagnosisCode = N'I10',
        @AttendingPhysician = N'Dr. Lim', @Reason = N'High blood pressure, monitoring', @AdmitDate = @when, @CreatedBy = N'demo seed';
    SELECT @adm = MAX(Id) FROM @ids;
    SELECT @svc = ServiceID FROM dbo.tblServices WHERE Code = N'CONS-SPEC' AND IsActive = 1;
    EXEC dbo.usp_tblAdmissionCharges_Add @AdmissionID = @adm, @ServiceID = @svc, @Quantity = 1, @ChargedBy = N'demo seed';
    SELECT @svc = ServiceID FROM dbo.tblServices WHERE Code = N'LAB-CBC' AND IsActive = 1;
    EXEC dbo.usp_tblAdmissionCharges_Add @AdmissionID = @adm, @ServiceID = @svc, @Quantity = 1, @ChargedBy = N'demo seed';
    SELECT @svc = ServiceID FROM dbo.tblServices WHERE Code = N'MED-PARA' AND IsActive = 1;
    EXEC dbo.usp_tblAdmissionCharges_Add @AdmissionID = @adm, @ServiceID = @svc, @Quantity = 12, @ChargedBy = N'demo seed';
    DELETE FROM @ids; INSERT INTO @ids EXEC dbo.usp_tblBills_Generate @AdmissionID = @adm, @GeneratedBy = N'demo seed';

    -- 3) Ana: full cycle, admitted, charged, billed, paid in two payments and discharged (room 201)
    SET @when = DATEADD(DAY, -3, SYSUTCDATETIME());
    DELETE FROM @ids; INSERT INTO @ids EXEC dbo.usp_tblAdmissions_Admit
        @PatientID = @Ana, @RoomNo = N'201', @DepartmentCode = N'SURG', @DiagnosisCode = N'K35',
        @AttendingPhysician = N'Dr. Tan', @Reason = N'Acute abdominal pain', @AdmitDate = @when, @CreatedBy = N'demo seed';
    SELECT @adm = MAX(Id) FROM @ids;
    SELECT @svc = ServiceID FROM dbo.tblServices WHERE Code = N'PRC-APP' AND IsActive = 1;
    EXEC dbo.usp_tblAdmissionCharges_Add @AdmissionID = @adm, @ServiceID = @svc, @Quantity = 1, @ChargedBy = N'demo seed';
    SELECT @svc = ServiceID FROM dbo.tblServices WHERE Code = N'IMG-US' AND IsActive = 1;
    EXEC dbo.usp_tblAdmissionCharges_Add @AdmissionID = @adm, @ServiceID = @svc, @Quantity = 1, @ChargedBy = N'demo seed';
    DELETE FROM @ids; INSERT INTO @ids EXEC dbo.usp_tblBills_Generate @AdmissionID = @adm, @GeneratedBy = N'demo seed';
    SELECT @bill = MAX(Id) FROM @ids;
    EXEC dbo.usp_tblPayments_Post @BillID = @bill, @Amount = 20000, @Method = N'Cash', @ReceivedBy = N'demo seed';
    DECLARE @rest DECIMAL(12,2) = (SELECT Balance FROM dbo.tblBills WHERE BillID = @bill);
    EXEC dbo.usp_tblPayments_Post @BillID = @bill, @Amount = @rest, @Method = N'Card', @ReferenceNo = N'DEMO-CARD-0001', @ReceivedBy = N'demo seed';
    EXEC dbo.usp_tblAdmissions_Discharge @AdmissionID = @adm, @DischargeSummary = N'Recovered well after surgery. Follow-up in two weeks.', @DischargedBy = N'demo seed';
END
