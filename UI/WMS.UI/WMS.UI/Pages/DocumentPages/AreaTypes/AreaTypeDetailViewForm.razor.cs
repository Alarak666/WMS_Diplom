using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.AreaTypes
{
    public partial class AreaTypeDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IAreaTypeService AreaTypeService { get; set; }

        #region Form

        private AreaTypeDetailViewModel? Model { get; set; } = new AreaTypeDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await AreaTypeService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await AreaTypeService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await AreaTypeService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
