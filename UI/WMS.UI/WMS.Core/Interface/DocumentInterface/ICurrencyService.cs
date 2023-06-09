using WMS.Core.Models.DocumentModels.Currencies;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<CurrencyDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(CurrencyDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(CurrencyDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
