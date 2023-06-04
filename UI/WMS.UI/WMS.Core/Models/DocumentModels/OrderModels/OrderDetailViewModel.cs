namespace WMS.Core.Models.DocumentModels.OrderModels;
public class OrderDetailViewModel
{
    public Guid Id { get; set; }
    public Guid? CreatedUserId { get; set; }
    public DateTime CreatedDate { get; set; } = default;
    public string? UniqueCode { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? EmployeeId { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public string? OrderStatus { get; set; }
    public DateTime DateOrders { get; set; }
    public virtual ICollection<OrderDetailModel>? OrderDetails { get; set; }
}
