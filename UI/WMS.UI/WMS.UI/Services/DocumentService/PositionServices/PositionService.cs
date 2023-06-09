using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Positions;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.PositionServices
{
    public class PositionService : IPositionService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public PositionService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<PositionListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Position", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<PositionListViewModel>>(responseContent);
            return items;
        }

        public async Task<PositionDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Position/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<PositionDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(PositionDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/Position",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(PositionDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/Position",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/Position/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
