using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Employees;

namespace WMS.Data.Entity.Orders;
public class Order : BaseCatalog
{
    public Employee? Employee { get; set; }
    public Guid? EmployeeId { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string? OrderStatus { get; set; }
    public DateTime DateOrders { get; set; }
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}
