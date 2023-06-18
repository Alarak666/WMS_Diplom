using DevExpress.Blazor.Reporting;
using DevExpress.XtraReports.UI;
using WMS.UI.ReportEditor;

namespace WMS.UI.Pages.Dashboards.BaseDocument
{
    public partial class AcceptanceOfGoodReportViwer
    {
        DxReportViewer? reportViewer;
        XtraReport? Report { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Report = new AcceptanceofGoodReport();
        }
    }
}
