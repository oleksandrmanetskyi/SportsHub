using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using SportsHub.RecommendationSystem.Services.RecommendationModule.Objects;

namespace SportsHub.RecommendationSystem.Services
{
    public interface IRecommendationsService
    {
        void PerformNewRecommendations();
        Task LikeAsync(string userId, int trainingProgramId, int rating);
        List<SuggestionDto> GetRecommendations(string userId, int suggestionsCount);
        bool HasNewRecords();
        Task<int?> GetRatingAsync(string userId, int trainingProgramId);
    }
}