using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Divisions;
using WMS.Core.Models.DocumentModels.VendorCustomers;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Divisions
{
    public partial class DivisionDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IDivisionService DivisionService { get; set; }

        #region Form

        private DivisionDetailViewModel? Model { get; set; } = new DivisionDetailViewModel();

        public IEnumerable<DivisionListViewModel>? DivisionListViewModels { get; set; }
        public DivisionListViewModel? DivisionListViewModel { get; set; }

        private async Task LoadListViewModel()
        {
            DivisionListViewModels = await DivisionService.GetListViewItems("", CancellationToken);
        }

        private async Task UpdateModel()
        {
            if (DivisionListViewModel?.Id != Guid.Empty)
                Model.ParentDivisionId = DivisionListViewModel?.Id;
        }
        #endregion
        protected override async Task Load()
        {
            await base.Load();
            await LoadListViewModel();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await DivisionService.GetDetailViewData(SelectedItemId, CancellationToken);
            DivisionListViewModel = DivisionListViewModels?.FirstOrDefault(x => x.Id == Model?.ParentDivisionId);

        }

        protected override async Task Save()
        {
            await UpdateModel();
            if (SelectedItemId != null)
                await DivisionService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await DivisionService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
