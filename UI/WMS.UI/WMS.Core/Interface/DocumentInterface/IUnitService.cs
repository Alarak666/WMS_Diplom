using WMS.Core.Models.DocumentModels.Units;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<UnitDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(UnitDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(UnitDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
