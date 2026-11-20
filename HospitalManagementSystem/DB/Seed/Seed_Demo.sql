-- Demo data for the defense. Every demo patient has an e-mail ending in @demo.hms.
-- Re-runnable: the whole block is skipped when demo patients already exist.
-- It is built with the real stored procedures, so it proves the full cycle works.
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

-- Ensure the lookup, room, and service records required by the demo admissions exist.
IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = N'GEN' AND IsDeleted = 0)
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status) VALUES (N'GEN', N'General Ward', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = N'ICU' AND IsDeleted = 0)
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status) VALUES (N'ICU', N'Intensive Care', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = N'PRV' AND IsDeleted = 0)
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status) VALUES (N'PRV', N'Private Room', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = N'SEMI' AND IsDeleted = 0)
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status) VALUES (N'SEMI', N'Semi-private Room', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'EMR' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'EMR', N'Emergency', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'MED' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'MED', N'Internal Medicine', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'PED' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'PED', N'Pediatrics', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'SURG' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'SURG', N'Surgery', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'OBG' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'OBG', N'Obstetrics and Gynecology', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'A00' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'A00', N'Cholera', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'J18' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'J18', N'Pneumonia, unspecified', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'I10' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'I10', N'Essential hypertension', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'K35' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'K35', N'Acute appendicitis', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'E11' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'E11', N'Type 2 diabetes mellitus', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'S72' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'S72', N'Fracture of femur', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'O80' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'O80', N'Normal delivery', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'ANTIB' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'ANTIB', N'Antibiotics', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'CARDIO' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'CARDIO', N'Cardiovascular', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'ANTID' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'ANTID', N'Antidiabetics', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'VITAM' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'VITAM', N'Vitamins and supplements', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'IVFLU' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'IVFLU', N'IV fluids', N'Active');

IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'301')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'301', N'PRV', 3000);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'302')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'302', N'PRV', 3000);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'303')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'303', N'PRV', 3200);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'304')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'304', N'GEN', 800);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'305')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'305', N'GEN', 800);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'306')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'306', N'GEN', 800);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'307')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'307', N'GEN', 800);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'ICU-A')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'ICU-A', N'ICU', 6000);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'ICU-B')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'ICU-B', N'ICU', 6000);

IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'101')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'101', N'GEN', 800);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'102')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'102', N'GEN', 800);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'103')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'103', N'GEN', 800);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'201')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'201', N'SEMI', 1500);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'202')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate) VALUES (N'202', N'SEMI', 1500);

IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'CONS-SPEC' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'CONS-SPEC', N'Specialist consultation', N'Consultation', NULL, 1200);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'LAB-CBC' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'LAB-CBC', N'Complete blood count', N'Laboratory', NULL, 350);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'MED-PARA' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'MED-PARA', N'Paracetamol 500 mg tablet', N'Medication', N'ANALG', 5);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'PRC-APP' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'PRC-APP', N'Appendectomy', N'Procedure', NULL, 35000);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'IMG-US' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'IMG-US', N'Ultrasound', N'Imaging', NULL, 1200);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'LAB-URI' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'LAB-URI', N'Urinalysis', N'Laboratory', NULL, 150);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'LAB-GLU' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'LAB-GLU', N'Blood glucose', N'Laboratory', NULL, 120);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'IMG-XRAY' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'IMG-XRAY', N'Chest X-ray', N'Imaging', NULL, 600);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'PRC-SUT' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'PRC-SUT', N'Wound suturing', N'Procedure', NULL, 1500);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'MED-AMOX' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'MED-AMOX', N'Amoxicillin 500 mg capsule', N'Medication', N'ANTIB', 15);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'MED-IVF' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'MED-IVF', N'IV fluid 1 L', N'Medication', N'IVFLU', 120);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'SUP-GLOVE' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'SUP-GLOVE', N'Examination gloves (pair)', N'Supply', NULL, 20);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'SUP-DRSG' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'SUP-DRSG', N'Wound dressing set', N'Supply', NULL, 250);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'OTH-ERF' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'OTH-ERF', N'Emergency room fee', N'Other', NULL, 1000);

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
    (7, N'ramon.bautista@demo.hms', N'307', N'MED', N'E11', N'Dr. Marco Ramos', N'Routine glucose monitoring', N'LAB-GLU', N'CONS-SPEC');

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
       AND EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = @MoreRoomNo AND Status = N'Available' AND IsActive = 1)
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
