using WMS.Core.Models;
using WMS.Core.Models.Report;
using WMS.UI.Services.DocumentService.AcceptanceOfGoodServices;
using WMS.UI.Services.ReportService;

namespace WMS.UI.ReportEditor
{
    public partial class AcceptanceofGoodReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AcceptanceofGoodReport()
        {
            InitializeComponent();
            BeforePrint += StockTurnoverReport_BeforePrint;
        }


        private void StockTurnoverReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Report.DataSource = Task.Run(async () => await GetReportData()).Result;
        }

        private async Task<IEnumerable<AcceptanceofGoodReportDto>> GetReportData()
        {
            var serviceScope = ApplicationAccess.ServiceProvider?.CreateScope();
            var _dataService = serviceScope?.ServiceProvider.GetRequiredService<AcceptanceofGoodReportService>();
            var dataList = await _dataService.ReportData();
            return dataList;
        }
    }
}
