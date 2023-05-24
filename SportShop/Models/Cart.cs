namespace SportShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal grandTotal { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
