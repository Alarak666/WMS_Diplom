using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.Persons;
using WMS.Core.Models.DocumentModels.Positions;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Employes
{
    public partial class EmployeeDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IEmployeeService employeeService { get; set; }

        #region Form

        private EmployeeDetailViewModel? Model { get; set; } = new EmployeeDetailViewModel();

        private PersonDetailViewModel? Person { get; set; } = new PersonDetailViewModel();
        private List<PersonDetailViewModel>? Persons { get; set; } = new List<PersonDetailViewModel>();
        private PositionDetailViewModel? Position { get; set; } = new PositionDetailViewModel();
        private List<PositionDetailViewModel>? Positions { get; set; } = new List<PositionDetailViewModel>();
        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await employeeService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        private async Task UpdateModel()
        {
            if (Person?.Id != Guid.Empty)
                Model.PersonId = Person.Id;
            if (Position?.Id != Guid.Empty)
                Model.PositionId = Position.Id;
        }
        protected override async Task Save()
        {
            await UpdateModel();

            if (SelectedItemId != null)
                await employeeService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await employeeService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
