using SportShop.ViewModels.Product;

namespace SportShop.Interface
{
    public interface IProductService
    {
        Task Add(ProductViewModel model);
        Task Delete(int? id);
        Task Update(ProductViewModel model);
        Task<DetailProductViewModel> GetById(int? id);
        Task<List<ListProductViewModel>> GetAll();
        Task<List<ProductViewModel>> GetProductsByCategory(int? id);
        Task<List<ProductViewModel>> GetProductsByCaTegory(string? key);
    }
}
