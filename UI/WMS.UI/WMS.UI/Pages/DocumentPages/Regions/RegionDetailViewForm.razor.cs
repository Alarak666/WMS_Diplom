using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Regions
{
    public partial class RegionDetailViewForm: BaseDetailViewPopupForm
    {
        [Inject] public IRegionService RegionService { get; set; }

        #region Form

        private RegionDetailViewModel? Model { get; set; } = new RegionDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if(SelectedItemId !=null)
                Model = await RegionService.GetDetailViewData(SelectedItemId, CancellationToken);
        }
       
        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await RegionService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await RegionService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
