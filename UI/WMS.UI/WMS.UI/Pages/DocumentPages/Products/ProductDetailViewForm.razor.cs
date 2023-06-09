using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Products;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Products
{
    public partial class ProductDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IProductService ProductService { get; set; }

        #region Form

        private ProductDetailViewModel? Model { get; set; } = new ProductDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await ProductService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await ProductService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await ProductService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
