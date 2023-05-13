using ERP.Core.Enums;

namespace WMS.Data.Entity.Products;

public class Unit : BaseCatalog
{
    public Unit()
    {
        ReferenceType = ReferenceType.CatalogUnit;
    }

    public int OrisId { get; set; }
    public int RsUnitId { get; set; }
}