using WMS.Core.Models.DocumentModels.IdentityModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IApplicationUserSettingService
    {
        Task<IEnumerable<ApplicationUserSettingListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<ApplicationUserSettingDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(ApplicationUserSettingDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(ApplicationUserSettingDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
