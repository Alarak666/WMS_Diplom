using WMS.Core.Models.Dashboards;

namespace WMS.Core.Services.Dashboards;

public interface IBankTurnoverCardService
{
    Task<IEnumerable<DashboardBankTurnoverItem>> GetData(DateTime currentMonth);
}