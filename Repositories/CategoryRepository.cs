using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private AdoNetMarketContext _AdoNetMarket;

        public CategoryRepository(AdoNetMarketContext AdoNetMarket)
        {
            _AdoNetMarket = AdoNetMarket;
        }
        public async Task<List<MyFirstWebApiSite.Category>> GetCategories()

        {
            try
            {
                List<MyFirstWebApiSite.Category> categories = await _AdoNetMarket.Categories.ToListAsync();
                return categories;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
