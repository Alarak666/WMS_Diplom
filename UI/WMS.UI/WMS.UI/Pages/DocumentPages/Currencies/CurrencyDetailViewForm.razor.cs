using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Currencies;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Currencies
{
    public partial class CurrencyDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public ICurrencyService CurrencyService { get; set; }

        #region Form

        private CurrencyDetailViewModel? Model { get; set; } = new CurrencyDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await CurrencyService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await CurrencyService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await CurrencyService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
