using WMS.Data.Entity.BaseClass;

namespace WMS.Data.Entity.Divisions;

public class Division : BaseCatalog
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Division? ParentDivision { get; set; }
    public Guid? ParentDivisionId { get; set; }
}