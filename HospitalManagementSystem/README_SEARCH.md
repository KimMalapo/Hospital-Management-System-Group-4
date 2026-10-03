# Master Data Search Feature

Simple search functionality for master data (Room Types, Diagnosis Codes, Drug Categories, Department Types).

## Backend Files

### Services (BusinessLogic/)
- `LookupService.cs` - Generic master data service with Search(), GetAll(), GetById(), Update()
- `DiagnosisCodeService.cs` - Diagnosis codes search
- `DrugCategoryService.cs` - Drug categories search
- `DepartmentTypeService.cs` - Department types search
- `RoomService.cs` - Room types search

### Database (Database/)
- `SearchStoredProcedures.sql` - Four search stored procedures

## Usage

```csharp
// Search diagnosis codes
var diagService = new DiagnosisCodeService(connectionString);
var results = diagService.Search("ICD");

// Search drug categories
var drugService = new DrugCategoryService(connectionString);
var results = drugService.Search("ANTI");

// Search department types
var deptService = new DepartmentTypeService(connectionString);
var results = deptService.Search("ICU");

// Search room types
var roomService = new RoomService(connectionString);
var results = roomService.SearchRoomTypes("EME");
```

Returns `List<LookupItem>` with Id, Code, Name, Status, Notes, LastUpdated.

## Deployment

1. Execute `SearchStoredProcedures.sql` on database
2. Build solution
3. Use services in UI layer

Done!
