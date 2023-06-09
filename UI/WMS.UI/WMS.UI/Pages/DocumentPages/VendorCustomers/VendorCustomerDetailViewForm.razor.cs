using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.VendorCustomers;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.VendorCustomers
{
    public partial class VendorCustomerDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IVendorCustomerService VendorCustomerService { get; set; }

        #region Form

        private VendorCustomerDetailViewModel? Model { get; set; } = new VendorCustomerDetailViewModel();

        #endregion
        protected override async Task Load()
        {
            await base.Load();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await VendorCustomerService.GetDetailViewData(SelectedItemId, CancellationToken);
        }

        protected override async Task Save()
        {
            if (SelectedItemId != null)
                await VendorCustomerService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await VendorCustomerService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
