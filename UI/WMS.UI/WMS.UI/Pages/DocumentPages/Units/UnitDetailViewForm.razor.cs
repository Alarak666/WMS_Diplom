using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.Units;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Units
{
    public partial class UnitDetailViewForm: BaseDetailViewPopupForm
    {
        [Inject] public IUnitService UnitService { get; set; }

        #region Form

        private UnitDetailViewModel? Model { get; set; } = new UnitDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if(SelectedItemId !=null)
                Model = await UnitService.GetDetailViewData(SelectedItemId, CancellationToken);
        }
       
        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await UnitService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await UnitService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
