
using SportShop.ViewModels.Category;

namespace SportShop.ViewModels.HomeCustomer
{
    public class HomeCustomer
    {
        public List<CategoryViewModel> Categories { get; set; }
        public List<Product.ProductViewModel>? NewProduct { get; set; }
    }
}
