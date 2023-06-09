using WMS.Data.Interface;

namespace WMS.Data.DTO.BaseClassDtos;

public class BaseCatalogDto : ISearchable
{
    public Guid Id { get; set; }
    public Guid? CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; } = default;
    public string? UniqueCode { get; set; }
    public string? Name { get; set; } = string.Empty;
}