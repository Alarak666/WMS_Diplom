using System.Reflection;
using Microsoft.AspNetCore.Components;
using WMS.Core.Services.BaseServices;

namespace WMS.UI.Shared.Components
{
    public partial class LookupCatalog<TItem> where TItem : class
    {
        private Guid? _selectedItemId;
        [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
        [Parameter] public string CssClass { get; set; }
        [Parameter] public bool Enabled { get; set; } = true;
        [Parameter] public bool ReadOnly { get; set; } = false;
        [Parameter] public IEnumerable<TItem>? Items { get; set; }

        [Parameter]
        public Guid? SelectedItemId
        {
            get => _selectedItemId;
            set
            {
                if (_selectedItemId == value) return;
                _selectedItemId = value;
                SelectedItemIdChanged.InvokeAsync(value);
            }
        }

        [Parameter] public EventCallback<Guid?> SelectedItemIdChanged { get; set; }

        [Inject] public IBaseDataService<TItem> DataService { get; set; }

        private IEnumerable<TItem> _items;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            if (Items != null)
            {
                _items = Items;
            }
            else
            {
                //Todo: catalog
                //if (typeof(TItem).GetTypeInfo().BaseType == typeof(BaseCatalog).GetTypeInfo())
                //{
                //    _items = await DataService.GetAll(string.Empty, typeof(BaseCatalog));
                //}
                //else
                //{
                //    _items = await DataService.GetAll(string.Empty);
                //}
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            await LoadData();
        }
    }
}