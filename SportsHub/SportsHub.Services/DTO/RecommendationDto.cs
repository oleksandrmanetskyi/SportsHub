using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsHub.Services.DTO
{
    public class RecommendationDto
    {
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [JsonProperty("TrainingProgramId")]
        public int TrainingProgramId { get; set; }
        [JsonProperty("Rating")]
        public float ScoreAssumption { get; set; }
    }
}
