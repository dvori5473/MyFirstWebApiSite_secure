using MyFirstWebApiSite;

namespace Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(int? minPrice, int? maxPrice, int?[] categories, string? descrebtion);
        Task<Product> GetById(int id);
    }
}