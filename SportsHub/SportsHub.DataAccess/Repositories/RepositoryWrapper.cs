using SportsHub.DataAccess.Repositories.Interfaces;
using SportsHub.DataAccess.Repositories.Realizations;
using System.Threading.Tasks;

namespace SportsHub.DataAccess.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private readonly SportsHubDbContext _dbContext;
        private IUserRepository _user;
        private INewsRepository  _newsRepository;
        private INutritionRepository _nutritionRepository;
        private IShopRepository _shopRepository;
        private ISportKindRepository _sportKindRepository;
        private ISportPlaceRepository _sportPlaceRepository;
        private ITrainingProgramRepository _trainingProgramRepository;
        private IRecommendationRepository _recommendationRepository;

        public RepositoryWrapper(SportsHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository User
        {
            get { return _user ??= new UserRepository(_dbContext); }
        }
        public INewsRepository News
        {
            get { return _newsRepository ??= new NewsRepository(_dbContext); }
        }

        public INutritionRepository Nutrition
        {
            get { return _nutritionRepository ??= new NutritionRepository(_dbContext); }
        }

        public IShopRepository Shop
        {
            get { return _shopRepository ??= new ShopRepository(_dbContext); }
        }

        public ISportKindRepository SportKind
        {
            get { return _sportKindRepository ??= new SportKindRepository(_dbContext); }
        }

        public ISportPlaceRepository SportPlace
        {
            get { return _sportPlaceRepository ??= new SportPlaceRepository(_dbContext); }
        }

        public ITrainingProgramRepository TrainingProgram
        {
            get { return _trainingProgramRepository ??= new TrainingProgramRepository(_dbContext); }
        }

        public IRecommendationRepository Recommendation
        {
            get { return _recommendationRepository ??= new RecommendationRepository(_dbContext); }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
