-- Re-runnable realistic demo data: patient, room, admission, clinical, billing, and payment examples.
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;

-- Replace the earlier placeholder identities in place; do not alter unrelated patients.
DECLARE @LegacyPatients TABLE
(
    SeedNo INT NOT NULL PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Gender NVARCHAR(20) NOT NULL,
    Street NVARCHAR(100) NOT NULL,
    City NVARCHAR(60) NOT NULL
);

INSERT INTO @LegacyPatients (SeedNo, FirstName, LastName, Gender, Street, City)
SELECT ROW_NUMBER() OVER (ORDER BY f.FirstName, l.LastName), f.FirstName, l.LastName, f.Gender, l.Street, l.City
FROM (VALUES
    (N'Ana', N'Female'), (N'Carlo', N'Male'), (N'Lea', N'Female'), (N'Miguel', N'Male'), (N'Maria', N'Female'),
    (N'Jose', N'Male'), (N'Sofia', N'Female'), (N'Gabriel', N'Male'), (N'Angela', N'Female'), (N'Rafael', N'Male')
) f(FirstName, Gender)
CROSS JOIN (VALUES
    (N'Reyes', N'Rizal Street', N'Quezon City'), (N'Santos', N'Mabini Avenue', N'Manila'),
    (N'Cruz', N'Bonifacio Street', N'Pasig'), (N'Mendoza', N'Katipunan Avenue', N'Quezon City'),
    (N'Garcia', N'Legazpi Street', N'Makati')
) l(LastName, Street, City);

UPDATE p
SET p.FirstName = s.FirstName,
    p.LastName = s.LastName,
    p.DateOfBirth = DATEADD(DAY, (s.SeedNo * 37) % 365, DATEFROMPARTS(1970 + (s.SeedNo % 30), 1, 1)),
    p.Gender = s.Gender,
    p.ContactNumber = N'09' + RIGHT(N'000000000' + CONVERT(NVARCHAR(9), 170000000 + s.SeedNo), 9),
    p.Address = CONVERT(NVARCHAR(5), 10 + s.SeedNo) + N' ' + s.Street + N', ' + s.City,
    p.Email = LOWER(s.FirstName + N'.' + s.LastName + RIGHT(N'000' + CONVERT(NVARCHAR(3), s.SeedNo), 3) + N'@example.test'),
    p.UpdatedAt = SYSUTCDATETIME()
FROM dbo.tblPatients p
JOIN @LegacyPatients s
  ON p.Email = N'patient' + RIGHT(N'000' + CONVERT(NVARCHAR(3), s.SeedNo), 3) + N'@example.test'
  OR p.Email = LOWER(s.FirstName + N'.' + s.LastName + RIGHT(N'000' + CONVERT(NVARCHAR(3), s.SeedNo), 3) + N'@example.test');

INSERT INTO dbo.tblPatients (FirstName, LastName, DateOfBirth, Gender, ContactNumber, Address, Email)
SELECT s.FirstName, s.LastName, s.DateOfBirth, s.Gender, s.ContactNumber, s.Address, s.Email
FROM (VALUES
    (N'Maria', N'Santos', CONVERT(date, '19850312'), N'Female', N'0917-100-0001', N'12 Rizal St, Quezon City', N'maria.santos@demo.hms'),
    (N'Juan', N'Dela Cruz', CONVERT(date, '19721102'), N'Male', N'0917-100-0002', N'45 Mabini Ave, Manila', N'juan.delacruz@demo.hms'),
    (N'Ana', N'Reyes', CONVERT(date, '19900725'), N'Female', N'0917-100-0003', N'8 Bonifacio St, Pasig', N'ana.reyes@demo.hms'),
    (N'Pedro', N'Garcia', CONVERT(date, '19640130'), N'Male', N'0917-100-0004', N'23 Luna St, Makati', N'pedro.garcia@demo.hms'),
    (N'Liza', N'Mendoza', CONVERT(date, '20010909'), N'Female', N'0917-100-0005', N'77 Aguinaldo Hwy, Cavite', N'liza.mendoza@demo.hms'),
    (N'Carlo', N'Ramos', CONVERT(date, '19780518'), N'Male', N'0917-100-0006', N'5 Katipunan Ave, Quezon City', N'carlo.ramos@demo.hms'),
    (N'Grace', N'Villanueva', CONVERT(date, '19551221'), N'Female', N'0917-100-0007', N'91 Taft Ave, Manila', N'grace.villanueva@demo.hms'),
    (N'Mark', N'Torres', CONVERT(date, '19950404'), N'Male', N'0917-100-0008', N'14 Ortigas Ave, Pasig', N'mark.torres@demo.hms'),
    (N'Joy', N'Aquino', CONVERT(date, '20150816'), N'Female', N'0917-100-0009', N'3 Commonwealth Ave, QC', N'joy.aquino@demo.hms'),
    (N'Ramon', N'Bautista', CONVERT(date, '19490227'), N'Male', N'0917-100-0010', N'60 Espana Blvd, Manila', N'ramon.bautista@demo.hms'),
    (N'Noel', N'Villareal', CONVERT(date, '19830209'), N'Male', N'0917-100-0016', N'18 P. Tuazon St, Quezon City', N'noel.villareal@demo.hms'),
    (N'Rosa', N'Dizon', CONVERT(date, '19761127'), N'Female', N'0917-100-0017', N'6 Pedro Gil St, Manila', N'rosa.dizon@demo.hms'),
    (N'Paolo', N'Gonzales', CONVERT(date, '19990413'), N'Male', N'0917-100-0018', N'55 J.P. Rizal, Makati', N'paolo.gonzales@demo.hms'),
    (N'Teresa', N'Chua', CONVERT(date, '19580530'), N'Female', N'0917-100-0019', N'27 Wilson St, San Juan', N'teresa.chua@demo.hms'),
    (N'Francis', N'Lim', CONVERT(date, '20031102'), N'Male', N'0917-100-0020', N'14 Congressional Ave, Quezon City', N'francis.lim@demo.hms'),
    (N'Elena', N'Navarro', CONVERT(date, '19880614'), N'Female', N'0917-100-0021', N'16 Aurora Blvd, Quezon City', N'elena.navarro@demo.hms'),
    (N'Luis', N'Fernandez', CONVERT(date, '19750923'), N'Male', N'0917-100-0022', N'22 Taft Avenue, Manila', N'luis.fernandez@demo.hms'),
    (N'Bea', N'Castillo', CONVERT(date, '20080219'), N'Female', N'0917-100-0023', N'9 Shaw Boulevard, Mandaluyong', N'bea.castillo@demo.hms'),
    (N'Andres', N'Ramos', CONVERT(date, '19681205'), N'Male', N'0917-100-0024', N'31 EDSA, Makati', N'andres.ramos@demo.hms'),
    (N'Camille', N'Flores', CONVERT(date, '19930811'), N'Female', N'0917-100-0025', N'4 Ortigas Avenue, Pasig', N'camille.flores@demo.hms')
) s(FirstName, LastName, DateOfBirth, Gender, ContactNumber, Address, Email)
WHERE NOT EXISTS (SELECT 1 FROM dbo.tblPatients p WHERE p.Email = s.Email);

BEGIN
    DECLARE @Maria INT = (SELECT PatientID FROM dbo.tblPatients WHERE Email = N'maria.santos@demo.hms');
    DECLARE @Juan  INT = (SELECT PatientID FROM dbo.tblPatients WHERE Email = N'juan.delacruz@demo.hms');
    DECLARE @Ana   INT = (SELECT PatientID FROM dbo.tblPatients WHERE Email = N'ana.reyes@demo.hms');
    DECLARE @Pedro INT = (SELECT PatientID FROM dbo.tblPatients WHERE Email = N'pedro.garcia@demo.hms');
    DECLARE @when  DATETIME2(0);
    DECLARE @adm   INT, @bill INT, @svc INT;
    DECLARE @ids TABLE (Id INT);

    -- 1) Maria: admitted only (room 101)
    IF NOT EXISTS (SELECT 1 FROM dbo.tblAdmissions WHERE PatientID = @Maria AND CreatedBy = N'demo seed')
    BEGIN
    SET @when = DATEADD(HOUR, -20, SYSUTCDATETIME());
    DELETE FROM @ids; INSERT INTO @ids EXEC dbo.usp_tblAdmissions_Admit
        @PatientID = @Maria, @RoomNo = N'101', @DepartmentCode = N'MED', @DiagnosisCode = N'J18',
        @AttendingPhysician = N'Dr. Cruz', @Reason = N'Fever and cough for 4 days', @AdmitDate = @when, @CreatedBy = N'demo seed';
    END

    -- 2) Juan: admitted, charges added, bill generated but unpaid (room 102)
    IF NOT EXISTS (SELECT 1 FROM dbo.tblAdmissions WHERE PatientID = @Juan AND CreatedBy = N'demo seed')
    BEGIN
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
    END

    -- 3) Ana: full cycle, admitted, charged, billed, paid in two payments and discharged (room 201)
    IF NOT EXISTS (SELECT 1 FROM dbo.tblAdmissions WHERE PatientID = @Ana AND CreatedBy = N'demo seed')
    BEGIN
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
END;

-- Add admission, clinical charge, billing, and payment examples for the remaining demo patients.
DECLARE @AdditionalCases TABLE
(
    CaseNo INT NOT NULL PRIMARY KEY,
    PatientEmail NVARCHAR(200) NOT NULL,
    RoomNo NVARCHAR(20) NOT NULL,
    DepartmentCode NVARCHAR(50) NOT NULL,
    DiagnosisCode NVARCHAR(50) NOT NULL,
    Physician NVARCHAR(200) NOT NULL,
    Reason NVARCHAR(500) NOT NULL,
    ServiceCode1 NVARCHAR(50) NOT NULL,
    ServiceCode2 NVARCHAR(50) NOT NULL
);

INSERT INTO @AdditionalCases
VALUES
    (1, N'pedro.garcia@demo.hms', N'301', N'MED', N'E11', N'Dr. Elena Garcia', N'Blood glucose monitoring', N'LAB-GLU', N'MED-AMOX'),
    (2, N'liza.mendoza@demo.hms', N'302', N'EMR', N'S72', N'Dr. Rafael Bautista', N'Fall injury assessment', N'IMG-XRAY', N'SUP-DRSG'),
    (3, N'carlo.ramos@demo.hms', N'303', N'MED', N'I10', N'Dr. Sofia Reyes', N'Blood pressure evaluation', N'LAB-CBC', N'CONS-SPEC'),
    (4, N'grace.villanueva@demo.hms', N'304', N'MED', N'J18', N'Dr. Daniel Santos', N'Respiratory observation', N'IMG-XRAY', N'MED-IVF'),
    (5, N'mark.torres@demo.hms', N'305', N'EMR', N'S72', N'Dr. Maya Cruz', N'Minor injury assessment', N'PRC-SUT', N'SUP-GLOVE'),
    (6, N'joy.aquino@demo.hms', N'306', N'PED', N'J18', N'Dr. Isabel Flores', N'Pediatric respiratory observation', N'LAB-CBC', N'MED-AMOX'),
    (7, N'ramon.bautista@demo.hms', N'307', N'MED', N'E11', N'Dr. Marco Ramos', N'Routine glucose monitoring', N'LAB-GLU', N'CONS-SPEC'),
    (8, N'elena.navarro@demo.hms', N'201', N'OBG', N'O80', N'Dr. Patricia Lim', N'Post-delivery monitoring', N'LAB-CBC', N'CONS-SPEC'),
    (9, N'luis.fernandez@demo.hms', N'201', N'CAR', N'I10', N'Dr. Roberto Tan', N'Cardiac observation', N'LAB-CBC', N'IMG-XRAY'),
    (10, N'bea.castillo@demo.hms', N'104', N'PED', N'J45', N'Dr. Isabel Flores', N'Asthma observation', N'LAB-CBC', N'MED-AMOX'),
    (11, N'andres.ramos@demo.hms', N'105', N'ORT', N'M54', N'Dr. Marco Ramos', N'Back pain assessment', N'IMG-XRAY', N'CONS-SPEC'),
    (12, N'camille.flores@demo.hms', N'106', N'MED', N'N39', N'Dr. Elena Garcia', N'Urinary infection treatment', N'LAB-URI', N'MED-AMOX');

DECLARE @MorePatientID INT;
DECLARE @MoreRoomNo NVARCHAR(20);
DECLARE @MoreDepartmentCode NVARCHAR(50);
DECLARE @MoreDiagnosisCode NVARCHAR(50);
DECLARE @MorePhysician NVARCHAR(200);
DECLARE @MoreReason NVARCHAR(500);
DECLARE @MoreAdmitDate DATETIME2(0);
DECLARE @MoreCaseNo INT;
DECLARE @MoreAdmissionID INT;
DECLARE @MoreServiceID INT;
DECLARE @MoreServiceCode NVARCHAR(50);
DECLARE @MoreQuantity INT;
DECLARE @MoreIDs TABLE (ID INT);

DECLARE more_admission_cursor CURSOR LOCAL FAST_FORWARD FOR
    SELECT c.CaseNo, p.PatientID, c.RoomNo, c.DepartmentCode, c.DiagnosisCode, c.Physician, c.Reason
    FROM @AdditionalCases c
    JOIN dbo.tblPatients p ON p.Email = c.PatientEmail AND p.IsActive = 1
    ORDER BY c.CaseNo;

OPEN more_admission_cursor;
FETCH NEXT FROM more_admission_cursor INTO @MoreCaseNo, @MorePatientID, @MoreRoomNo, @MoreDepartmentCode, @MoreDiagnosisCode, @MorePhysician, @MoreReason;
WHILE @@FETCH_STATUS = 0
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.tblAdmissions WHERE PatientID = @MorePatientID AND CreatedBy = N'demo seed')
       AND EXISTS (SELECT 1 FROM dbo.tblRooms r WHERE r.RoomNo = @MoreRoomNo AND r.IsActive = 1
                   AND r.Status NOT IN (N'Maintenance', N'Reserved')
                   AND (SELECT COUNT(*) FROM dbo.tblRoomAssignments a
                        WHERE a.RoomNo = r.RoomNo AND a.DischargeDate IS NULL) < r.Capacity)
    BEGIN
        DELETE FROM @MoreIDs;
        SET @MoreAdmitDate = DATEADD(DAY, -(@MoreCaseNo + 1), SYSUTCDATETIME());
        INSERT INTO @MoreIDs (ID)
        EXEC dbo.usp_tblAdmissions_Admit
            @PatientID = @MorePatientID,
            @RoomNo = @MoreRoomNo,
            @DepartmentCode = @MoreDepartmentCode,
            @DiagnosisCode = @MoreDiagnosisCode,
            @AttendingPhysician = @MorePhysician,
            @Reason = @MoreReason,
            @AdmitDate = @MoreAdmitDate,
            @CreatedBy = N'demo seed';
    END;

    FETCH NEXT FROM more_admission_cursor INTO @MoreCaseNo, @MorePatientID, @MoreRoomNo, @MoreDepartmentCode, @MoreDiagnosisCode, @MorePhysician, @MoreReason;
END;
CLOSE more_admission_cursor;
DEALLOCATE more_admission_cursor;

DECLARE more_charge_cursor CURSOR LOCAL FAST_FORWARD FOR
    SELECT a.AdmissionID, svc.ServiceID, v.Quantity
    FROM @AdditionalCases c
    JOIN dbo.tblPatients p ON p.Email = c.PatientEmail
    JOIN dbo.tblAdmissions a ON a.PatientID = p.PatientID AND a.CreatedBy = N'demo seed' AND a.Status = N'Admitted'
    CROSS APPLY (VALUES (c.ServiceCode1, 1), (c.ServiceCode2, CASE WHEN c.CaseNo = 7 THEN 2 ELSE 1 END)) v(ServiceCode, Quantity)
    JOIN dbo.tblServices svc ON svc.Code = v.ServiceCode AND svc.IsActive = 1
    WHERE NOT EXISTS (SELECT 1 FROM dbo.tblBills b WHERE b.AdmissionID = a.AdmissionID AND b.Status <> N'Voided')
      AND NOT EXISTS
      (
          SELECT 1 FROM dbo.tblAdmissionCharges ch
          WHERE ch.AdmissionID = a.AdmissionID AND ch.ServiceID = svc.ServiceID
            AND ch.ChargedBy = N'demo seed' AND ch.IsVoided = 0
      )
    ORDER BY c.CaseNo, svc.ServiceID;

OPEN more_charge_cursor;
FETCH NEXT FROM more_charge_cursor INTO @MoreAdmissionID, @MoreServiceID, @MoreQuantity;
WHILE @@FETCH_STATUS = 0
BEGIN
    DELETE FROM @MoreIDs;
    INSERT INTO @MoreIDs (ID)
    EXEC dbo.usp_tblAdmissionCharges_Add
        @AdmissionID = @MoreAdmissionID,
        @ServiceID = @MoreServiceID,
        @Quantity = @MoreQuantity,
        @ChargedBy = N'demo seed';

    FETCH NEXT FROM more_charge_cursor INTO @MoreAdmissionID, @MoreServiceID, @MoreQuantity;
END;
CLOSE more_charge_cursor;
DEALLOCATE more_charge_cursor;

DECLARE more_bill_cursor CURSOR LOCAL FAST_FORWARD FOR
    SELECT a.AdmissionID
    FROM @AdditionalCases c
    JOIN dbo.tblPatients p ON p.Email = c.PatientEmail
    JOIN dbo.tblAdmissions a ON a.PatientID = p.PatientID AND a.CreatedBy = N'demo seed' AND a.Status = N'Admitted'
    WHERE NOT EXISTS (SELECT 1 FROM dbo.tblBills b WHERE b.AdmissionID = a.AdmissionID AND b.Status <> N'Voided');

OPEN more_bill_cursor;
FETCH NEXT FROM more_bill_cursor INTO @MoreAdmissionID;
WHILE @@FETCH_STATUS = 0
BEGIN
    DELETE FROM @MoreIDs;
    INSERT INTO @MoreIDs (ID)
    EXEC dbo.usp_tblBills_Generate @AdmissionID = @MoreAdmissionID, @GeneratedBy = N'demo seed';

    FETCH NEXT FROM more_bill_cursor INTO @MoreAdmissionID;
END;
CLOSE more_bill_cursor;
DEALLOCATE more_bill_cursor;

DECLARE @MoreBillID INT;
DECLARE @MoreBalance DECIMAL(12,2);
DECLARE @MorePaymentAmount DECIMAL(12,2);

DECLARE more_payment_cursor CURSOR LOCAL FAST_FORWARD FOR
    SELECT c.CaseNo, b.BillID, b.Balance
    FROM @AdditionalCases c
    JOIN dbo.tblPatients p ON p.Email = c.PatientEmail
    JOIN dbo.tblAdmissions a ON a.PatientID = p.PatientID AND a.CreatedBy = N'demo seed'
    JOIN dbo.tblBills b ON b.AdmissionID = a.AdmissionID AND b.Status <> N'Voided'
    WHERE c.CaseNo IN (1, 3, 5)
      AND b.Balance > 0
      AND NOT EXISTS (SELECT 1 FROM dbo.tblPayments pay WHERE pay.BillID = b.BillID AND pay.ReceivedBy = N'demo seed')
    ORDER BY c.CaseNo;

OPEN more_payment_cursor;
FETCH NEXT FROM more_payment_cursor INTO @MoreCaseNo, @MoreBillID, @MoreBalance;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @MorePaymentAmount = CASE WHEN @MoreCaseNo = 3 THEN @MoreBalance ELSE ROUND(@MoreBalance / 2.0, 2) END;
    EXEC dbo.usp_tblPayments_Post
        @BillID = @MoreBillID,
        @Amount = @MorePaymentAmount,
        @Method = N'Cash',
        @ReceivedBy = N'demo seed';

    FETCH NEXT FROM more_payment_cursor INTO @MoreCaseNo, @MoreBillID, @MoreBalance;
END;
CLOSE more_payment_cursor;
DEALLOCATE more_payment_cursor;
