using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories.Interfaces;

namespace SportsHub.DataAccess.Repositories.Realizations
{
    public class SportPlaceRepository: RepositoryBase<SportPlace>, ISportPlaceRepository
    {
        public SportPlaceRepository(SportsHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}
