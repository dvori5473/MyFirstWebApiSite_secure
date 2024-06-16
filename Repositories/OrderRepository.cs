using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApiSite;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private AdoNetMarketContext _AdoNetMarket;

        public OrderRepository(AdoNetMarketContext AdoNetMarket)
        {
            _AdoNetMarket = AdoNetMarket;
        }
        public async Task<MyFirstWebApiSite.Order> AddOrder(Order order)

        {
            try
            {
                await _AdoNetMarket.Orders.AddAsync(order);
                await _AdoNetMarket.SaveChangesAsync();
                return order;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
