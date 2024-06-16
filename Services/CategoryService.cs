using Repositories;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<MyFirstWebApiSite.Category>> GetCategories()
        {

            return await _categoryRepository.GetCategories();
        }
    }
}
