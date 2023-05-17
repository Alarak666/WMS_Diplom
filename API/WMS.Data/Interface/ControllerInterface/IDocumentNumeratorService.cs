using System.Linq.Expressions;

namespace WMS.Data.Interface.ControllerInterface;

public interface IDocumentNumeratorService<T> where T : class
{
    Task<string> SetCatalogNumber(string uniqueCode);
}