using MyFirstWebApiSite;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
    }
}