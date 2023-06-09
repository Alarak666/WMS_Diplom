using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.IdentityModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.ApplicationUser
{
    public partial class ApplicationUserDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IApplicationUserService ApplicationUserService { get; set; }

        #region Form

        private ApplicationUserDetailViewModel? Model { get; set; } = new ApplicationUserDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await ApplicationUserService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await ApplicationUserService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await ApplicationUserService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
