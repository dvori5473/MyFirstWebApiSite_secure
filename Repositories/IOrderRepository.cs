using MyFirstWebApiSite;

namespace Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
    }
}