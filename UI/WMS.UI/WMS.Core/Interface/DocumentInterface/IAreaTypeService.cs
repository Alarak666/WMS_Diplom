using WMS.Core.Models.DocumentModels.StockModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IAreaTypeService
    {
        Task<IEnumerable<AreaTypeListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<AreaTypeDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(AreaTypeDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(AreaTypeDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
