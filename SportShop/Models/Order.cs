using Microsoft.Data.SqlClient.DataClassification;

namespace SportShop.Models
{
    public class Order
    {
        public int CartId { get; set; }
        public int CartItemId { get; set; }
        

        public Cart Cart { get; set; }
        public CartItems CartItem { get; set; }
    }
}
