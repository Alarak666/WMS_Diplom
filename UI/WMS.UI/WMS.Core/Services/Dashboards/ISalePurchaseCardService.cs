using WMS.Core.Models.Dashboards;

namespace WMS.Core.Services.Dashboards;

public interface ISalePurchaseCardService
{
    Task<IEnumerable<DashboardSaleTurnoverItem>> GetData(DateTime beginDate, DateTime endDate);
}