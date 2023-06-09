using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.PlaceParameters
{
    public partial class PlaceParameterDetailViewForm: BaseDetailViewPopupForm
    {
        [Inject] public IPlaceParameterService PlaceParameterService { get; set; }

        #region Form

        private PlaceParameterDetailViewModel? Model { get; set; } = new PlaceParameterDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if(SelectedItemId !=null)
                Model = await PlaceParameterService.GetDetailViewData(SelectedItemId, CancellationToken);
        }
       
        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await PlaceParameterService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await PlaceParameterService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
