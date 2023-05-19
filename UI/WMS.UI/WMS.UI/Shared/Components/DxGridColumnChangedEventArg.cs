using DevExpress.Blazor;

namespace WMS.UI.Shared.Components
{
    public class DxGridColumnChangedEventArg<T>
    {
        public T NewValue { get; set; } = default!;
        public GridDataColumnCellEditTemplateContext Context { get; set; }
    }
}
