using WMS.Core.Models.DocumentModels.OrderModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<OrderDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<string> SaveDetailViewModel(OrderDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(OrderDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
