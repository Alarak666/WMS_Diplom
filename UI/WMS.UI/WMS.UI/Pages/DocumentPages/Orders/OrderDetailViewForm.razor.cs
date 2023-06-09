using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.OrderModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Orders
{
    public partial class OrderDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IOrderService OrderService { get; set; }

        #region Form

        private OrderDetailViewModel? Model { get; set; } = new OrderDetailViewModel();
        private List<OrderDetailModel>? OrderDetailModels { get; set; } = new List<OrderDetailModel>();
        private OrderDetailModel OrderDetailModel { get; set; } = new OrderDetailModel();
        
        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await OrderService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await OrderService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await OrderService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
