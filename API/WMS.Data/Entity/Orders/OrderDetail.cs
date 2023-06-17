using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Products;

namespace WMS.Data.Entity.Orders;

public class OrderDetail : BaseCatalog
{
    public Guid OrderId { get; set; }
    public Product? Product { get; set; }
    public Guid? ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}