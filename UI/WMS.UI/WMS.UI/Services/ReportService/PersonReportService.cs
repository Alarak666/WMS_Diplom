using Newtonsoft.Json;
using WMS.Core.Models.Report;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.ReportService;

public class PersonReportService
{
    private readonly HttpClientHelper _httpClientHelper;

    public PersonReportService(HttpClientHelper httpClientHelper)
    {
        _httpClientHelper = httpClientHelper;
    }
    public async Task<IEnumerable<PersonReportDto>> ReportData()
    {
        var response = await _httpClientHelper.Get($"api/Dashboard/person-report", CancellationToken.None);
        var responseContent = await response.Content.ReadAsStringAsync(CancellationToken.None);
        var items = JsonConvert.DeserializeObject<List<PersonReportDto>>(responseContent);
        return items;
    }
}