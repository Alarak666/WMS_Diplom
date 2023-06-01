namespace WMS.Core.DemoTemplate;

public class DataProviderAccessArea : ISalesInfoDataProvider
{
    // Ваш код для доступа к провайдеру данных
    // Пример метода для получения данных из провайдера
    public async Task<IEnumerable<SaleInfo>> GetSalesAsync(CancellationToken ct = default)
    {
        var sales = new List<SaleInfo>
        {
            new SaleInfo
            {
                OrderId = 1,
                Region = "Region 1",
                Country = "Country 1",
                City = "City 1",
                Amount = 100,
                Date = DateTime.Now.AddDays(-1)
            },
            new SaleInfo
            {
                OrderId = 2,
                Region = "Region 2",
                Country = "Country 2",
                City = "City 2",
                Amount = 200,
                Date = DateTime.Now.AddDays(-2)
            }
        };

        // Имитация асинхронной задержки
        await Task.Delay(1000, ct);

        return sales;
    }

    public async Task<IEnumerable<SaleInfo>> GetReducedSalesAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    // Другие методы и свойства, специфичные для доступа к провайдеру данных
}