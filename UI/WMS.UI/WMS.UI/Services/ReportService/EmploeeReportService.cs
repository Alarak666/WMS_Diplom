using Newtonsoft.Json;
using WMS.Core.Models.Report;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.ReportService;

public class EmploeeReportService
{
    private readonly HttpClientHelper _httpClientHelper;

    public EmploeeReportService(HttpClientHelper httpClientHelper)
    {
        _httpClientHelper = httpClientHelper;
    }
    public async Task<IEnumerable<EmployeeReportDto>> ReportData()
    {
        var response = await _httpClientHelper.Get($"api/Dashboard/employee-report", CancellationToken.None);
        var responseContent = await response.Content.ReadAsStringAsync(CancellationToken.None);
        var items = JsonConvert.DeserializeObject<List<EmployeeReportDto>>(responseContent);
        return items;
    }
}