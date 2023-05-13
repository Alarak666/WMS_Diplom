using WMS.Data.DTO.BaseClassDtos;

namespace WMS.Data.DTO.OrderDtos;
public class OrderDto : BaseCatalogDto
{
    public Guid? EmployeeId { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string? OrderStatus { get; set; }
    public DateTime DateOrders { get; set; }
    public virtual ICollection<OrderDetailDto>? OrderDetails { get; set; }
}
