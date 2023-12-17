using SportsHub.RecommendationSystem.Services.RecommendationModule.Objects;

namespace SportsHub.RecommendationSystem.Services.Database
{
    public interface ISportsHubRepository
    {
        List<string> GetAllUserIds();
        List<int> GetAllTrainingProgramIds();
        List<SuggestionDto> GetAllTrainingProgramsFromRecommendations();
    }
}