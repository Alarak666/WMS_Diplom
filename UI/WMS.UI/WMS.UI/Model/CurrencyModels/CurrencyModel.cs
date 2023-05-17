using WMS.UI.Model.BaseClassModels;

namespace WMS.UI.Model.CurrencyModels;

public class CurrencyModel : BaseCatalogModel
{
    public string? SymbolCode { get; set; }
    public int CurrencyCode { get; set; }
}