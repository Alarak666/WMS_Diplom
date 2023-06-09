using WMS.Core.Models.DocumentModels.IdentityModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IApplicationRoleService
    {
        Task<IEnumerable<ApplicationRoleListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<ApplicationRoleDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(ApplicationRoleDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(ApplicationRoleDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
