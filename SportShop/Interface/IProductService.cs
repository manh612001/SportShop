using SportShop.ViewModels.Product;

namespace SportShop.Interface
{
    public interface IProductService
    {
        Task Add(ProductViewModel model);
        Task Delete(int? id);
        Task Update(ProductViewModel model);
        Task<ProductViewModel> GetById(int? id);
        Task<List<ListProductViewModel>> GetAll();
    }
}
