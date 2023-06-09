using WMS.Core.Models.DocumentModels.StockModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IRegionService
    {
        Task<IEnumerable<RegionListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<RegionDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(RegionDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(RegionDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
