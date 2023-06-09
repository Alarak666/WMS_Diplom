using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Pallets
{
    public partial class PalletDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IPalletService PalletService { get; set; }

        #region Form

        private PalletDetailViewModel? Model { get; set; } = new PalletDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await PalletService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await PalletService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await PalletService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
