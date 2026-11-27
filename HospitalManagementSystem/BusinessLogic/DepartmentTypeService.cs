using System.Collections.Generic;
using Model;

namespace BusinessLogic
{
    /// <summary>Hospital departments (Emergency, Surgery...). Just uses LookupService.</summary>
    public class DepartmentTypeService
    {
        private readonly LookupService _lookupService;

        public DepartmentTypeService(string connectionString)
        {
            _lookupService = new LookupService(connectionString);
        }

        public List<LookupItem> GetAll() => _lookupService.GetAll("DepartmentTypes");

        public LookupItem? GetById(int id) => _lookupService.GetById("DepartmentTypes", id);

        public List<LookupItem> Search(string searchTerm) => _lookupService.Search("DepartmentTypes", searchTerm);

        public int Update(LookupItem item) => _lookupService.Update("DepartmentTypes", item);
    }
}
