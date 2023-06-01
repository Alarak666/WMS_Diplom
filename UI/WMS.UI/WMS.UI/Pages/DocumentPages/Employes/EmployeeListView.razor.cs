using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using WMS.Core.DTO.EmployeeDtos;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Employes
{
    public partial class EmployeeListView : BaseListPage
    {
        //[Inject] public IEmployeeService _employeeService { get; set; }
        private IEnumerable<EmployeeDto>? _items;
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
          //  _items = await _employeeService.GetListViewItems(_searchText);
        }

        private async Task HandleDetailViewPopupClosed(bool saved)
        {
            _detailViewPopupVisible = false;
            if (saved) await LoadData();
        }

        private async Task HandleEditItem()
        {
            if (_selectedItemId == null) return;
            _detailViewPopupVisible = true;
            _selectedItemId = null;
        }

        private async Task HandleRemoveItem()
        {
            if (_selectedItemId == null) return;
          //  await _employeeService.Delete((Guid)_selectedItemId);
            await LoadData();
            _selectedItemId = null;
        }
        private async Task HandleDoubleClickRow(GridRowClickEventArgs arg)
        {
            //_selectedItemId = (_dataGrid.SelectedDataItem as EmployeeListViewModel)?.Id;
            await HandleNewItem();
        }
        private void HandleCloneItem()
        {
            throw new NotImplementedException();
        }
    }
}
