using System;

namespace SportsHub.RecommendationSystem.Services.DTO
{
    public class Rating
    {
        public string UserId { get; set; }
        public int TrainingProgramId { get; set; }
        public int Score { get; set; }
        public bool IsTrained { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
