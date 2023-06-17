using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WMS.Core.FluentValidations.Documents;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.Products;
using WMS.Core.Models.DocumentModels.StockModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.AcceptanceOfGoods
{
    public partial class AcceptanceOfGoodDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IAcceptanceOfGoodService AcceptanceOfGoodService { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Inject] public IProductService ProductService { get; set; }
        [Inject] public IPalletService PalletService { get; set; }


        #region Form

        private double Total = 0;
        private AcceptanceOfGoodDetailViewModel? Model { get; set; } = new AcceptanceOfGoodDetailViewModel();
        private IEnumerable<EmployeeListViewModel>? EmployeeListViewModels { get; set; } =
            new List<EmployeeListViewModel>();
        private IEnumerable<PalletListViewModel>? PalletListViewModels { get; set; } =
            new List<PalletListViewModel>();
        private IEnumerable<ProductListViewModel>? ProductListViewModels { get; set; } =
            new List<ProductListViewModel>();

        private EmployeeListViewModel EmployeDetailModel { get; set; } = new EmployeeListViewModel();
        private ProductListViewModel ProductListViewModel { get; set; } = new ProductListViewModel();
        private PalletListViewModel PalletListViewModel { get; set; } = new PalletListViewModel();
        #endregion
        protected override async Task Load()
        {
            await base.Load();
            CalculateWeight();
            await LoadCombobox(string.Empty);
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await AcceptanceOfGoodService.GetDetailViewData(SelectedItemId, CancellationToken);

            EmployeDetailModel = EmployeeListViewModels?.FirstOrDefault(x => x.Id == Model?.EmployerId);
            PalletListViewModel = PalletListViewModels?.FirstOrDefault(x => x.Id == Model?.TypePalletId);
            ProductListViewModel = ProductListViewModels?.FirstOrDefault(x => x.Id == Model?.ProductId);
        }
        private void HandleInputChange(ChangeEventArgs e)
        {
            CalculateWeight();
        }
        private void CalculateWeight()
        {
            if (Model.Width <= 0 || Model.Height <= 0 || Model.Length <= 0 || Model.Qty <= 0)
            {
                Total= 0;
            }
            StateHasChanged();
            Total = Model.Width * Model.Height * Model.Length;
        }
        protected override async Task Save()
        {
            await UpdateModel();
            CalculateWeight();
            var validationResult = await ValidateSave();
            if (validationResult)
            {
                if (SelectedItemId != null)
                    await AcceptanceOfGoodService.UpdateDetailViewModel(Model, CancellationToken);
                else
                    await AcceptanceOfGoodService.SaveDetailViewModel(Model, CancellationToken);
                await Load();
                StateHasChanged();
            }
            else
            {
                throw new Exception("Bad");
            }
        }

        protected override async Task<bool> ValidateSave()
        {
            var validator = new AcceptanceOfGoodValidation();
            if (Model != null)
            {
                ValidationResult = await validator.ValidateAsync(Model);
                await ValidateObjectFailed();
                return ValidationResult.IsValid;
            }
            return false;
        }

        private async Task LoadCombobox(string? search)
        {
            EmployeeListViewModels = await EmployeeService.GetListViewItems("Staff", CancellationToken, "P");
            ProductListViewModels = await ProductService.GetListViewItems(search, CancellationToken);
            PalletListViewModels = await PalletService.GetListViewItems(search, CancellationToken);

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

        private async Task UpdateModel()
        {
            if (EmployeDetailModel?.Id != Guid.Empty)
                Model.EmployerId = EmployeDetailModel?.Id;
            if (ProductListViewModel?.Id != Guid.Empty)
                Model.ProductId = ProductListViewModel?.Id;
            if (PalletListViewModel?.Id != Guid.Empty)
                Model.TypePalletId = PalletListViewModel?.Id;

        }
    }
}
