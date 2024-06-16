using MyFirstWebApiSite;

namespace Services
{
    public interface IRatingService
    {
        Task<Rating> AddRating(Rating raiting);
    }
}