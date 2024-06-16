using MyFirstWebApiSite;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<Rating> AddRating(Rating raiting)
        {
            
            return await _ratingRepository.AddRating(raiting);
        }
    }
}
