using Microsoft.AspNetCore.Components;
using WMS.Core.Interface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Persons
{
    public partial class EmployeeDetailViewForm: BaseDetailViewPopupForm
    {
        [Inject] public IEmployeeService employeeService { get; set; }

        #region Form

        private EmployeeDetailViewModel? Model { get; set; } = new EmployeeDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if(SelectedItemId !=null)
                Model = await employeeService.GetDetailViewData(SelectedItemId, CancellationToken);
        }
       
        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await employeeService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await employeeService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
