using Microsoft.EntityFrameworkCore;
using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using SportsHub.RecommendationSystem.RecommendationModule.Recommenders;
using SportsHub.RecommendationSystem.Services.Database;
using SportsHub.RecommendationSystem.Services.RecommendationModule.Objects;

namespace SportsHub.RecommendationSystem.Services
{
    public class RecommendationsService : IRecommendationsService
    {
        private RecommendationsDbContext _database;
        private ISportsHubRepository _sportsHubRepository;

        public RecommendationsService(RecommendationsDbContext database, ISportsHubRepository sportsHubRepository)
        {
            _database = database;
            _sportsHubRepository = sportsHubRepository;
        }

        public async Task<int?> GetRatingAsync(string userId, int trainingProgramId)
        {
            return (await _database.Ratings.FirstOrDefaultAsync(x => x.UserId == userId && x.TrainingProgramId == trainingProgramId))?.Score;
        }

        public List<SuggestionDto> GetRecommendations(string userId, int suggestionsCount)
        {
            var data = new RatingsData();//тут напряму колати спортс хаб бд
            data.UserIds = _sportsHubRepository.GetAllUserIds();
            data.TrainingProgramIds = _sportsHubRepository.GetAllTrainingProgramIds();
            data.Ratings = _database.Ratings.ToList();

            if (data.UserIds.Count == 0 || !data.UserIds.Contains(userId) || data.TrainingProgramIds.Count == 0 || data.Ratings.Count == 0)
            {
                return null;
            }

            var randomRecommender = new RandomRecommender(data);
            var matrixRecommender = new MatrixFactorizationRecommender();
            if (data.Ratings.Count == 0 || data.Ratings.Where(x => x.UserId == userId).Count() == 0)//якшо нема рейтингів вопше або якшо цей юзер нічо не лайкнув ще
            {
                var excluded = _sportsHubRepository.GetAllTrainingProgramsFromRecommendations().Where(x => x.UserId == userId).Select(x => new Suggestion
                {
                    TrainingProgramId = x.TrainingProgramId,
                    UserId = x.UserId,
                    Rating = x.ScoreAssumption
                });
                return randomRecommender.GetSuggestions(userId, suggestionsCount, 100, excluded).Select(x => new SuggestionDto
                {
                    ScoreAssumption = (int)x.Rating - 10,
                    TrainingProgramId = x.TrainingProgramId,
                    UserId = x.UserId
                }).ToList();
            }
            data.TrainingProgramIds = data.Ratings.Select(x => x.TrainingProgramId).Distinct().ToList();
            matrixRecommender.LoadFromDatabase(_database, data);

            var hybridRecommender = new HybridRecommender(matrixRecommender, randomRecommender);

            var excludeSuggestions = _sportsHubRepository.GetAllTrainingProgramsFromRecommendations().Where(x => x.UserId == userId).Select(x => new Suggestion
            {
                TrainingProgramId = x.TrainingProgramId,
                UserId = x.UserId,
                Rating = x.ScoreAssumption
            });

            var res = hybridRecommender.GetSuggestions(userId, suggestionsCount, excludeSuggestions: excludeSuggestions);
            return res.Select(x => new SuggestionDto
            {
                ScoreAssumption = (int)x.Rating,
                TrainingProgramId = x.TrainingProgramId,
                UserId = x.UserId
            }).ToList();
        }

        public bool HasNewRecords()
        {
            return _database.Ratings.Where(x => x.IsTrained == false).Any();
        }

        public async Task LikeAsync(string userId, int trainingProgramId, int rating)
        {
            var userIds = _sportsHubRepository.GetAllUserIds();
            var trainingProgramIds = _sportsHubRepository.GetAllTrainingProgramIds();
            if (!userIds.Contains(userId))
            {
                throw new Exception($"User with id=[{userId}] is not found.");
            }
            if (!trainingProgramIds.Contains(trainingProgramId))
            {
                throw new Exception($"Training program with id=[{trainingProgramId}] is not found.");
            }
            var existingRating = _database.Ratings.FirstOrDefault(x => x.UserId == userId && x.TrainingProgramId == trainingProgramId);
            if (existingRating != null)
            {
                existingRating.Score = rating;
                existingRating.IsTrained = false;
                _database.Ratings.Update(existingRating);
                await _database.SaveChangesAsync();
                return;
            }
            await _database.Ratings.AddAsync(new DTO.Rating
            {
                UserId = userId,
                TrainingProgramId = trainingProgramId,
                Score = rating,
                IsTrained = false,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            });
            await _database.SaveChangesAsync();
        }

        public void PerformNewRecommendations()
        {
            var data = new RatingsData();//тут напряму колати спортс хаб бд
            data.UserIds = _sportsHubRepository.GetAllUserIds();
            data.TrainingProgramIds = _sportsHubRepository.GetAllTrainingProgramIds();
            data.Ratings = _database.Ratings.ToList();

            var matrixRecommender = new MatrixFactorizationRecommender();

            matrixRecommender.Train(data);

            matrixRecommender.SaveToDatabase(_database);
        }
    }
}