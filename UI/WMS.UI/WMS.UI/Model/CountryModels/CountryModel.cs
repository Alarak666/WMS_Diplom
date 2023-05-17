using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.CountryModels;

public class CountryModel : BaseCatalogModel
{
    public string? Code { get; set; }
    public Guid? CurrencyId { get; set; }
}