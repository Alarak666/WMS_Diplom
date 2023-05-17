using WMS.Data.Constant.Enum;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Units;

namespace WMS.Data.Entity.Products;

public class Product : BaseCatalog
{
    public ItemType ItemType { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public string? VendorCode { get; set; }
    public Guid? MainUnitId { get; set; }
    public Unit? MainUnit { get; set; }
    public VatType VatRate { get; set; }
    public Guid? GroupId { get; set; }
    public Product? Group { get; set; }
    public Guid? UnitId { get; set; }
    public Unit? Unit { get; set; }
    public bool Import { get; set; }
    public decimal? ImportPrice { get; set; }
    public decimal? Price { get; set; }
}
