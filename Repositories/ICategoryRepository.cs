using Entities;

namespace Repositories
{
    public interface ICategoryRepository
    {
        Task<List<MyFirstWebApiSite.Category>> GetCategories();
    }
}