using Microsoft.AspNetCore.Components;
using WMS.Core.Services.Framework;

namespace WMS.UI.Shared.Components;

public partial class BaseListViewComponent
{
   // [Inject] public IDocumentListService<CustomerInvoiceListModel> ListDataService { get; set; }
    [Inject] public IAppFramework AppFramework { get; set; }
    private IAppFrameworkEntity AppFrameworkEntity { get; set; } = default!;

//    private IEnumerable<CustomerInvoiceListModel>? _items;

    private bool _detailViewPopupVisible;
    private DynamicComponent? _detailView;
    // private bool _commodityWaybillDetailViewVisible;

    public Guid? SelectedItemId { get; set; } = default!;
    private bool _isFailure = false;
    private string? _errorMessage = default!;

    // private CreateBasedAtCustomerInvoiceModel? _createBasedAtCustomerInvoiceModel;

    [Parameter] public RenderFragment ToolbarContent { get; set; }
    [Parameter] public RenderFragment GridContent { get; set; }

    // protected override async Task<bool> Validate()
    // {
    //     // ValidationResult = await DocumentDataService.DoPostValidate(_selectedItemId);
    //     // return ValidationResult.IsValid;
    //     return true;
    // }
    public void HandleNewItem()
    {
        SelectedItemId = default!;
        _detailViewPopupVisible = true;
        StateHasChanged();
    }

    protected override  async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    //    AppFrameworkEntity = AppFramework.GetEntity(typeof(CustomerInvoice));
    }

    protected virtual async Task LoadData()
   {
        //     
        //     _items = await ListDataService.GetListViewItems(_searchText);
        //     StateHasChanged();
        // }
        //
        // private async Task HandleDetailViewPopupClosed(bool saved)
        // {
        //     DetailViewPopupVisible = false;
        //      await LoadData();
    }

    protected virtual async Task HandleEditItem()
    {
        // if (_selectedItemId == null) return;
        // DetailViewPopupVisible = true;
        // _selectedItemId = null;
    }

    //
    protected virtual async Task HandleRemoveItem()
    {
        //     if (_selectedItemId == null) return;
        //     //await DocumentDataService.Delete((Guid)_selectedItemId);
        //     await LoadData();
        //     _selectedItemId = default!;
    }

    //
    protected virtual async Task HandlePostDocument()
    {
        //     if (_selectedItemId == null) return;
        //     await PostObject();
    }

    //
    protected virtual async Task Post()
    {
        //     //await DocumentDataService.Post((Guid)_selectedItemId);
    }

    //
    protected virtual async Task HandleCancelDocument()
    {
        //     if (_selectedItemId == null) return;
        //     //await DocumentDataService.CancelPost((Guid)_selectedItemId);
    }

    //
    // private async Task HandleCreatedCommodityWaybill(bool arg)
    // {
    //     //_commodityWaybillDetailViewVisible = false;
    //     _createBasedAtCustomerInvoiceModel = null;
    // }
    //
    // private async Task HandleCreateCommodityWaybill(ToolbarItemClickEventArgs arg)
    // {
    //     _createBasedAtCustomerInvoiceModel = new CreateBasedAtCustomerInvoiceModel()
    //     {
    //         CustomerInvoiceId = _selectedItemId,
    //     };
    //     //_commodityWaybillDetailViewVisible = true;
    // }
    //
    // private async Task HandleDoubleClickRow(GridRowClickEventArgs arg)
    // {
    //     _selectedItemId = (_dataGrid.SelectedDataItem as CustomerInvoiceListModel)?.Id;
    //     await HandleNewItem();
    // }
    //
    protected virtual void HandleCloneItem()
    {
        throw new NotImplementedException();
    }
    //
    // private async Task HandleCreateDocumentBaseAt()
    // {
    //     var _componentType = typeof(CustomerInvoice).Name;
    //     await JsRuntime.InvokeVoidAsync("open", $"/documentBaseAt/{_componentType}/{_selectedItemId}", "_blank");
    // }
}