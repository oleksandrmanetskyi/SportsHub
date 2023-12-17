using SportsHub.RecommendationSystem.Services;
using SportsHub.RecommendationSystem.RecommendationModule.Abstractions;
using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsHub.RecommendationSystem.Services.Database;

namespace SportsHub.RecommendationSystem.RecommendationModule.Recommenders
{
    public class RandomRecommender : IRecommender
    {
        private Random rand;
        RatingsData _db;

        public RandomRecommender(RatingsData db)
        {
            rand = new Random();
            _db = new RatingsData
            {
                Ratings = db.Ratings,
                TrainingProgramIds = db.TrainingProgramIds,
                UserIds = db.UserIds
            };
        }

        public void Train(RatingsData db)
        {
        }

        public double GetRating(string userId, int trainingProgramId)
        {
            return rand.NextDouble() * 5.0;
        }

        public List<Suggestion> GetSuggestions(string userId, int numSuggestions, int persentsOfSuggestions = 50, IEnumerable<Suggestion> excludeSuggestions = null)
        {
            int minSuggestions = 1;
            numSuggestions = (numSuggestions * persentsOfSuggestions) / 100 > minSuggestions ? (numSuggestions * persentsOfSuggestions) / 100 : minSuggestions;
            List<Suggestion> suggestions = new List<Suggestion>();
            var programs = _db.TrainingProgramIds;
            if (excludeSuggestions != null)
            {
                programs = programs.Except(excludeSuggestions.Select(y => y.TrainingProgramId)).ToList();
            }
            int maxCount = programs.Count();
            for (int i = 0; i < numSuggestions && i < maxCount; i++)
            {
                int programInd = rand.Next(0, maxCount);
                if (suggestions.Select(x => x.TrainingProgramId).Contains(programs[programInd]))
                {
                    i--;
                    continue;
                }
                suggestions.Add(new Suggestion(userId, programs[programInd], rand.NextDouble() * 5.0));
            }

            return suggestions;
        }

        public void Load(string file)
        {
            
        }

        public void Save(string file)
        {
            
        }

        public void SaveToDatabase(RecommendationsDbContext database)
        {
            throw new NotImplementedException();
        }

        public void LoadFromDatabase(RecommendationsDbContext database, RatingsData db)
        {
            throw new NotImplementedException();
        }
    }
}
