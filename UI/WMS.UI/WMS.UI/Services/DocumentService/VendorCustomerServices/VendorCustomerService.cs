using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.VendorCustomers;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.VendorCustomerServices
{
    public class VendorCustomerService : IVendorCustomerService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public VendorCustomerService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<VendorCustomerListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/VendorCustomer", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<VendorCustomerListViewModel>>(responseContent);
            return items;
        }

        public async Task<VendorCustomerDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/VendorCustomer/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<VendorCustomerDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(VendorCustomerDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/VendorCustomer",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            var responseContent = await response.Content.ReadAsStringAsync();
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(VendorCustomerDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/VendorCustomer",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/VendorCustomer/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
