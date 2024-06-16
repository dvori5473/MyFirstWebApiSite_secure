using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ProductRepositoryIntegrationTest:IClassFixture<DatabaseFixture>
    {
    
    
        private readonly AdoNetMarketContext _dbContext;
        private readonly ProductRepository _ProductRepository;
        public ProductRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _ProductRepository = new ProductRepository(_dbContext);
        }
    }
}
