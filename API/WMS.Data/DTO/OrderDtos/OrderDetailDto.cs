using WMS.Data.DTO.BaseClassDtos;

namespace WMS.Data.DTO.OrderDtos;

public class OrderDetailDto 
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid? ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}