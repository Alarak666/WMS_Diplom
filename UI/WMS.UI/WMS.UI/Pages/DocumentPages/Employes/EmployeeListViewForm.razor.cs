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
        private bool _isCheckedF;
        private bool _isCheckedM;
        private bool _isCheckedL;

        private bool isCheckedF
        {
            get => _isCheckedF;
            set
            {
                _isCheckedF = value;
                if (value)
                {
                    selectedOption = "F";
                    _isCheckedM = false;
                    _isCheckedL = false;
                }
                StateHasChanged();
            }
        }

        private bool isCheckedM
        {
            get => _isCheckedM;
            set
            {
                _isCheckedM = value;
                if (value)
                {
                    selectedOption = "M";
                    _isCheckedF = false;
                    _isCheckedL = false;
                }
                StateHasChanged();

            }
        }

        private bool isCheckedL
        {
            get => _isCheckedL;
            set
            {
                _isCheckedL = value;
                if (value)
                {
                    selectedOption = "L";
                    _isCheckedF = false;
                    _isCheckedM = false;
                }
                StateHasChanged();

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
