using System.Collections.Generic;
using Model;

namespace BusinessLogic
{
    public class DrugCategoryService
    {
        private readonly LookupService _lookupService;

        public DrugCategoryService(string connectionString)
        {
            _lookupService = new LookupService(connectionString);
        }

        public List<LookupItem> GetAll() => _lookupService.GetAll("DrugCategories");

        public LookupItem? GetById(int id) => _lookupService.GetById("DrugCategories", id);

        public List<LookupItem> Search(string searchTerm) => _lookupService.Search("DrugCategories", searchTerm);

        public int Update(LookupItem item) => _lookupService.Update("DrugCategories", item);
    }
}
