using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApiSite;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private AdoNetMarketContext _AdoNetMarket;

        public ProductRepository(AdoNetMarketContext AdoNetMarket)
        {
            _AdoNetMarket = AdoNetMarket;
        }
        public async Task<List<MyFirstWebApiSite.Product>> GetProducts(int? minPrice, int? maxPrice, int?[] categories, string? descrebtion)

        {
            try
            {
                var query = _AdoNetMarket.Products.Where(product =>
                (descrebtion == null ? (true) : (product.ProductName.Contains(descrebtion)))
                && ((minPrice == null) ? (true) : (product.Price >= minPrice))
                && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
                && ((categories.Length == 0) ? (true) : (categories.Contains(product.CategoryId))))
                .OrderBy(product => product.Price);
                List<MyFirstWebApiSite.Product> products = await query.ToListAsync();
                return products;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Product> GetById(int id)

        {
            try
            {
                return await _AdoNetMarket.Products.FindAsync(id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
