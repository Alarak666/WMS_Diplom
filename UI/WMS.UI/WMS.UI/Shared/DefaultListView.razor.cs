using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using WMS.Core.Entities;
using WMS.Core.Services.BaseServices;
namespace WMS.UI.Shared
{
    public partial class DefaultListView<TItem, TDetailForm> where TItem:class
    {
        [Inject]
        public IBaseDataService<TItem> DataService { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private DxGrid? _dataGrid;
        private IEnumerable<TItem>? _items;
        private string _searchText = string.Empty;
        private bool _detailViewPopupVisible = false;
        private TItem? _selectedItem;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await RefreshData();
        }

        private async Task HandleNewItem()
        {
            _selectedItem = default;
            _detailViewPopupVisible = true;
        }

        private async Task RefreshData()
        {
            _items = await DataService.GetAll(_searchText);
        }

        private async Task HandleSearchTextChanged(string newSearchText)
        {
            _searchText = newSearchText;
            await RefreshData();
        }

        private void HandleClickColumnChooser()
        {
            _dataGrid?.ShowColumnChooser(".column-chooser-button");
        }

        private async Task HandleDetailViewPopupClosed(bool saved)
        {
            _detailViewPopupVisible = false;
            if (saved) await RefreshData();
        }

        private async Task HandleEditItem()
        {
            _detailViewPopupVisible = true;
        }

        private async Task HandleRemoveItem()
        {
            await DataService.Delete((Guid)(_selectedItem as IReferenceType).Id);
            await RefreshData();
        }

    }
}
