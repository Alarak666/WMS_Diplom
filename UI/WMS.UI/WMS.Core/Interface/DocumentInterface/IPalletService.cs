using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.StockModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IPalletService
    {
        Task<IEnumerable<PalletListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<PalletDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(PalletDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(PalletDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
