using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsHub.DataAccess.Repositories.Realizations
{
    public class RecommendationRepository : RepositoryBase<Recommendation>, IRecommendationRepository
    {
        public RecommendationRepository(SportsHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}
