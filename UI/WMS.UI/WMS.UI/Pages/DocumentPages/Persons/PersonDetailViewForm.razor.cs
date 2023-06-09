using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.Persons;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Persons
{
    public partial class PersonDetailViewForm: BaseDetailViewPopupForm
    {
        [Inject] public IPersonService PersonService { get; set; }

        #region Form

        private PersonDetailViewModel? Model { get; set; } = new PersonDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if(SelectedItemId !=null)
                Model = await PersonService.GetDetailViewData(SelectedItemId, CancellationToken);
        }
       
        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await PersonService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await PersonService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
