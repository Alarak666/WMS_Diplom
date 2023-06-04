namespace WMS.Core.Services.BaseServices
{
    public interface IBaseDataService<T> where T : class
    {
        Task<T?> Get(Guid id);
        Task<IEnumerable<T>> GetTop(string? searchText, int top = 50, string? searchableColumns = null);
        Task<IEnumerable<T>> GetAll(string? searchText, Type? entityType = null);
        Task<T?> Create(T entity);
        Task Delete(Guid id);
        Task Update(Guid id, T entity);
    }
}