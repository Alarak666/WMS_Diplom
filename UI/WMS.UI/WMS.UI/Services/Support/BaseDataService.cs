using Newtonsoft.Json;
using WMS.Core.Services.BaseServices;
using WMS.UI.Services.HttpClients;

namespace WMS.UI.Services.Support
{
    public class BaseDataService<T> : IBaseDataService<T> where T : class
    {
        HttpClientHelper _httpClientHelper;

        public BaseDataService(HttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }
        public async Task<T?> Get(Guid id, CancellationToken cancellation)
        {
            if (id == Guid.Empty) ;
            return null;
        }

        public async Task<T?> Get(Guid? id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty) ;
            return null;
        }

        public async Task<IEnumerable<T>> GetTop(string? searchText, CancellationToken cancellationToken, int top = 50, string? searchableColumns = null)
        {
            return null;
        }

        public async Task<IEnumerable<T>> GetAll(string? searchText, CancellationToken cancellation, Type? entityType = null)
        {
            var response = await _httpClientHelper.Get($"api/{nameof(T).Replace("DetailViewModel", "")}", cancellation);
            var responseContent = await response.Content.ReadAsStringAsync(cancellation);
            var items = JsonConvert.DeserializeObject<List<T?>>(responseContent);
            return items;
        }

        public async Task<T?> Create(T entity, CancellationToken cancellationToken)
        {
            return null;
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
        }

        public async Task Update(Guid id, T entity, CancellationToken cancellationToken)
        {
        }

    }
}