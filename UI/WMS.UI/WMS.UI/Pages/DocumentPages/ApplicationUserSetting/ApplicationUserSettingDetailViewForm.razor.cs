using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.IdentityModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.ApplicationUserSetting
{
    public partial class ApplicationUserSettingDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IApplicationUserSettingService ApplicationUserSettingService { get; set; }

        #region Form

        private ApplicationUserSettingDetailViewModel? Model { get; set; } = new ApplicationUserSettingDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await ApplicationUserSettingService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await ApplicationUserSettingService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await ApplicationUserSettingService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
