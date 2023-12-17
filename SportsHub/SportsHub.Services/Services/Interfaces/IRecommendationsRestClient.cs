using SportsHub.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Services.Services.Interfaces
{
    public interface IRecommendationsRestClient
    {
        Task<List<RecommendationDto>> GetRecommendationsAsync(string userId, int count);
        Task<bool> LikeAsync(string userId, int trainingProgramId, int rating);
        Task<int?> GetRatingAsync(string userId, int trainingProgramId);
    }
}
