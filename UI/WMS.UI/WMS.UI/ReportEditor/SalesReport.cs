using WMS.Core.Models;
using WMS.Core.Models.Report;
using WMS.UI.Services.ReportService;

namespace WMS.UI.ReportEditor
{
    public partial class SalesReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SalesReport()
        {
            InitializeComponent();
            BeforePrint += StockTurnoverReport_BeforePrint;
        }


        private void StockTurnoverReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Report.DataSource = Task.Run(async () => await GetReportData()).Result;
        }

        private async Task<IEnumerable<SalesReportDto>> GetReportData()
        {
            var serviceScope = ApplicationAccess.ServiceProvider?.CreateScope();
            var _dataService = serviceScope?.ServiceProvider.GetRequiredService<SalesReportService>();
            var dataList = await _dataService.ReportData();
            return dataList;
        }
    }
}
