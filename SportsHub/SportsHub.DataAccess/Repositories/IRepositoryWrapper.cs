using System.Threading.Tasks;
using SportsHub.DataAccess.Repositories.Interfaces;

namespace SportsHub.DataAccess.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        INewsRepository News { get; }
        INutritionRepository Nutrition { get; }
        IShopRepository Shop { get; }
        ISportKindRepository SportKind { get; }
        ISportPlaceRepository SportPlace { get; }
        ITrainingProgramRepository TrainingProgram { get; }
        IRecommendationRepository Recommendation { get; }
        Task SaveAsync();
    }
}
