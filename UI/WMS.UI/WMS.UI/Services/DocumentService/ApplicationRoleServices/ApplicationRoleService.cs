using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.IdentityModels;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.ApplicationRoleServices
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public ApplicationRoleService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<ApplicationRoleListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/ApplicationRole", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<ApplicationRoleListViewModel>>(responseContent);
            return items;
        }

        public async Task<ApplicationRoleDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/ApplicationRole/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<ApplicationRoleDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(ApplicationRoleDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/ApplicationRole",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(ApplicationRoleDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/ApplicationRole",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/ApplicationRole/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
