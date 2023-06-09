using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.CountryModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Countries
{
    public partial class CountryDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public ICountryService CountryService { get; set; }

        #region Form

        private CountryDetailViewModel? Model { get; set; } = new CountryDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await CountryService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await CountryService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await CountryService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
