-- Service catalog (re-runnable: a service is inserted only when its code is missing)

IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'CONS-GEN' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'CONS-GEN', N'General consultation', N'Consultation', NULL, 500);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'CONS-SPEC' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'CONS-SPEC', N'Specialist consultation', N'Consultation', NULL, 1200);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'LAB-CBC' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'LAB-CBC', N'Complete blood count', N'Laboratory', NULL, 350);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'LAB-URI' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'LAB-URI', N'Urinalysis', N'Laboratory', NULL, 150);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'LAB-GLU' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'LAB-GLU', N'Blood glucose', N'Laboratory', NULL, 120);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'IMG-XRAY' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'IMG-XRAY', N'Chest X-ray', N'Imaging', NULL, 600);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'IMG-US' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'IMG-US', N'Ultrasound', N'Imaging', NULL, 1200);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'IMG-CT' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'IMG-CT', N'CT scan', N'Imaging', NULL, 5500);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'PRC-SUT' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'PRC-SUT', N'Wound suturing', N'Procedure', NULL, 1500);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'PRC-APP' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'PRC-APP', N'Appendectomy', N'Procedure', NULL, 35000);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'MED-AMOX' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'MED-AMOX', N'Amoxicillin 500 mg capsule', N'Medication', N'ANTIB', 15);
IF NOT EXISTS (SELECT 1 FROM dbo.tblServices WHERE Code = N'MED-PARA' AND IsActive = 1)
    INSERT INTO dbo.tblServices (Code, Name, Category, DrugCategoryCode, UnitPrice)
    VALUES (N'MED-PARA', N'Paracetamol 500 mg tablet', N'Medication', N'ANALG', 5);
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
