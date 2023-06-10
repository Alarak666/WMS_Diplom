using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Employees;
using WMS.Data.Entity.VendorCustomers;

namespace WMS.Data.Entity.Orders;
public class Order : BaseCatalog
{
    public Employee? Employee { get; set; }
    public Guid? EmployeeId { get; set; }
    public Guid? VendorCustomerId { get; set; }
    public VendorCustomer? VendorCustomer { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string? OrderStatus { get; set; }
    public DateTime DateOrders { get; set; }
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}
