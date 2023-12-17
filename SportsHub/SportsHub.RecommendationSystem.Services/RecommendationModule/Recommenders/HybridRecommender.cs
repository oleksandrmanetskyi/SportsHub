using SportsHub.RecommendationSystem.Services;
using SportsHub.RecommendationSystem.RecommendationModule.Abstractions;
using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using SportsHub.RecommendationSystem.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.RecommendationModule.Recommenders
{
    public class HybridRecommender : IRecommender
    {
        private IRecommender _matrixFactorizationRecommender;
        private IRecommender _randomRecommender;

        public HybridRecommender(MatrixFactorizationRecommender matrixFactorizationRecommender, RandomRecommender randomRecommender)
        {
            _matrixFactorizationRecommender = matrixFactorizationRecommender;
            _randomRecommender = randomRecommender;
        }


        public void Train(RatingsData db)
        {
            _matrixFactorizationRecommender.Train(db);
            _randomRecommender.Train(db);
        }

        public double GetRating(string userId, int articleId)
        {
            double rating1 = _matrixFactorizationRecommender.GetRating(userId, articleId);
            double rating2 = _randomRecommender.GetRating(userId, articleId);
            return (rating1 + rating2) / 2;
        }

        public List<Suggestion> GetSuggestions(string userId, int numSuggestions, int persentsOfSuggestions = 50, IEnumerable<Suggestion> excludeSuggestions = null)
        {
            List<Suggestion> suggestions = new List<Suggestion>();

            suggestions.AddRange(_matrixFactorizationRecommender.GetSuggestions(userId, numSuggestions, 80, excludeSuggestions));
            suggestions.AddRange(_randomRecommender.GetSuggestions(userId, numSuggestions, 20, excludeSuggestions));
            

            suggestions.Sort((c, n) => n.Rating.CompareTo(c.Rating));

            return suggestions.Take(numSuggestions).ToList();
        }

        private List<Suggestion> GetCommonSuggestions(string userId, int numSuggestions)
        {
            int internalSuggestions = 100;

            List<List<Suggestion>> suggestions = new List<List<Suggestion>>();
            suggestions.Add(_matrixFactorizationRecommender.GetSuggestions(userId, internalSuggestions, 80));
            suggestions.Add(_randomRecommender.GetSuggestions(userId, internalSuggestions, 20, suggestions[0]));

            List<Tuple<Suggestion, int>> final = new List<Tuple<Suggestion, int>>();

            foreach (List<Suggestion> list in suggestions)
            {
                foreach (Suggestion suggestion in list)
                {
                    int existingIndex = final.FindIndex(x => x.Item1.TrainingProgramId == suggestion.TrainingProgramId);

                    if (existingIndex >= 0)
                    {
                        Suggestion highestRated = final[existingIndex].Item1.Rating > suggestion.Rating ? final[existingIndex].Item1 : suggestion;
                        final[existingIndex] = new Tuple<Suggestion, int>(highestRated, final[existingIndex].Item2 + 1);
                    }
                    else
                    {
                        final.Add(new Tuple<Suggestion, int>(suggestion, 1));
                    }
                }
            }

            final = final.OrderByDescending(x => x.Item2).ThenByDescending(x => x.Item1.Rating).ToList();

            return final.Select(x => x.Item1).Take(numSuggestions).ToList();
        }

        public void Save(string file)
        {
            int n = 1;
            _matrixFactorizationRecommender.Save(file + ".rm" + (n++));
            _randomRecommender.Save(file + ".rm" + (n++));
        }

        public void Load(string file)
        {
            int n = 1;
            _matrixFactorizationRecommender.Load(file + ".rm" + (n++));
            _randomRecommender.Load(file + ".rm" + (n++));
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
