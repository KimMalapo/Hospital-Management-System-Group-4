-- Run this BEFORE deploying the unique index on an existing database. Not part of the DB project build.
-- It lists active patients that share last name + first name + date of birth.
SELECT LastName, FirstName, DateOfBirth, COUNT(*) AS Copies,
       STRING_AGG(CAST(PatientID AS NVARCHAR(20)), N', ') AS PatientIDs
FROM   dbo.tblPatients
WHERE  IsActive = 1
GROUP BY LastName, FirstName, DateOfBirth
HAVING COUNT(*) > 1;
-- Fix: keep one row per group and deactivate the others, for example
-- UPDATE dbo.tblPatients SET IsActive = 0 WHERE PatientID IN (<ids to remove>);
