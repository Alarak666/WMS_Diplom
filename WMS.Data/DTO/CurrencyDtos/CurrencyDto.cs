using WMS.Data.DTO.BaseClassDtos;

namespace WMS.Data.DTO.CurrencyDtos;

public class CurrencyDto : BaseCatalogDto
{
    public string? SymbolCode { get; set; }
    public int CurrencyCode { get; set; }
}