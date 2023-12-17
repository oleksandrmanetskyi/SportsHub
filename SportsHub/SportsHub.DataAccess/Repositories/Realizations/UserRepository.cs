using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories.Interfaces;

namespace SportsHub.DataAccess.Repositories.Realizations
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserRepository(SportsHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}
