using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Currencies;
using WMS.Core.Models.DocumentModels.Products;
using WMS.Core.Models.DocumentModels.Units;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Products
{
    public partial class ProductDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IProductService ProductService { get; set; }
        [Inject] public IUnitService UnitService { get; set; }

    

        #region Form
        public IEnumerable<UnitListViewModel>? UnitListViewModels { get; set; }
        public UnitListViewModel? UnitListViewModel { get; set; }
        private ProductDetailViewModel? Model { get; set; } = new ProductDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            await LoadListViewModel();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await ProductService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        private async Task LoadListViewModel()
        {
            UnitListViewModels = await UnitService.GetListViewItems("", CancellationToken);
        }

        private async Task UpdateModel()
        {
            if (UnitListViewModel?.Id != Guid.Empty)
                Model.MainUnitId= UnitListViewModel?.Id;
        }
        protected override async Task Save()
        {
            await UpdateModel();
            if (SelectedItemId != null)
                await ProductService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await ProductService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
