namespace WMS.Core.Services.BaseServices
{
    public interface IBaseDataService<T> where T : class
    {
        Task<T?> Get(Guid id);
        Task<T?> Get(Guid? id);
        Task<IEnumerable<T>> GetTop(string? searchText, int top = 50, string? searchableColumns = null);
        Task<IEnumerable<T>> GetAll(string? searchText, Type? entityType = null);
        Task<T?> Create(T entity);
        Task Delete(Guid id);
        Task Update(Guid id, T entity);
        T MapNew(T entity);
        void MapCopy(T source, T target);
        T MapFrom<Y>(Y source);
        Y MapTo<Y>(T source);
        Task<T?> GetFirstOrDefaultAsync(Func<T, bool> func);
        Task<IEnumerable<T?>> GetWhereAsync(Func<T, bool> func);
    }
}