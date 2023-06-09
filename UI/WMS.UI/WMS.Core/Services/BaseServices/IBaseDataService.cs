namespace WMS.Core.Services.BaseServices
{
    public interface IBaseDataService<T> where T : class
    {
        Task<T?> Get(Guid id,CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetTop(string? searchText, CancellationToken cancellationToken, int top = 50,string? searchableColumns = null);
        Task<IEnumerable<T>> GetAll(string? searchText, CancellationToken cancellationToken, Type? entityType = null);
        Task<T?> Create(T entity, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
        Task Update(Guid id, T entity, CancellationToken cancellationToken);
    }
}