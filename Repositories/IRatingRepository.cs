using MyFirstWebApiSite;

namespace Repositories
{
    public interface IRatingRepository
    {
        Task<Rating> AddRating(Rating rating);
    }
}