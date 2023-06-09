using WMS.Core.Models.DocumentModels.Positions;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<PositionDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(PositionDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(PositionDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
