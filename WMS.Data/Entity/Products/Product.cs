using ERP.Core.Entities.Accounting;
using ERP.Core.Entities.Catalogs.VatCatalog;
using ERP.Core.Enums;
using ERP.Core.Services.Interfaces;

namespace WMS.Data.Entity.Products;

public class Product : BaseCatalog, IHierarchy
{
    public Product()
    {
        ReferenceType = ReferenceType.CatalogProduct;
    }

    public int OrisId { get; set; }
    public ItemType ItemType { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public string? VendorCode { get; set; }
    public Guid? MainUnitId { get; set; }
    public Unit? MainUnit { get; set; }
    public Guid? ReportingUnitId { get; set; }
    public Unit? ReportingUnit { get; set; }
    public Guid? AccountId { get; set; }
    public Account? Account { get; set; }

    public VatType VatRate { get; set; }
    public Guid? GroupId { get; set; }
    public Product? Group { get; set; }
    public string? UniqueCode { get; set; }
    public Guid? UnitForReportingId { get; set; }
    public Unit? UnitForReporting { get; set; }
    public Guid? InventoryAccountId { get; set; }
    public Account? InventoryAccount { get; set; }
    public Guid? CostAccountingAccountServiceId { get; set; }
    public Account? CostAccountingAccountService { get; set; }
    public Guid? RevenueAccountingAccountId { get; set; }
    public Account? RevenueAccountingAccount { get; set; }
    public Guid? CostAccountingAccountGoodsId { get; set; }
    public Account? CostAccountingAccountGoods { get; set; }
    public Guid? AccruedVatAccountId { get; set; }
    public Account? AccruedVatAccount { get; set; }
    public Guid? VatId { get; set; }
    public Vat? Vat { get; set; }
    public Guid? UnitId { get; set; }
    public Unit? Unit { get; set; }

    public bool Import { get; set; }
    public decimal ImportRate { get; set; }
    public bool IsGroup { get; set; }
    public Guid? ParentId { get; set; }
}