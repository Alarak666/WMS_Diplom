using WMS.Data.Constant.Enum;
using WMS.Data.DTO.BaseClassDtos;

namespace WMS.Data.DTO.OrderDtos;
public class OrderDto : BaseCatalogDto
{
    public Guid? EmployeeId { get; set; }
    public Guid? VendorCustomerId { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime DateOrders { get; set; }
    public virtual ICollection<OrderDetailDto>? OrderDetails { get; set; }
}
