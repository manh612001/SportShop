using SportShop.Interface;
using SportShop.Models;

namespace SportShop.Service
{
    public class OrderService : IOrder
    {
        private readonly DatabaseDbContext _context;
        public OrderService(DatabaseDbContext context)
        {
            _context = context;
        }

        public  async Task<List<Order>> GetAll()
        {
            var result =  await _context.Orders.GroupBy();
        }
    }
}
