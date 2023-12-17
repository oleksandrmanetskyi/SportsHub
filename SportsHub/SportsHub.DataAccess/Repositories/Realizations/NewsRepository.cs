using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories.Interfaces;

namespace SportsHub.DataAccess.Repositories.Realizations
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(SportsHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}
