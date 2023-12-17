using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories.Interfaces;

namespace SportsHub.DataAccess.Repositories.Realizations
{
    public class SportKindRepository: RepositoryBase<SportKind>, ISportKindRepository
    {
        public SportKindRepository(SportsHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}
