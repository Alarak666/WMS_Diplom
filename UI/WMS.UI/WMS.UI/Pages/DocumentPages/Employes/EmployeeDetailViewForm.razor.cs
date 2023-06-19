using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.ControllerInterface;
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
        [Inject] public IPersonService PersonService { get; set; }
        [Inject] public IPositionService PositionService { get; set; }

        [Inject] public IUserNotificationService userNotificationService { get; set; }

        #region Form

        private EmployeeDetailViewModel? Model { get; set; } = new EmployeeDetailViewModel();
        private PersonListViewModel _person;
        private PersonListViewModel? Person
        {
            get { return _person; }
            set
            {
                _person = value;
                Model.FirstName = value?.FirstName ?? "";
                Model.LastName = value?.LastName ?? "";
                Model.MiddleName = value?.Name ?? "";
            }
        } 
        private IEnumerable<PersonListViewModel>? Persons { get; set; } = new List<PersonListViewModel>();
        private PositionListViewModel? Position { get; set; } = new PositionListViewModel();
        private IEnumerable<PositionListViewModel>? Positions { get; set; } = new List<PositionListViewModel>();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            Persons = await PersonService.GetListViewItems("", CancellationToken);
            Positions = await PositionService.GetListViewItems("", CancellationToken);

            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await employeeService.GetDetailViewData(SelectedItemId, CancellationToken);
            Person = Persons?.FirstOrDefault(x => x.Id == Model?.PersonId);
            Position = Positions?.FirstOrDefault(x => x.Id == Model?.PositionId);
            StateHasChanged();

        }

        private async Task UpdateModel()
        {
            if (Person?.Id != Guid.Empty && Person?.Id != null)
                Model.PersonId = Person.Id;
            if (Position?.Id != Guid.Empty && Position?.Id != null)
                Model.PositionId = Position.Id;
            StateHasChanged();
        }
        protected override async Task Save()
        {
            await UpdateModel();

            if (SelectedItemId != null)
                await employeeService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await employeeService.SaveDetailViewModel(Model, CancellationToken);
            userNotificationService.AddDocumentCreateSuccessMessage("Create Document");
            StateHasChanged();

        }
    }
}
