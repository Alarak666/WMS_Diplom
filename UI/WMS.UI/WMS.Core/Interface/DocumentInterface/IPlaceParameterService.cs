using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.StockModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IPlaceParameterService
    {
        Task<IEnumerable<PlaceParameterListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<PlaceParameterDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(PlaceParameterDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(PlaceParameterDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
