using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DatabaseSeeding
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string Sex { get; set; }

        [Column("SportKindId")]
        public int? SportKindId { get; set; }
        public SportKind SportKind { get; set; }

        public int? TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }

        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
