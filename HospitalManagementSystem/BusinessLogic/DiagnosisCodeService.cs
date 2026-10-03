using System.Collections.Generic;
using Model;

namespace BusinessLogic
{
    public class DiagnosisCodeService
    {
        private readonly LookupService _lookupService;

        public DiagnosisCodeService(string connectionString)
        {
            _lookupService = new LookupService(connectionString);
        }

        public List<LookupItem> GetAll() => _lookupService.GetAll("DiagnosisCodes");

        public LookupItem? GetById(int id) => _lookupService.GetById("DiagnosisCodes", id);

        public List<LookupItem> Search(string searchTerm) => _lookupService.Search("DiagnosisCodes", searchTerm);

        public int Update(LookupItem item) => _lookupService.Update("DiagnosisCodes", item);
    }
}
