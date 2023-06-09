using Newtonsoft.Json;
using System.Text;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.IdentityModels;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.ApplicationUserSettingServices
{
    public class ApplicationUserSettingService : IApplicationUserSettingService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public ApplicationUserSettingService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IEnumerable<ApplicationUserSettingListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/ApplicationUserSetting", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<ApplicationUserSettingListViewModel>>(responseContent);
            return items;
        }

        public async Task<ApplicationUserSettingDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/ApplicationUserSetting/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<ApplicationUserSettingDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(ApplicationUserSettingDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/ApplicationUserSetting",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(ApplicationUserSettingDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/ApplicationUserSetting",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/ApplicationUserSetting/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
