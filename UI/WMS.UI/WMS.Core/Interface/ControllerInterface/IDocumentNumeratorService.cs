using System.Linq.Expressions;

namespace WMS.Core.Interface.ControllerInterface;

public interface IDocumentNumeratorService<T> where T : class
{
    Task<string> SetCatalogNumber(string uniqueCode);
}