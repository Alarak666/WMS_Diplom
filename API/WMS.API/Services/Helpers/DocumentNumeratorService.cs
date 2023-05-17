using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WMS.Data.Context;
using WMS.Data.Entity.BaseClass;
using WMS.Data.Interface.ControllerInterface;

namespace WMS.API.Services.Helpers;

public class DocumentNumeratorService<T> : IDocumentNumeratorService<T> where T:class
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DocumentNumeratorService<T>> _logger;

    public DocumentNumeratorService(ApplicationDbContext contextFactoryFactory,
        ILogger<DocumentNumeratorService<T>> logger)
    {
        _context = contextFactoryFactory;
        _logger = logger;
    }



    public async Task<string> SetCatalogNumber(string catalogUniqueCode)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(catalogUniqueCode))
                return catalogUniqueCode;

            const int numberLength = 9;

            var lastItem = await _context
                .Set<T>()
                .Where(x => (x as BaseCatalog).UniqueCode != null)
                .OrderByDescending(x => (x as BaseCatalog).UniqueCode)
                .Select(x => new { (x as BaseCatalog).UniqueCode })
                .FirstOrDefaultAsync();

            if (lastItem != null && lastItem?.UniqueCode != null)
                try
                {
                    var oldNumber = lastItem.UniqueCode;
                    oldNumber = new string(oldNumber.Where(char.IsDigit).ToArray());
                    var nextNumber = Convert.ToInt32(oldNumber) + 1;
                    return nextNumber.ToString().PadLeft(numberLength, '0');
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }

            return 1.ToString().PadLeft(numberLength, '0');
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }


        return "";
    }

}