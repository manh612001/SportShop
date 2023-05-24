using SportShop.Models;

namespace SportShop.ViewModels.Cart
{
    public class CartViewModel
    {
        public List<CartItems> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
