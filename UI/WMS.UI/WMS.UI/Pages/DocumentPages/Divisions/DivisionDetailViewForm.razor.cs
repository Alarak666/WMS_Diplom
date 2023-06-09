using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Divisions;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Divisions
{
    public partial class DivisionDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IDivisionService DivisionService { get; set; }

        #region Form

        private DivisionDetailViewModel? Model { get; set; } = new DivisionDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await DivisionService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await DivisionService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await DivisionService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
