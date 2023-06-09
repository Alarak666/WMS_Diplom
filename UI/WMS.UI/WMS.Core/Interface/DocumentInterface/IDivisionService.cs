using WMS.Core.Models.DocumentModels.Divisions;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IDivisionService
    {
        Task<IEnumerable<DivisionListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<DivisionDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(DivisionDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(DivisionDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
