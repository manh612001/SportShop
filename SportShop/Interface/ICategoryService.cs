using SportShop.Models;
using SportShop.ViewModels.Category;

namespace SportShop.Interface
{
    public interface ICategoryService
    {
        Task Add(CategoryViewModel model);
        Task<List<CategoryViewModel>> GetAll();

        Task<CategoryViewModel> GetById(int? id);
        Task Update(CategoryViewModel model);
        Task Delete(int? id);
    }
}
