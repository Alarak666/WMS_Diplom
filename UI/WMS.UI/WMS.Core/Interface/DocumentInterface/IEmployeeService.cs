using WMS.Core.Models.DocumentModels.Employes;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<EmployeeDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(EmployeeDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(EmployeeDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
