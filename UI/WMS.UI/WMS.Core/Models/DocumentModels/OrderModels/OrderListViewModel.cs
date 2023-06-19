﻿using WMS.Core.Constants.Enum;

namespace WMS.Core.Models.DocumentModels.OrderModels;
public class OrderListViewModel 
{
    public Guid Id { get; set; }
    public Guid? CreatedUserId { get; set; }
    public string? CreatedUserName { get; set; }
    public Guid? VendorCustomerId { get; set; }
    public string? VendorCustomerName { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? UniqueCode { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? EmployeeId { get; set; }
    public string? ShippingAddress { get; set; }
    public string? PaymentMethod { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public DateTime DateOrders { get; set; } = DateTime.UtcNow;
    public virtual ICollection<OrderDetailModel>? OrderDetails { get; set; }
}
