using Newtonsoft.Json;
using WMS.Core.Interface;
using WMS.Core.Models.Dashboards;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DashBoardService;

public class DataProviderAccessArea : IDashboardProvider
{
    private readonly HttpClientHelper _httpClientHelper;

    public DataProviderAccessArea(HttpClientHelper httpClientHelper)
    {
        _httpClientHelper = httpClientHelper;
    }

    public async Task<IEnumerable<DashBoardPalletDto>> GetSalesAsync(CancellationToken ct = default)
    {
        var response = await _httpClientHelper.Get($"api/Dashboard", ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);
        var items = JsonConvert.DeserializeObject<List<DashBoardPalletDto>>(responseContent);
        return items;
    }
}