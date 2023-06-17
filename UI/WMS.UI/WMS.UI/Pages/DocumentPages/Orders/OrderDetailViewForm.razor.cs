using DevExpress.XtraSpellChecker.Parser;
using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.OrderModels;
using WMS.Core.Models.DocumentModels.VendorCustomers;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Orders
{
    public partial class OrderDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IOrderService OrderService { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Inject] public IVendorCustomerService VendorCustomerService { get; set; }

        #region Form

        public IEnumerable<VendorCustomerListViewModel>? VendorCustomerListViewModels { get; set; }
        public VendorCustomerListViewModel? VendorCustomerListViewModel { get; set; }
        public IEnumerable<EmployeeListViewModel>? EmployeeListViewModels { get; set; }
        public EmployeeListViewModel? EmployeeListViewModel { get; set; }
        private OrderDetailViewModel? Model { get; set; } = new OrderDetailViewModel();
        private List<OrderDetailModel>? OrderDetailModels { get; set; } = new List<OrderDetailModel>();
        private OrderDetailModel OrderDetailModel { get; set; } = new OrderDetailModel();

        #endregion

        private async Task LoadListViewModel()
        {
            EmployeeListViewModels = await EmployeeService.GetListViewItems("", CancellationToken);
            VendorCustomerListViewModels = await VendorCustomerService.GetListViewItems("", CancellationToken);
        }

        private async Task UpdateModel()
        {
            if (VendorCustomerListViewModel?.Id != Guid.Empty)
                Model.VendorCustomerId = VendorCustomerListViewModel?.Id;
            if (EmployeeListViewModel?.Id != Guid.Empty)
                Model.EmployeeId = EmployeeListViewModel?.Id;  

        }
        protected override async Task Load()
        {
            await base.Load();
            await LoadListViewModel();
            ToastService.ShowInfo("Load Good");
            if (SelectedItemId != null)
                Model = await OrderService.GetDetailViewData(SelectedItemId, CancellationToken);
            EmployeeListViewModel = EmployeeListViewModels?.FirstOrDefault(x => x.Id == Model?.EmployeeId);
            VendorCustomerListViewModel = VendorCustomerListViewModels?.FirstOrDefault(x => x.Id == Model?.VendorCustomerId);

        }


        protected override async Task Save()
        {
            await UpdateModel();
            if (SelectedItemId != null)
                await OrderService.UpdateDetailViewModel(Model, CancellationToken);
            else
                await OrderService.SaveDetailViewModel(Model, CancellationToken);
        }
    }
}
