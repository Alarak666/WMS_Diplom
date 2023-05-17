namespace WMS.UI.Model.BaseClassModels;

public class BaseCatalogModel 
{
    public Guid Id { get; set; }
    public Guid? CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; } = default;
    public string? UniqueCode { get; set; }
    public string Name { get; set; } = string.Empty;
}