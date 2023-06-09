using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.AreaTypeServices
{
    public class AreaTypeService : IAreaTypeService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public AreaTypeService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<AreaTypeListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/AreaType", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<AreaTypeListViewModel>>(responseContent);
            return items;
        }

        public async Task<AreaTypeDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/AreaType/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<AreaTypeDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(AreaTypeDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/AreaType",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(AreaTypeDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/AreaType",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/AreaType/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
