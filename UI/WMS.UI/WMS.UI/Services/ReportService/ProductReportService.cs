using Newtonsoft.Json;
using WMS.Core.Models.Report;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.ReportService;

public class ProductReportService
{
    private readonly HttpClientHelper _httpClientHelper;

    public ProductReportService(HttpClientHelper httpClientHelper)
    {
        _httpClientHelper = httpClientHelper;
    }
    public async Task<IEnumerable<ProductReportDto>> ReportData()
    {
        var response = await _httpClientHelper.Get($"api/Dashboard/product-report", CancellationToken.None);
        var responseContent = await response.Content.ReadAsStringAsync(CancellationToken.None);
        var items = JsonConvert.DeserializeObject<List<ProductReportDto>>(responseContent);
        return items;
    }
}