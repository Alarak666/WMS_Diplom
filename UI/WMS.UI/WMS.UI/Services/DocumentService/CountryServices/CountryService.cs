using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.CountryModels;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.CountryServices
{
    public class CountryService : ICountryService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public CountryService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<CountryListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Country", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<CountryListViewModel>>(responseContent);
            return items;
        }

        public async Task<CountryDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Country/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<CountryDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(CountryDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/Country",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(CountryDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/Country",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/Country/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
