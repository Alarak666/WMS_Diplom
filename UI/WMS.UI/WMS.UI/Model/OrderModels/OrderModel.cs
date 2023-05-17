using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.OrderModels;
public class OrderModel : BaseCatalogModel
{
    public Guid? EmployeeId { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string? OrderStatus { get; set; }
    public DateTime DateOrders { get; set; }
    public virtual ICollection<OrderDetailModel>? OrderDetails { get; set; }
}
