using SportsHub.RecommendationSystem.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.Services
{
    public class RatingsData
    {
        public List<string> UserIds { get; set; }
        public List<int> TrainingProgramIds { get; set; }
        public List<RecommendationSystem.Services.DTO.Rating> Ratings { get; set; }
    }
}
