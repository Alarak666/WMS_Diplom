using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Currencies;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.AreaTypes
{
    public partial class AreaTypeDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IAreaTypeService AreaTypeService { get; set; }
        [Inject] public IRegionService RegionService { get; set; }

        #region Form

        private AreaTypeDetailViewModel? Model { get; set; } = new AreaTypeDetailViewModel();

        private RegionListViewModel? RegionListViewModel { get; set; } = new();
        private AreaTypeListViewModel? AreaTypeListViewModel { get; set; } = new();
        private IEnumerable<RegionListViewModel>? RegionListViewModels { get; set; } = new List<RegionListViewModel>();
        private IEnumerable<AreaTypeListViewModel>? AreaTypeListViewModels { get; set; } = new List<AreaTypeListViewModel>();

        private async Task LoadListViewModel()
        {
            AreaTypeListViewModels = await AreaTypeService.GetListViewItems("", CancellationToken);
            RegionListViewModels = await RegionService.GetListViewItems("", CancellationToken);
        }

        private async Task UpdateModel()
        {
            if (RegionListViewModel?.Id != Guid.Empty)
                Model.RegionId = RegionListViewModel?.Id;
            if (AreaTypeListViewModel?.Id != Guid.Empty)
                Model.IncludeAreaId = AreaTypeListViewModel?.Id;
        }

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            if (SelectedItemId != null)
                Model = await AreaTypeService.GetDetailViewData(SelectedItemId, CancellationToken);
            await LoadListViewModel();
            ToastService.ShowInfo("Load Good");
            RegionListViewModel = RegionListViewModels?.FirstOrDefault(x => x.Id == Model?.RegionId);

        }

        protected override async Task Save()
        {
            await UpdateModel();
            if (SelectedItemId != null)
                await AreaTypeService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await AreaTypeService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
