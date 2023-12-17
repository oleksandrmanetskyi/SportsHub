using System;
using System.Collections.Generic;
using System.Text;

namespace SportsHub.Services.DTO
{
    public class RecommendationSystemAPI
    {
        public string Host { get; set; }
        public string GetEndpoint { get; set; }
        public string LikeEndpoint { get; set; }
        public string GetRatingEndpoint { get; set; }
    }
}
