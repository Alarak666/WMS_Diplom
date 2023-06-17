namespace WMS.Core.Models.DocumentModels.OrderModels;

public class OrderDetailModel
{
    public Guid Id { get; set; }
    public Guid OrderModelId { get; set; }
    public Guid? ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}