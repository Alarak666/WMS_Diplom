using DevExpress.Utils.About;
using DevExpress.XtraSpellChecker.Parser;
using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.OrderModels;
using WMS.Core.Models.DocumentModels.Products;
using WMS.Core.Models.DocumentModels.VendorCustomers;
using WMS.UI.Services.DocumentService.ProductServices;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Orders
{
    public partial class OrderDetailViewForm : BaseDetailViewPopupForm
    {
        [Inject] public IOrderService OrderService { get; set; }
        [Inject] public IAcceptanceOfGoodService AcceptanceOfGoodService { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Inject] public IVendorCustomerService VendorCustomerService { get; set; }
        [Inject] public IProductService ProductService { get; set; }


        #region Form

        public IEnumerable<VendorCustomerListViewModel>? VendorCustomerListViewModels { get; set; } = new List<VendorCustomerListViewModel>();

        public VendorCustomerListViewModel? VendorCustomerListViewModel { get; set; } =
            new VendorCustomerListViewModel();

        public IEnumerable<EmployeeListViewModel>? EmployeeListViewModels { get; set; } =
            new List<EmployeeListViewModel>();

        public EmployeeListViewModel? EmployeeListViewModel { get; set; } = new EmployeeListViewModel();

        public IEnumerable<ProductListViewModel>? ProductListViewModels { get; set; } =
            new List<ProductListViewModel>();
        public ProductListViewModel? ProductListViewModel { get; set; } = new ProductListViewModel();
        private OrderDetailViewModel? Model { get; set; } = new OrderDetailViewModel();
        private List<OrderDetailModel>? OrderDetailModels { get; set; } = new List<OrderDetailModel>();
        private OrderDetailModel OrderDetailModel { get; set; } = new OrderDetailModel();
        public decimal TotalAmount { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }

        #endregion

        private async Task LoadListViewModel()
        {
            EmployeeListViewModels = await EmployeeService.GetListViewItems("", CancellationToken);
            VendorCustomerListViewModels = await VendorCustomerService.GetListViewItems("", CancellationToken);
            var lists = await AcceptanceOfGoodService.GetListViewItems("", CancellationToken);
            var products = await ProductService.GetListViewItems("", CancellationToken); 
            ProductListViewModels = products
                .Where(product => lists.Any(list => list.ProductId == product.Id))
                .ToList();
        }

        private async Task UpdateModel()
        {
            if (VendorCustomerListViewModel?.Id != Guid.Empty)
                Model.VendorCustomerId = VendorCustomerListViewModel?.Id;
            if (EmployeeListViewModel?.Id != Guid.Empty)
                Model.EmployeeId = EmployeeListViewModel?.Id;
            Model.OrderDetails = new List<OrderDetailModel>();
            Model.OrderDetails = OrderDetailModels;

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

        private async void HandleChangeProduct(OrderDetailModel productLine, Guid? newItemId)
        {
            var product = ProductListViewModels.FirstOrDefault(x => x.Id == (Guid)newItemId);
            productLine.ProductId = product?.Id;
            productLine.ProductName = product?.Name;
            productLine.UnitPrice = product.Price;
        }

        private void HandleChangeProductQuantity(OrderDetailModel productLine, decimal newValue)
        {
            productLine.Quantity = newValue;
            TotalAmount = OrderDetailModels.Sum(x => x.UnitPrice*x.Quantity);
            TotalPrice = OrderDetailModels.Sum(x => x.UnitPrice);
            TotalQuantity = OrderDetailModels.Sum(x => x.Quantity);


        }

        private void HandleChangeProductPrice(OrderDetailModel productLine, decimal newValue)
        {
            productLine.UnitPrice = newValue;
            TotalAmount = OrderDetailModels.Sum(x => x.UnitPrice * x.Quantity);
            TotalPrice = OrderDetailModels.Sum(x => x.UnitPrice);
            TotalQuantity = OrderDetailModels.Sum(x => x.Quantity);
        }
    }
}
