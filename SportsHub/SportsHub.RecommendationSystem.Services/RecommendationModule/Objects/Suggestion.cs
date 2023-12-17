using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.RecommendationModule.Objects
{
    public class Suggestion
    {
        public string UserId { get; set; }

        public int TrainingProgramId { get; set; }

        public double Rating { get; set; }
        public Suggestion() { }

        public Suggestion(string userId, int trainingProgramId, double assurance)
        {
            UserId = userId;
            TrainingProgramId = trainingProgramId;
            Rating = assurance;
        }

        public override string ToString()
        {
            return $"For user {UserId} the suggestion is TrainingProgramId - {TrainingProgramId} with {Rating} possible rating.";
        }
    }
}
