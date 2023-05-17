using WMS.Data.DTO.BaseClassDtos;

namespace WMS.Data.DTO.CountryDtos;

public class CountryDto : BaseCatalogDto
{
    public string? Code { get; set; }
    public Guid? CurrencyId { get; set; }
}