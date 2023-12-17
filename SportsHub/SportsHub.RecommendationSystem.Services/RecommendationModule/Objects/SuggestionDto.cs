using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.Services.RecommendationModule.Objects
{
    public class SuggestionDto
    {
        public string UserId { get; set; }

        public int TrainingProgramId { get; set; }

        public int ScoreAssumption { get; set; }
    }
}
