using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.RegionServices
{
    public class RegionService : IRegionService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public RegionService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<RegionListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Region", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<RegionListViewModel>>(responseContent);
            return items;
        }

        public async Task<RegionDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Region/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<RegionDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(RegionDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/Region",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(RegionDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/Region",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/Region/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
