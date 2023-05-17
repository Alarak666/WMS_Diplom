using WMS.Data.Entity.Identity;
using WMS.Data.Interface;

namespace WMS.Data.Entity.BaseClass;

public class BaseCatalog : ISearchable
{
    public Guid Id { get; set; }
    public ApplicationUser? CreatedUser { get; set; }
    public Guid? CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; } = default;
    public string UniqueCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}