using DevExpress.Blazor.Reporting;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Components;
using WMS.Core.Interface.DocumentInterface;
using WMS.UI.ReportEditor;

namespace WMS.UI.Pages.Dashboards.BaseDocument
{
    public partial class ProductReportViwer
    {
        DxReportViewer? reportViewer;
        XtraReport? Report { get; set; }
        [Inject] IProductService ProductService { get; set; }
            
        
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Report = new ProductReport();
        }
    }
}
