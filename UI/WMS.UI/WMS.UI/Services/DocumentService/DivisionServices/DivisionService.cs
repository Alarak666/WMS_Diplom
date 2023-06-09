using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Divisions;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.DivisionServices
{
    public class DivisionService : IDivisionService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public DivisionService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<DivisionListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Division", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<DivisionListViewModel>>(responseContent);
            return items;
        }

        public async Task<DivisionDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Division/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<DivisionDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(DivisionDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/Division",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(DivisionDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/Division",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/Division/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
