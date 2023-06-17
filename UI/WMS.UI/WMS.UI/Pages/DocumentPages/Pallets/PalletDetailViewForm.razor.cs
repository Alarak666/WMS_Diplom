using DevExpress.XtraSpellChecker.Parser;
using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Pallets
{
    public partial class PalletDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IPalletService PalletService { get; set; }
        [Inject] public IAreaTypeService AreaTypeService { get; set; }

        #region Form

        private PalletDetailViewModel? Model { get; set; } = new PalletDetailViewModel();

        public IEnumerable<AreaTypeListViewModel>? AreaTypeListViewModels { get; set; }
        public AreaTypeListViewModel? AreaTypeListViewModel { get; set; }

     
        #endregion
        protected override async Task Load()
        {
            await base.Load();
            await LoadListViewModel();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await PalletService.GetDetailViewData(SelectedItemId, CancellationToken);
            AreaTypeListViewModel = AreaTypeListViewModels?.FirstOrDefault(x => x.Id == Model?.AreaTypeId);

        }
        private async Task LoadListViewModel()
        {
            AreaTypeListViewModels = await AreaTypeService.GetListViewItems("", CancellationToken);
        }

        private async Task UpdateModel()
        {
            if (AreaTypeListViewModel?.Id != Guid.Empty)
                Model.AreaTypeId = AreaTypeListViewModel?.Id;
        }
        protected override async Task Save()
        {
            await UpdateModel();
            if (SelectedItemId != null)
                await PalletService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await PalletService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
