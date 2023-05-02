using SportShop.ViewModels.Category;
using SportShop.ViewModels.HomeCustomer;

namespace SportShop.Interface
{
    public interface IHomeService
    {
        Task<List<CategoryViewModel>> GetCategories();

        Task<HomeCustomer> HomePage();
    }
}
