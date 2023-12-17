using SportsHub.RecommendationSystem.Services.DTO;
using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsHub.RecommendationSystem.Services;

namespace SportsHub.RecommendationSystem.RecommendationModule.Parsers
{
    public class UserBehaviorTransformer
    {
        private RatingsData _data;

        public UserBehaviorTransformer(RatingsData data)
        {
            _data = data;
        }

        /// <summary>
        /// Get a list of all users and their ratings on every trainingProgram
        /// </summary>
        public UserTrainingProgramRatingsTable GetUserTrainingProgramRatingsTable()
        {
            UserTrainingProgramRatingsTable table = new UserTrainingProgramRatingsTable();

            table.UserIndexToID = _data.UserIds;
            table.TrainingProgramIndexToID = _data.TrainingProgramIds;


            foreach (string id in table.UserIndexToID)
            {
                table.Users.Add(new UserTrainingProgramRatings(id, table.TrainingProgramIndexToID.Count));
            }

            //var userArticleRatingGroup = db.Ratings
            //    .GroupBy(x => new { x.UserId, x.TrainingProgramId })
            //    .Select(g => new { g.Key.UserId, g.Key.TrainingProgramId, Rating = rater.GetRating(g.ToList()) })
            //    .ToList();

            foreach (var rating in _data.Ratings)
            {
                int userIndex = table.UserIndexToID.IndexOf(rating.UserId);
                int programIndex = table.TrainingProgramIndexToID.IndexOf(rating.TrainingProgramId);

                table.Users[userIndex].TrainingProgramRatings[programIndex] = rating.Score;
            }

            return table;
        }
    }
}
