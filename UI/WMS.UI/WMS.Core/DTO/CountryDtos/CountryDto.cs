using WMS.Core.DTO.BaseClassDtos;

namespace WMS.Core.DTO.CountryDtos;

public class CountryDto : BaseCatalogDto
{
    public string? Code { get; set; }
    public Guid? CurrencyId { get; set; }
}