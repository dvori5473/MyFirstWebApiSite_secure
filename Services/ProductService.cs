using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstWebApiSite;
using Repositories;

namespace Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<MyFirstWebApiSite.Product>> GetProducts(int? minPrice, int? maxPrice, int?[] categories, string? descrebtion)
        {

            return await _productRepository.GetProducts(minPrice, maxPrice, categories, descrebtion);
        }
        public async Task<Product> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }
    }
}
