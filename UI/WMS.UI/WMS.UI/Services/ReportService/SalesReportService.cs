using Newtonsoft.Json;
using WMS.Core.Models.Report;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.ReportService;

public class SalesReportService
{
    private readonly HttpClientHelper _httpClientHelper;

    public SalesReportService(HttpClientHelper httpClientHelper)
    {
        _httpClientHelper = httpClientHelper;
    }
    public async Task<IEnumerable<SalesReportDto>> ReportData()
    {
        var response = await _httpClientHelper.Get($"api/Dashboard/sales-report", CancellationToken.None);
        var responseContent = await response.Content.ReadAsStringAsync(CancellationToken.None);
        var items = JsonConvert.DeserializeObject<List<SalesReportDto>>(responseContent);
        return items;
    }
}