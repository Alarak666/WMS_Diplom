using System.Linq.Expressions;

namespace WMS.Core.Interface;

public interface IDocumentRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken, Expression<Func<T, bool>>? whereClause = null,
        Expression<Func<T, string>>? orderClause = null);
    Task<T?> Get(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includeExpressions);
    Task<T> Create(T entity, CancellationToken cancellationToken);
    Task<T> Update(T entity, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetPage(
        CancellationToken cancellationToken,
        int PaneNo,
        int PageSize,
        Expression<Func<T, bool>>? whereClause = null,
        Expression<Func<T, string>>? orderClause = null);
}