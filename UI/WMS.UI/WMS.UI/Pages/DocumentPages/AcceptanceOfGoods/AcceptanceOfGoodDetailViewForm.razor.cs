using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.AcceptanceOfGoods
{
    public partial class AcceptanceOfGoodDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IAcceptanceOfGoodService AcceptanceOfGoodService { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; }

        #region Form

        private AcceptanceOfGoodDetailViewModel? Model { get; set; } = new AcceptanceOfGoodDetailViewModel();
        private IEnumerable<EmployeeListViewModel> EmployeeListViewModels { get; set; } =
            new List<EmployeeListViewModel>();

        private EmployeeListViewModel EmployeDetailModel { get; set; } = new EmployeeListViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            await EmployeeLoad(string.Empty);
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await AcceptanceOfGoodService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
           await UpdateModel();
            if (SelectedItemId != null)
                await AcceptanceOfGoodService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await AcceptanceOfGoodService.SaveDetailViewModel(Model, CancellationToken);
        }

        private async Task EmployeeLoad(string? search)
        {
            EmployeeListViewModels = await EmployeeService.GetListViewItems(search, CancellationToken);
            StateHasChanged();
        }

        private async Task SaveAndClose(MouseEventArgs arg)
        {
            if (SelectedItemId != null)
                await AcceptanceOfGoodService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await AcceptanceOfGoodService.SaveDetailViewModel(Model, CancellationToken);
                await Close();
        }

        private async  Task UpdateModel()
        {
            Model.EmployerId = EmployeDetailModel.Id;
        }
    }
}
