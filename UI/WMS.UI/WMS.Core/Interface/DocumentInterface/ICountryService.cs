using WMS.Core.Models.DocumentModels.CountryModels;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<CountryDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(CountryDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(CountryDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
