﻿using WMS.Core.Constants.Enum;
using WMS.Core.DTO.BaseClassDtos;

namespace WMS.Core.DTO.ProductDtos;

public class ProductDto : BaseCatalogDto
{
    public ItemType ItemType { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public string? VendorCode { get; set; }
    public Guid? MainUnitId { get; set; }
    public VatType VatRate { get; set; }
    public Guid? GroupId { get; set; }
    public Guid? UnitId { get; set; }
    public bool Import { get; set; }
    public decimal? ImportPrice { get; set; }
    public decimal? Price { get; set; }
}
