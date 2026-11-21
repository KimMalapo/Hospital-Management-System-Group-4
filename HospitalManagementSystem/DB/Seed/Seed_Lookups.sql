-- Lookup data (re-runnable: each row is inserted only when its code is missing)
SET NOCOUNT ON;

-- Diagnosis codes
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'A00' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'A00', N'Cholera', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'J18' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'J18', N'Pneumonia, unspecified', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'I10' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'I10', N'Essential hypertension', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'E11' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'E11', N'Type 2 diabetes mellitus', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'K35' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'K35', N'Acute appendicitis', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'S72' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'S72', N'Fracture of femur', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'O80' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'O80', N'Normal delivery', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'J45' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'J45', N'Asthma', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'N39' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'N39', N'Urinary tract infection', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'R50' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'R50', N'Fever of unknown origin', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = N'M54' AND IsDeleted = 0)
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status) VALUES (N'M54', N'Back pain', N'Active');

-- Drug categories
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'ANTIB' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'ANTIB', N'Antibiotics', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'ANALG' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'ANALG', N'Analgesics', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'CARDIO' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'CARDIO', N'Cardiovascular', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'ANTID' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'ANTID', N'Antidiabetics', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'VITAM' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'VITAM', N'Vitamins and supplements', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = N'IVFLU' AND IsDeleted = 0)
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status) VALUES (N'IVFLU', N'IV fluids', N'Active');

-- Room types
IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = N'GEN' AND IsDeleted = 0)
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status) VALUES (N'GEN', N'General Ward', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = N'ICU' AND IsDeleted = 0)
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status) VALUES (N'ICU', N'Intensive Care', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = N'PRV' AND IsDeleted = 0)
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status) VALUES (N'PRV', N'Private Room', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = N'SEMI' AND IsDeleted = 0)
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status) VALUES (N'SEMI', N'Semi-private Room', N'Active');

-- Departments
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'EMR' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'EMR', N'Emergency', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'MED' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'MED', N'Internal Medicine', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'SURG' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'SURG', N'Surgery', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'PED' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'PED', N'Pediatrics', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'OBG' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'OBG', N'Obstetrics and Gynecology', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'CAR' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'CAR', N'Cardiology', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'NEU' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'NEU', N'Neurology', N'Active');
IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = N'ORT' AND IsDeleted = 0)
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status) VALUES (N'ORT', N'Orthopedics', N'Active');

-- Rooms (rate is per day, in pesos)
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'101')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'101', N'GEN', 800, 4);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'102')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'102', N'GEN', 800, 4);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'103')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'103', N'GEN', 800, 4);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'201')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'201', N'SEMI', 1500, 2);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'202')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'202', N'SEMI', 1500, 2);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'301')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'301', N'PRV', 3000, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'302')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'302', N'PRV', 3000, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'303')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'303', N'PRV', 3200, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'ICU-A')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'ICU-A', N'ICU', 6000, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = N'ICU-B')
    INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity) VALUES (N'ICU-B', N'ICU', 6000, 1);

DECLARE @WardRoom INT = 104;
WHILE @WardRoom <= 115
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = CONVERT(NVARCHAR(20), @WardRoom))
        INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity)
        VALUES (CONVERT(NVARCHAR(20), @WardRoom), N'GEN', 800, 4);
    SET @WardRoom += 1;
END;

DECLARE @AdditionalRoom INT = 304;
WHILE @AdditionalRoom <= 307
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.tblRooms WHERE RoomNo = CONVERT(NVARCHAR(20), @AdditionalRoom))
        INSERT INTO dbo.tblRooms (RoomNo, RoomType, Rate, Capacity)
        VALUES (CONVERT(NVARCHAR(20), @AdditionalRoom), N'GEN', 800, 4);
    SET @AdditionalRoom += 1;
END;

UPDATE dbo.tblRooms
SET Capacity = CASE
    WHEN RoomNo BETWEEN N'101' AND N'115' THEN 4
    WHEN RoomNo IN (N'201', N'202') THEN 2
    WHEN RoomNo BETWEEN N'301' AND N'303' OR RoomNo IN (N'ICU-A', N'ICU-B') THEN 1
    WHEN RoomNo BETWEEN N'304' AND N'307' THEN 4
END
WHERE RoomNo BETWEEN N'101' AND N'115'
   OR RoomNo IN (N'201', N'202', N'ICU-A', N'ICU-B')
   OR RoomNo BETWEEN N'301' AND N'307';
