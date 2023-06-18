﻿using WMS.Data.Constant.Enum;
using WMS.Data.DTO.OrderDtos;

namespace WMS.Data.ReportDto
{
    public class SalesReportDto
    {
        public Guid? EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public Guid? VendorCustomerId { get; set; }
        public string VendorCustomerName { get; set; } = string.Empty;
        public string? ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public DateTime DateOrders { get; set; }
        public virtual IEnumerable<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
    }
}
