using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.PlaceParameterServices
{
    public class PlaceParameterService : IPlaceParameterService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public PlaceParameterService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<PlaceParameterListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/PlaceParameter", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<PlaceParameterListViewModel>>(responseContent);
            return items;
        }

        public async Task<PlaceParameterDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/PlaceParameter/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<PlaceParameterDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(PlaceParameterDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/PlaceParameter",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(PlaceParameterDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/PlaceParameter",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/PlaceParameter/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
