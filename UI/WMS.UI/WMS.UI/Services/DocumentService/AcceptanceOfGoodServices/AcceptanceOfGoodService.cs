using Newtonsoft.Json;
using System.Text;
using WMS.Core.DTO.Middlewares;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.AcceptanceOfGoodServices
{
    public class AcceptanceOfGoodService : IAcceptanceOfGoodService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public AcceptanceOfGoodService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<AcceptanceOfGoodListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/AcceptanceOfGood", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<AcceptanceOfGoodListViewModel>>(responseContent);
            return items;
        }

        public async Task<AcceptanceOfGoodDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/AcceptanceOfGood/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<AcceptanceOfGoodDetailViewModel>(responseContent);
            return item;
        }

        public async Task<ErrorResponseDto> SaveDetailViewModel(AcceptanceOfGoodDetailViewModel? model, CancellationToken cancellation)
        {
     
            var response = await _httpClientHelper.Post("api/AcceptanceOfGood",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            string jsonString = await response.Content.ReadAsStringAsync();

            var modelss = JsonConvert.DeserializeObject<ErrorResponseDto>(jsonString);

            return modelss;
        }

        public async Task<bool> UpdateDetailViewModel(AcceptanceOfGoodDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/AcceptanceOfGood",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/AcceptanceOfGood/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
