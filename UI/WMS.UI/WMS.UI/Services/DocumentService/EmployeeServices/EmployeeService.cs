    using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Text;
using WMS.Core.DTO.EmployeeDtos;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.DocumentService.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClientHelper _httpClientHelper;
        public EmployeeService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }
        public async Task<IEnumerable<EmployeeListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation, string? searchOption)
        {
            var response = await _httpClientHelper.Get($"api/Employee?searchText={searchText}&searchOption={searchOption}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<EmployeeListViewModel>>(responseContent);
            return items;
        }

        public async Task<EmployeeDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Get($"api/Employee/{id}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var item = JsonConvert.DeserializeObject<EmployeeDetailViewModel>(responseContent);
            return item;
        }

        public async Task<bool> SaveDetailViewModel(EmployeeDetailViewModel? model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Post("api/Employee",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDetailViewModel(EmployeeDetailViewModel model, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Put("api/Employee",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"), cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation)
        {
            var response = await _httpClientHelper.Delete($"api/Employee/{id}", cancellation);
            return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
        }
    }
}
