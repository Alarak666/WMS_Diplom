using WMS.Core.DTO.BaseClassDtos;

namespace WMS.Core.DTO.CurrencyDtos;

public class CurrencyDto : BaseCatalogDto
{
    public string? SymbolCode { get; set; }
    public int CurrencyCode { get; set; }
}