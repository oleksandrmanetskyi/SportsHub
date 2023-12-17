using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DatabaseSeeding
{
    public class Rating
    {
        [Column("UserId")]
        public string UserId { get; set; }
        public User user { get; set; }

        [Column("TrainingProgramId")]
        public int TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }

        public int Score { get; set; }
    }
}
