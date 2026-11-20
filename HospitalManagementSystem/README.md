# Hospital Management System

## Automated tests

The automated test project covers password validation and lookup-service behavior without a database, plus SQL-script and full patient-to-discharge integration tests.

### Prerequisites

- .NET 10 SDK.
- SQL Server LocalDB (`MSSQLLocalDB`) on Windows to run the SQL-script and business-cycle integration tests.

### Run the tests

From the directory containing `HospitalManagementSystem.slnx`, run:

```powershell
dotnet test .\HospitalManagementSystem.slnx
```

The unit tests do not require SQL Server. When LocalDB is unavailable, the integration tests are reported as skipped; install and start the LocalDB runtime to execute them. The integration fixture creates a uniquely named `HMS_Test_*` database, applies the database project scripts and post-deployment seed, and drops the database after the tests. If the database project is outside the solution directory, set `HMS_DB_FOLDER` to its full path before running the tests.
