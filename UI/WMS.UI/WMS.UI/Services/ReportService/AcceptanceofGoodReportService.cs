using Newtonsoft.Json;
using WMS.Core.Models.Report;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.ReportService;

public class AcceptanceofGoodReportService
{
    private readonly HttpClientHelper _httpClientHelper;

    public AcceptanceofGoodReportService(HttpClientHelper httpClientHelper)
    {
        _httpClientHelper = httpClientHelper;
    }
    public async Task<IEnumerable<AcceptanceofGoodReportDto>> ReportData()
    {
        var response = await _httpClientHelper.Get($"api/Dashboard/acceptance-of-good-report", CancellationToken.None);
        var responseContent = await response.Content.ReadAsStringAsync(CancellationToken.None);
        var items = JsonConvert.DeserializeObject<List<AcceptanceofGoodReportDto>>(responseContent);
        return items;
    }
}