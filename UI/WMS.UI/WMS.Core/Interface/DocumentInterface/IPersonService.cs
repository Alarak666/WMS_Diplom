using WMS.Core.Models.DocumentModels.Employes;
using WMS.Core.Models.DocumentModels.Persons;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<PersonDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(PersonDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(PersonDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
