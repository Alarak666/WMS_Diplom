using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.IdentityModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.ApplicationUser
{
    public partial class ApplicationUserDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IApplicationUserService ApplicationUserService { get; set; }
        [Inject] public IApplicationUserSettingService ApplicationUserSettingService { get; set; }
        [Inject] public IToastService ToastService { get; set; }
        #region Form

        private ApplicationUserDetailViewModel? Model { get; set; } = new ApplicationUserDetailViewModel();

        private ApplicationUserSettingListViewModel? ListViewModel { get; set; } = new ();
        private IEnumerable<ApplicationUserSettingListViewModel>? ListViewModels { get; set; } = new List<ApplicationUserSettingListViewModel>();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            await LoadListViewModel();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await ApplicationUserService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        private async Task LoadListViewModel()
        {
            ListViewModels = await ApplicationUserSettingService.GetListViewItems("", CancellationToken);
        }
        private async Task UpdateModel()
        {
            if (ListViewModel?.Id != Guid.Empty)
                Model.UserSettingsId = ListViewModel?.Id;
           
        }
        protected override async Task Save()
        {
           await UpdateModel();
            if (SelectedItemId != null)
                await ApplicationUserService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await ApplicationUserService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
