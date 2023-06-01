using Microsoft.AspNetCore.Components;
using WMS.Core.DTO.EmployeeDtos;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Employes
{
    public partial class EmployeeDetailViewForm: BaseDetailViewPopupForm
    {
        //[Inject] public IEmployeeService employeeService { get; set; }
        //[Inject] public IListDivisionService divisionService { get; set; }

        #region Form

        private EmployeeDto Model { get; set; } = new EmployeeDto();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
         //   Model = await employeeService.GetDetailViewData(SelectedItemId);
        }

        protected override async Task<bool> ValidateSave()
        {
            //ValidationResult = await employeeService.GetValidationPost(Model);
            StateHasChanged();
            return ValidationResult.IsValid;
        }

        protected override async Task Save()
        {
         //   await employeeService.SaveDetailViewModel(Model);
        }
        //private async Task<IEnumerable<Division>> HandleLoadDivisions(string? arg)
        //{
        //   // return await divisionService.GetByDivisionOnCompany(Model.CompanyId);
        //}


    }
}
