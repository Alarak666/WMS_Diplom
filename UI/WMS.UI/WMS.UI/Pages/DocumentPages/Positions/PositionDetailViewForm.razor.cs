using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Positions;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Positions
{
    public partial class PositionDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IPositionService PositionService { get; set; }

        #region Form

        private PositionDetailViewModel? Model { get; set; } = new PositionDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await PositionService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await PositionService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await PositionService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
