using SportShop.Models;

namespace SportShop.ViewModels.Cart
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
