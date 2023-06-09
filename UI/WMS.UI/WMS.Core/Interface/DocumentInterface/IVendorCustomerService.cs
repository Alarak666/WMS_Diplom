using WMS.Core.Models.DocumentModels.VendorCustomers;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IVendorCustomerService
    {
        Task<IEnumerable<VendorCustomerListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<VendorCustomerDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(VendorCustomerDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(VendorCustomerDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
