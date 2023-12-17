using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories.Interfaces;

namespace SportsHub.DataAccess.Repositories.Realizations
{
    public class NutritionRepository : RepositoryBase<Nutrition>, INutritionRepository
    {
        public NutritionRepository(SportsHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}
