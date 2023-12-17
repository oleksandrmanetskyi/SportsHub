using SportsHub.RecommendationSystem.Services;
using SportsHub.RecommendationSystem.RecommendationModule.Abstractions;
using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using SportsHub.RecommendationSystem.RecommendationModule.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.RecommendationModule.Recommenders
{
    public static class ClassifierExtensions
    {
        public static TestResults Test(this IRecommender classifier, RatingsData db, int numSuggestions)
        {
            // We're only using the ratings to check for existence of a rating, so we can use a simple rater for everything
            //SimpleRater rater = new SimpleRater();
            UserBehaviorTransformer ubt = new UserBehaviorTransformer(db);
            UserTrainingProgramRatingsTable ratings = ubt.GetUserTrainingProgramRatingsTable();

            int correctUsers = 0;
            double averagePrecision = 0.0;
            double averageRecall = 0.0;

            // Get a list of users in this database who interacted with an TrainingProgram for the first time
            List<string> distinctUsers = db.Ratings.Select(x => x.UserId).Distinct().ToList();

            var distinctUserTrainingPrograms = db.Ratings.GroupBy(x => new { x.UserId, x.TrainingProgramId });

            // Now get suggestions for each of these users
            foreach (string user in distinctUsers)
            {
                List<Suggestion> suggestions = classifier.GetSuggestions(user, numSuggestions);
                bool foundOne = false;
                int userIndex = ratings.UserIndexToID.IndexOf(user);

                int userCorrectTrainingPrograms = 0;
                int userTotalTrainingPrograms = distinctUserTrainingPrograms.Count(x => x.Key.UserId == user);

                foreach (Suggestion s in suggestions)
                {
                    int TrainingProgramIndex = ratings.TrainingProgramIndexToID.IndexOf(s.TrainingProgramId);

                    // If one of the top N suggestions is what the user ended up reading, then we're golden
                    if (ratings.Users[userIndex].TrainingProgramRatings[TrainingProgramIndex] != 0)
                    {
                        userCorrectTrainingPrograms++;

                        if (!foundOne)
                        {
                            correctUsers++;
                            foundOne = true;
                        }
                    }
                }

                averagePrecision += (double)userCorrectTrainingPrograms / numSuggestions;
                averageRecall += (double)userCorrectTrainingPrograms / userTotalTrainingPrograms;
            }

            averagePrecision /= distinctUsers.Count;
            averageRecall /= distinctUsers.Count;

            return new TestResults(distinctUsers.Count, correctUsers, averageRecall, averagePrecision);
        }

        public static ScoreResults Score(this IRecommender classifier, RatingsData db)
        {
            UserBehaviorTransformer ubt = new UserBehaviorTransformer(db);
            UserTrainingProgramRatingsTable actualRatings = ubt.GetUserTrainingProgramRatingsTable();

            var distinctUserTrainingProgramPairs = db.Ratings.ToList().GroupBy(x => new { x.UserId, x.TrainingProgramId }).ToList();

            double score = 0.0;
            int count = 0;

            foreach (var userTrainingProgram in distinctUserTrainingProgramPairs)
            {
                int userIndex = actualRatings.UserIndexToID.IndexOf(userTrainingProgram.Key.UserId);
                int TrainingProgramIndex = actualRatings.TrainingProgramIndexToID.IndexOf(userTrainingProgram.Key.TrainingProgramId);

                double actualRating = actualRatings.Users[userIndex].TrainingProgramRatings[TrainingProgramIndex];

                if (actualRating != 0)
                {
                    double predictedRating = classifier.GetRating(userTrainingProgram.Key.UserId, userTrainingProgram.Key.TrainingProgramId);

                    score += Math.Pow(predictedRating - actualRating, 2);
                    count++;
                }
            }

            if (count > 0)
            {
                score = Math.Sqrt(score / count);
            }

            return new ScoreResults(score);
        }
    }
}
