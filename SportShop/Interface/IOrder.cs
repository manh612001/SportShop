using SportShop.Models;

namespace SportShop.Interface
{
    public interface IOrder
    {
        Task<List<Order>> GetAll();
    }
}
