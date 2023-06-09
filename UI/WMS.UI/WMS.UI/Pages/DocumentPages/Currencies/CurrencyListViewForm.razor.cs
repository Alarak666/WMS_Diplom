using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Currencies;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Currencies
{
    public partial class CurrencyListViewForm : BaseListPage
    {
        [Inject] public ICurrencyService _CurrencyService { get; set; }
        private IEnumerable<CurrencyListViewModel>? _items;
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
            _items = await _CurrencyService.GetListViewItems(_searchText, cancellationToken);
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
            await _CurrencyService.DeleteDetailViewModel((Guid)_selectedItemId, cancellationToken);
            await LoadData();
            _selectedItemId = null;
        }
        private async Task HandleDoubleClickRow(GridRowClickEventArgs arg)
        {
            //_selectedItemId = (_dataGrid.SelectedDataItem as CurrencyListViewModel)?.Id;
            await HandleNewItem();
        }
        private void HandleCloneItem()
        {
            throw new NotImplementedException();
        }
    }
}
