﻿using WMS.Core.Constants.Enum;

namespace WMS.Core.Models.DocumentModels.Products
{
    public class ProductListViewModel
    {
        public Guid Id { get; set; }
        public string? CreatedUserName { get; set; }
        public Guid? CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? UniqueCode { get; set; }
        public ItemType ItemType { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public Guid? CompanyId { get; set; }
        public string? Barcode { get; set; }
        public string? VendorCode { get; set; }
        public string? MainUnitName { get; set; }
        public Guid? MainUnitId { get; set; }
    }
}
