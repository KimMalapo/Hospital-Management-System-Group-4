-- Post-deployment script: runs after every publish, so every statement must be re-runnable.
-- Seeds example master data only when missing.

IF NOT EXISTS (SELECT 1 FROM dbo.tblDiagnosisCodes WHERE Code = 'A00')
    INSERT INTO dbo.tblDiagnosisCodes (Code, Description, Status, Notes) VALUES ('A00', 'Cholera', 'Active', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.tblDrugCategories WHERE Code = 'ANTIB')
    INSERT INTO dbo.tblDrugCategories (Code, Name, Status, Notes) VALUES ('ANTIB', 'Antibiotics', 'Active', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.tblRoomTypes WHERE Code = 'GEN')
    INSERT INTO dbo.tblRoomTypes (Code, Name, Status, Notes) VALUES ('GEN', 'General Ward', 'Active', NULL);

IF NOT EXISTS (SELECT 1 FROM dbo.tblDepartmentTypes WHERE Code = 'EMR')
    INSERT INTO dbo.tblDepartmentTypes (Code, Name, Status, Notes) VALUES ('EMR', 'Emergency', 'Active', NULL);
