using AutoMapper;
using WMS.Core.Interface;
using WMS.Core.Services.BaseServices;
using WMS.Core.Services.UserMessages;

namespace WMS.UI.Services.Support
{
    public class BaseDataService<T> : IBaseDataService<T> where T : class
    {
        public async Task<T?> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> Get(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetTop(string? searchText, int top = 50, string? searchableColumns = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll(string? searchText, Type? entityType = null)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

    }
}