using WMS.Core.Models.Dashboards;

namespace WMS.Core.Interface;

public interface IDashboardProvider
{
    Task<IEnumerable<DashBoardPalletDto>> GetSalesAsync(CancellationToken ct = default);
}