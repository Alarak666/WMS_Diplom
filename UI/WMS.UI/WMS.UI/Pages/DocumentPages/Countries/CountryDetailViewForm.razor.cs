using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.CountryModels;
using WMS.Core.Models.DocumentModels.Currencies;
using WMS.Core.Models.DocumentModels.Divisions;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Countries
{
    public partial class CountryDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public ICountryService CountryService { get; set; }
        [Inject] public ICurrencyService CurrencyService { get; set; }


        #region Form

        private CountryDetailViewModel? Model { get; set; } = new CountryDetailViewModel();
        public IEnumerable<CurrencyListViewModel>? CurrencyListViewModels { get; set; }
        public CurrencyListViewModel? CurrencyListViewModel { get; set; }

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            await LoadListViewModel();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await CountryService.GetDetailViewData(SelectedItemId, CancellationToken);
            CurrencyListViewModel = CurrencyListViewModels?.FirstOrDefault(x => x.Id == Model?.CurrencyId);

        }
        private async Task LoadListViewModel()
        {
            CurrencyListViewModels = await CurrencyService.GetListViewItems("", CancellationToken);
        }

        private async Task UpdateModel()
        {
            if (CurrencyListViewModel.Id != Guid.Empty)
                Model.CurrencyId = CurrencyListViewModel.Id;
        }
        protected override async Task Save()
        {
            await UpdateModel();
            if (SelectedItemId != null)
                await CountryService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await CountryService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
