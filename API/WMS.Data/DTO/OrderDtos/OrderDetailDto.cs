using WMS.Data.DTO.BaseClassDtos;

namespace WMS.Data.DTO.OrderDtos;

public class OrderDetailDto : BaseCatalogDto
{
    public Guid? ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}