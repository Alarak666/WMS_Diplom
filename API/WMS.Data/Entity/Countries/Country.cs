using WMS.Data.Entity.BaseClass;
using WMS.Data.Entity.Currencies;

namespace WMS.Data.Entity.Countries;

public class Country : BaseCatalog
{
    public string? Code { get; set; }
    public Currency? Currency { get; set; }
    public Guid? CurrencyId { get; set; }
}