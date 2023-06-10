using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Currencies;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.PlaceParameters
{
    public partial class PlaceParameterDetailViewForm: BaseDetailViewPopupForm
    {
        [Inject] public IPlaceParameterService PlaceParameterService { get; set; }
        [Inject] public IPalletService PalletService { get; set; }

       
      

        #region Form
        public IEnumerable<PalletListViewModel>? PalletListViewModels { get; set; }
        public PalletListViewModel? PalletListViewModel { get; set; }

        private PlaceParameterDetailViewModel? Model { get; set; } = new PlaceParameterDetailViewModel();

        #endregion
        private async Task LoadListViewModel()
        {
            PalletListViewModels = await PalletService.GetListViewItems("", CancellationToken);
        }

        private async Task UpdateModel()
        {
            if (PalletListViewModel?.Id != Guid.Empty)
                Model.PalletId = PalletListViewModel?.Id;
        }
        protected override async Task Load()
        {
            await base.Load();
            await LoadListViewModel();
            ToastService.ShowInfo("Load Good");
            if(SelectedItemId !=null)
                Model = await PlaceParameterService.GetDetailViewData(SelectedItemId, CancellationToken);
        }
       
        protected override async Task Save()
        {
            await UpdateModel();
            if (SelectedItemId != null)
                await PlaceParameterService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await PlaceParameterService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
