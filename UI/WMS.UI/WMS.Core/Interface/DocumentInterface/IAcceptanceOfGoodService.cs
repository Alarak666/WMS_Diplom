using WMS.Core.Models.DocumentModels.StockModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IAcceptanceOfGoodService
    {
        Task<IEnumerable<AcceptanceOfGoodListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<AcceptanceOfGoodDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(AcceptanceOfGoodDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(AcceptanceOfGoodDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
