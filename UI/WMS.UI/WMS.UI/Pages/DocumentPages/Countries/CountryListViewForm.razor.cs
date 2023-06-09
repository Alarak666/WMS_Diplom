using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.CountryModels;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Countries
{
    public partial class CountryListViewForm : BaseListPage
    {
        [Inject] public ICountryService _CountyService { get; set; }
        private IEnumerable<CountryListViewModel>? _items;
        private bool _detailViewPopupVisible = false;

        protected override async Task OnInitializedAsync()
        {
            await OnInitializedAsync();
            await LoadData();
        }

        private async Task HandleNewItem()
        {
            _selectedItemId = default;
            _detailViewPopupVisible = true;
        }

        protected override async Task LoadData()
        {
           _items = await _CountyService.GetListViewItems(_searchText, cancellationToken);
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
            await _CountyService.DeleteDetailViewModel((Guid)_selectedItemId, cancellationToken);
            await LoadData();
            _selectedItemId = null;
        }
        private async Task HandleDoubleClickRow(GridRowClickEventArgs arg)
        {
            //_selectedItemId = (_dataGrid.SelectedDataItem as CountyListViewModel)?.Id;
            await HandleNewItem();
        }
        private void HandleCloneItem()
        {
            throw new NotImplementedException();
        }
    }
}
