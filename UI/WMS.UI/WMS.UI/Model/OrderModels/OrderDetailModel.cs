using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.OrderModels;

public class OrderDetailModel : BaseCatalogModel
{
    public Guid? ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}