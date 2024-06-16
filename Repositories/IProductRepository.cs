using MyFirstWebApiSite;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int? minPrice, int? maxPrice, int?[] categories, string? descrebtion);
        Task<Product> GetById(int id);
    }
}