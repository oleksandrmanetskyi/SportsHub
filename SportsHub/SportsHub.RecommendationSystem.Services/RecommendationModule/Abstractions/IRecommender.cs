using Microsoft.EntityFrameworkCore;
using SportsHub.RecommendationSystem.Services;
using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsHub.RecommendationSystem.Services.Database;

namespace SportsHub.RecommendationSystem.RecommendationModule.Abstractions
{
    public interface IRecommender
    {
        void Train(RatingsData db);

        List<Suggestion> GetSuggestions(string userId, int numSuggestions, int persentsOfSuggestions = 50, IEnumerable<Suggestion> excludeSuggestions = null);

        double GetRating(string userId, int trainingProgramId);

        void Save(string file);
        void SaveToDatabase(RecommendationsDbContext database);

        void Load(string file);
        void LoadFromDatabase(RecommendationsDbContext database, RatingsData db);
    }
}
