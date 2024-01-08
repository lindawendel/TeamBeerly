using HealthCare.Core.Data;
using HealthCare.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core
{
    public class RatingService

    {
        private readonly HealthCareContext _dbContext;

        public RatingService(HealthCareContext dbContext)
        {
            _dbContext = dbContext;

            // Ensure the database is created
            _dbContext.Database.EnsureCreated();
        }

        public IEnumerable<Rating> GetAllRatings()
        {
            return _dbContext.Ratings.ToList();
        }

        public void AddRating(Rating rating)
        {
            _dbContext.Ratings.Add(rating);
            _dbContext.SaveChanges();
        }

    }
}
