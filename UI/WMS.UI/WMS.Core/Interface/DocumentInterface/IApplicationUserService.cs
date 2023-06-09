using WMS.Core.Models.DocumentModels.IdentityModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<ApplicationUserDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(ApplicationUserDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(ApplicationUserDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
