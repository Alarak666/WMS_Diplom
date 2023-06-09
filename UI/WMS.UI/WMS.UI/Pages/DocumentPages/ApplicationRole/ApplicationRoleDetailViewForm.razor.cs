using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.IdentityModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.ApplicationRole
{
    public partial class ApplicationRoleDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IApplicationRoleService ApplicationRoleService { get; set; }

        #region Form

        private ApplicationRoleDetailViewModel? Model { get; set; } = new ApplicationRoleDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await ApplicationRoleService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await ApplicationRoleService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await ApplicationRoleService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
