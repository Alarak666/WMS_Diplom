namespace WMS.Core.DemoTemplate;

public interface ISalesInfoDataProvider
{
    Task<IEnumerable<SaleInfo>> GetSalesAsync(CancellationToken ct = default);
    Task<IEnumerable<SaleInfo>> GetReducedSalesAsync(CancellationToken ct = default);
}