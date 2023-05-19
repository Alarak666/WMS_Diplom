﻿using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using WMS.Core.Services.BaseServices;
using WMS.UI.Model;

namespace WMS.UI.Shared.FormComponents;

public partial class FormLookupCatalog<TItem> where TItem : class
{
    private bool _isOpen = false;

    private bool IsOpen
    {
        get => _isOpen;
        set
        {
            if (_isOpen == false && value == true)
            {
                _firstOpen = true;
            }
            else
            {
                _firstOpen = false;
            }

            _isOpen = value;
        }
    }

    [Inject] public IBaseDataService<TItem> DataService { get; set; }
    [Parameter] public string? Placeholder { get; set; } = "select something...";
    [Parameter] public bool ShowClearButton { get; set; } = true;
    [Parameter] public bool ShowEditButton { get; set; } = true;
    [Parameter] public bool ShowCreateButton { get; set; } = true;
    [Parameter] public bool ReadOnly { get; set; } = false;
    [Parameter] public bool Enabled { get; set; } = true;
    [Parameter] public bool? IsValid { get; set; }
    [Parameter] public string? SearchableColumns { get; set; }
    [Parameter] public Func<Task<Dictionary<string, object?>>>? PrepareNewObjectParams { get; set; }
    [Parameter] public Func<string?, Task<IEnumerable<TItem>>>? ServerData { get; set; }
    [Parameter] public RenderFragment<TItem> Columns { get; set; }
    [Parameter] public Type? DetailViewType { get; set; }
    private bool _firstOpen = true;
    private Guid? _selectedItemId;
    private DynamicComponent? _detailViewRef;
    private IDictionary<string, object?> _parameters = new Dictionary<string, object?>();
    private bool _isDetailViewOpen; 

    [Parameter]
    public Guid? SelectedItemId
    {
        get => _selectedItemId;
        set
        {
            _selectedItemId = value;
            UpdateCurrentSelectedItemData();
        }
    }

    [Parameter] public EventCallback<Guid?> SelectedItemIdChanged { get; set; }

    private string? ContainerClass
    {
        get
        {
            var containerClass = "form-lookup-catalog-input-container";
            if (IsValid.HasValue)
            {
                containerClass = IsValid.Value
                    ? " form-lookup-catalog-input-container-valid"
                    : "form-lookup-catalog-input-container-invalid";
            }
            else
            {
                if (IsOpen)
                {
                    containerClass = "form-lookup-catalog-input-container-focused";
                }
            }

            return containerClass;
        }
    }

    private IEnumerable<TItem> _items;
    private string? _searchText;
    private string? _componentId;

    protected override async Task OnInitializedAsync()
    {
        _componentId = Guid.NewGuid().ToString();
        await UpdateCurrentSelectedItemData();
    }

    private async Task UpdateCurrentSelectedItemData()
    {
        if (SelectedItemId != null)
        {
            var item = await DataService.Get(SelectedItemId.Value);
            if (item != null)
            {
                //_searchText = (item as BaseCatalog)?.Name;
            }
        }
        else
        {
            _searchText = string.Empty;
        }

        await InvokeAsync(StateHasChanged);
    }

    private async Task HandleClickOpenFlyout()
    {
        if (ReadOnly || !Enabled)
        {
            return;
        }
        
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            await LoadData();
        }
    }

    private async Task LoadData()
    {
        if (!_isOpen) return;
        if (ServerData != null)
        {
            _items = await ServerData(_searchText);
        }
        else
        {
            var searchText = _firstOpen ? string.Empty : _searchText;
            // if (typeof(TItem).GetTypeInfo().BaseType == typeof(BaseCatalog).GetTypeInfo())
            // {
            //     _items = await DataService.GetTop(searchText, typeof(BaseCatalog), 20);
            // }
            // else
            // {
                _items = await DataService.GetTop(searchText, 20, SearchableColumns);
            // }
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        await UpdateCurrentSelectedItemData();
    }

    private async Task HandleSelectItem(TItem selectedItem)
    {
        if (ReadOnly) return;
        IsOpen = false;
       // await SelectedItemIdChanged.InvokeAsync((selectedItem as BaseCatalog)?.Id);
    }

    private async Task HandleSearchTextChange(ChangeEventArgs arg)
    {
        if (ReadOnly) return;
        _searchText = arg.Value?.ToString();
        if (_isOpen)
        {
            _firstOpen = false;
        }

        IsOpen = true;
        await LoadData();
    }

    private async Task HandleClearSearchText()
    {
        if (ReadOnly || !Enabled) return;
        _searchText = string.Empty;
        await SelectedItemIdChanged.InvokeAsync(null);
        if (IsOpen)
        {
            await LoadData();
        }
    }

    private async Task HandlePopupClosed(FlyoutClosedEventArgs arg)
    {
        if (SelectedItemId == null)
        {
            _searchText = "";
        }
    }

    private async Task HandleCreateNew()
    {
        _parameters.Clear();
        _parameters.Add("IsVisible", true);
        _parameters.Add("ViewClosed", EventCallback.Factory.Create<ViewClosedEventArgs>(this, HandlePopupClosedNew));

        if (PrepareNewObjectParams != null)
        {
            var additionalParams = await PrepareNewObjectParams();
            foreach (var param in additionalParams)
            {
                _parameters.Add(param.Key, param.Value);
            }
        }

        _isDetailViewOpen = true;
    }

    private async Task HandlePopupClosedOld(bool o)
    {
        _isDetailViewOpen = false;
    }

    private async Task HandlePopupClosedNew(ViewClosedEventArgs eventArgs)
    {
        _isDetailViewOpen = false;
        if (eventArgs.Saved)
        {
            IsOpen = false;
            await SelectedItemIdChanged.InvokeAsync(eventArgs.ObjectId);
        }
    }

    private async Task HandleClickOpenObject()
    {
        if (SelectedItemId == null) return;
        
        _parameters.Clear();
        _parameters.Add("IsVisible", true);
        _parameters.Add("SelectedItemId", SelectedItemId);
        _parameters.Add("ViewClosed", EventCallback.Factory.Create<ViewClosedEventArgs>(this, HandlePopupClosedNew));
        
        _isDetailViewOpen = true;
    }
}