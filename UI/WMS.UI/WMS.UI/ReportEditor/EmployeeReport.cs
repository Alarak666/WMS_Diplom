using WMS.Core.Models;
using WMS.Core.Models.Report;
using WMS.UI.Services.ReportService;

namespace WMS.UI.ReportEditor
{
    public partial class EmployeeReport : DevExpress.XtraReports.UI.XtraReport
    {
        public EmployeeReport()
        {
            InitializeComponent();
            BeforePrint += StockTurnoverReport_BeforePrint;
        }


        private void StockTurnoverReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Report.DataSource = Task.Run(async () => await GetReportData()).Result;
        }

        private async Task<IEnumerable<EmployeeReportDto>> GetReportData()
        {
            var serviceScope = ApplicationAccess.ServiceProvider?.CreateScope();
            var _dataService = serviceScope?.ServiceProvider.GetRequiredService<EmploeeReportService>();
            var dataList = await _dataService.ReportData();
            return dataList;
        }
    }
}
