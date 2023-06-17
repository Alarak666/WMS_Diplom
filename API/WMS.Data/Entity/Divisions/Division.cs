using WMS.Data.Entity.BaseClass;

namespace WMS.Data.Entity.Divisions;

public class Division : BaseCatalog
{
    public Division? ParentDivision { get; set; }
    public Guid? ParentDivisionId { get; set; }
}