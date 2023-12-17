using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DTO
{
    public class SvdResult
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsSuccessful { get; set; }
        public float AverageGlobalRating { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime EndedOn { get; set; }
    }
}
