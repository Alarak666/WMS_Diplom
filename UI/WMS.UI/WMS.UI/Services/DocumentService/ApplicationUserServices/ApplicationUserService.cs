using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.IdentityModels;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.ApplicationUserServices
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public ApplicationUserService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<ApplicationUserListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/ApplicationUser", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<ApplicationUserListViewModel>>(responseContent);
            return items;
        }

        public async Task<ApplicationUserDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/ApplicationUser/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<ApplicationUserDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(ApplicationUserDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/ApplicationUser",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(ApplicationUserDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/ApplicationUser",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/ApplicationUser/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
