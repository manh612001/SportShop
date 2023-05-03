using SportShop.ViewModels.Cart;

namespace SportShop.Interface
{
    public interface ICartService
    {
        Task SaveCart(CartViewModel model);
    }
}
