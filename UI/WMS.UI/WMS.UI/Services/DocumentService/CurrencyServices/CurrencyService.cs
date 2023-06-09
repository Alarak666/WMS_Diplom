using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Currencies;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.CurrencyServices
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public CurrencyService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<CurrencyListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Currency", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<CurrencyListViewModel>>(responseContent);
            return items;
        }

        public async Task<CurrencyDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Currency/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<CurrencyDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(CurrencyDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/Currency",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(CurrencyDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/Currency",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/Currency/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
