using WMS.Data.Entity.BaseClass;

namespace WMS.Data.Entity.Currencies;

public class Currency : BaseCatalog
{
    public string? SymbolCode { get; set; }
    public int CurrencyCode { get; set; }
}