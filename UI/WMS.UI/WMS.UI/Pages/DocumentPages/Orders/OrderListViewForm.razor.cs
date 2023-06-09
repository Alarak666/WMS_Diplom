using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.OrderModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Orders
{
    public partial class OrderListViewForm : BaseListPage
    {
        [Inject] public IOrderService _OrderService { get; set; }
        private IEnumerable<OrderListViewModel>? _items;
        private bool _detailViewPopupVisible = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadData();
        }

        private async Task HandleNewItem()
        {
            _selectedItemId = default;
            _detailViewPopupVisible = true;
        }

        protected override async Task LoadData()
        {
            _items = await _OrderService.GetListViewItems(_searchText, cancellationToken);
        }

        private async Task HandleDetailViewPopupClosed(bool saved)
        {
            _detailViewPopupVisible = false;
            _selectedItemId = default;
            _dataGrid.ClearSelection();
            if (saved) await LoadData();
        }

        private async Task HandleEditItem()
        {
            if (_selectedItemId == null) return;
            _detailViewPopupVisible = true;
        }

        private async Task HandleRemoveItem()
        {
            if (_selectedItemId == null) return;
            await _OrderService.DeleteDetailViewModel((Guid)_selectedItemId, cancellationToken);
            await LoadData();
            _selectedItemId = null;
        }
        private async Task HandleDoubleClickRow(GridRowClickEventArgs arg)
        {
            //_selectedItemId = (_dataGrid.SelectedDataItem as OrderListViewModel)?.Id;
            await HandleNewItem();
        }
        private void HandleCloneItem()
        {
            throw new NotImplementedException();
        }
    }
}
