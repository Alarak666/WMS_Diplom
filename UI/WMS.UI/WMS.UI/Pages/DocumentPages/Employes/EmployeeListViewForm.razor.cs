using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.Core.Models.DocumentModels.Employes;
using WMS.UI.Shared;

namespace WMS.UI.Pages.DocumentPages.Employes
{
    public partial class EmployeeListViewForm : BaseListPage
    {
        [Inject] public IEmployeeService _employeeService { get; set; }
        public string? selectedOption { get; set; }
        bool isCheckedF;
        bool isCheckedM;
        bool isCheckedL;
        private void HandleCheckboxChange(bool value)
        {
            if (isCheckedF)
            {
                isCheckedM = false;
                isCheckedL = false;
            }
            else if (isCheckedM)
            {
                isCheckedF = false;
                isCheckedL = false;
            }
            else if (isCheckedL)
            {
                isCheckedF = false;
                isCheckedM = false;
            }
        }
        private IEnumerable<EmployeeListViewModel>? _items;
        private bool _detailViewPopupVisible = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task HandleNewItem()
        {
            _selectedItemId = default;
            _detailViewPopupVisible = true;
        }

        protected override async Task LoadData()
        {
            _items = await _employeeService.GetListViewItems(_searchText, cancellationToken);
        }

        private async Task HandleDetailViewPopupClosed(bool saved)
        {
            _detailViewPopupVisible = false;
            _selectedItemId = default;
            _dataGrid.ClearSelection();
            await LoadData();
        }

        private async Task HandleEditItem()
        {
            if (_selectedItemId == null) return;
            _detailViewPopupVisible = true;
        }

        private async Task HandleRemoveItem()
        {
            if (_selectedItemId == null) return;
            await _employeeService.DeleteDetailViewModel((Guid)_selectedItemId, cancellationToken);
            await LoadData();
            _selectedItemId = null;
        }
        private async Task HandleDoubleClickRow(GridRowClickEventArgs arg)
        {
            //_selectedItemId = (_dataGrid.SelectedDataItem as EmployeeListViewModel)?.Id;
            await HandleNewItem();
        }

        protected override async Task HandleSearchTextChanged(string newSearchText)
        {
            // await base.HandleSearchTextChanged(newSearchText);
            _items = await _employeeService.GetListViewItems(newSearchText, cancellationToken, selectedOption);
        }

        private void HandleCloneItem()
        {
            throw new NotImplementedException();
        }
    }
}
