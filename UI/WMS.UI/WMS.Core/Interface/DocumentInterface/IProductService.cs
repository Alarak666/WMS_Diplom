using WMS.Core.Models.DocumentModels.Products;

namespace WMS.Core.Interface.DocumentInterface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListViewModel>?> GetListViewItems(string? searchText, CancellationToken cancellation);
        Task<ProductDetailViewModel?> GetDetailViewData(Guid? id, CancellationToken cancellation);
        Task<bool> SaveDetailViewModel(ProductDetailViewModel? model, CancellationToken cancellation);
        Task<bool> UpdateDetailViewModel(ProductDetailViewModel model, CancellationToken cancellation);
        Task<bool> DeleteDetailViewModel(Guid? id, CancellationToken cancellation);
    }
}
