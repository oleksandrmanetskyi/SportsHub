using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories.Interfaces;

namespace SportsHub.DataAccess.Repositories.Realizations
{
    public class ShopRepository: RepositoryBase<Shop>, IShopRepository
    {
        public ShopRepository(SportsHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}
