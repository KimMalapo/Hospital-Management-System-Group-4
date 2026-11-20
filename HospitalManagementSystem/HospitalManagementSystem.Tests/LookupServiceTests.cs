using BusinessLogic;
using Model;

namespace HospitalManagementSystem.Tests;

public class LookupServiceTests
{
    private static readonly LookupItem ExistingItem = new()
    {
        Id = 7,
        Code = "EMR",
        Name = "Emergency",
        Status = "Active"
    };

    private readonly StubLookupRepository _repository = new();
    private LookupService CreateService() => new(_repository);

    [Fact]
    public void GetAll_uses_the_normalized_lookup_type()
    {
        _repository.Items.Add(ExistingItem);

        var result = CreateService().GetAll("RoomTypes");

        Assert.Same(_repository.Items, result);
        Assert.Equal("tblRoomTypes", _repository.LastTable);
    }

    [Fact]
    public void GetById_uses_the_requested_id_and_normalized_type()
    {
        _repository.Items.Add(ExistingItem);

        var result = CreateService().GetById("tblDepartmentTypes", ExistingItem.Id);

        Assert.Same(ExistingItem, result);
        Assert.Equal("tblDepartmentTypes", _repository.LastTable);
        Assert.Equal(ExistingItem.Id, _repository.LastId);
    }

    [Fact]
    public void Search_trims_the_term_and_returns_repository_results()
    {
        _repository.SearchResults.Add(ExistingItem);

        var result = CreateService().Search("DiagnosisCodes", "  EMR  ");

        Assert.Same(_repository.SearchResults, result);
        Assert.Equal("tblDiagnosisCodes", _repository.LastTable);
        Assert.Equal("EMR", _repository.LastSearchTerm);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Empty_search_is_forwarded_as_an_empty_term(string? term)
    {
        CreateService().Search("DrugCategories", term!);

        Assert.Equal("tblDrugCategories", _repository.LastTable);
        Assert.Equal(string.Empty, _repository.LastSearchTerm);
    }

    [Fact]
    public void Invalid_lookup_type_is_rejected_before_repository_access()
    {
        var ex = Assert.Throws<ArgumentException>(() => CreateService().GetAll("Users"));

        Assert.Contains("Unsupported lookup type", ex.Message);
        Assert.Null(_repository.LastTable);
    }

    [Fact]
    public void Update_uses_existing_item_and_writes_the_update()
    {
        _repository.Items.Add(ExistingItem);
        var updated = new LookupItem
        {
            Id = ExistingItem.Id,
            Code = ExistingItem.Code,
            Name = "Emergency Care",
            Status = "Active"
        };

        var rows = CreateService().Update("DepartmentTypes", updated);

        Assert.Equal(1, rows);
        Assert.Equal("tblDepartmentTypes", _repository.LastTable);
        Assert.Same(updated, _repository.UpdatedItem);
        Assert.Equal(1, _repository.UpdateCalls);
    }

    [Fact]
    public void Update_rejects_missing_record_without_writing()
    {
        var item = new LookupItem { Id = 1, Name = "Emergency", Status = "Active" };

        Assert.Throws<ArgumentException>(() => CreateService().Update("DepartmentTypes", item));
        Assert.Equal(0, _repository.UpdateCalls);
    }

    [Fact]
    public void Update_rejects_code_changes_without_writing()
    {
        _repository.Items.Add(ExistingItem);
        var item = new LookupItem
        {
            Id = ExistingItem.Id,
            Code = "NEW",
            Name = "Emergency",
            Status = "Active"
        };

        Assert.Throws<InvalidOperationException>(() => CreateService().Update("DepartmentTypes", item));
        Assert.Equal(0, _repository.UpdateCalls);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_requires_a_code(string? code)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            CreateService().Create("RoomTypes", new LookupItem { Code = code, Name = "Ward" }));
        Assert.Contains("Code", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_requires_a_name(string? name)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            CreateService().Create("RoomTypes", new LookupItem { Code = "WRD", Name = name }));
        Assert.Contains("Name", ex.Message);
    }

    [Fact]
    public void Update_requires_a_name_and_a_status()
    {
        var noName = Assert.Throws<ArgumentException>(() =>
            CreateService().Update("DrugCategories", new LookupItem { Id = 1, Name = "", Status = "Active" }));
        Assert.Contains("Name", noName.Message);

        var noStatus = Assert.Throws<ArgumentException>(() =>
            CreateService().Update("DrugCategories", new LookupItem { Id = 1, Name = "Antibiotics", Status = "" }));
        Assert.Contains("Status", noStatus.Message);
    }

    private sealed class StubLookupRepository : ILookupRepository
    {
        public List<LookupItem> Items { get; } = new();
        public List<LookupItem> SearchResults { get; } = new();
        public string? LastTable { get; private set; }
        public string? LastSearchTerm { get; private set; }
        public int? LastId { get; private set; }
        public int UpdateCalls { get; private set; }
        public LookupItem? UpdatedItem { get; private set; }

        public List<LookupItem> GetList(string table)
        {
            LastTable = table;
            return Items;
        }

        public LookupItem? GetById(string table, int id)
        {
            LastTable = table;
            LastId = id;
            return Items.Find(item => item.Id == id);
        }

        public bool CodeExists(string table, string code) => false;
        public int Create(string table, LookupItem item) => 1;

        public int Update(string table, LookupItem item)
        {
            LastTable = table;
            UpdatedItem = item;
            UpdateCalls++;
            return 1;
        }

        public int SoftDelete(string table, int id) => 1;
        public bool IsCodeInUse(string table, string? code) => false;

        public List<LookupItem> Search(string table, string term)
        {
            LastTable = table;
            LastSearchTerm = term;
            return SearchResults;
        }
    }
}
