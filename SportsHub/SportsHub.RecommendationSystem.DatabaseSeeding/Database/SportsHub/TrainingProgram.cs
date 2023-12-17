using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DatabaseSeeding
{
    public class TrainingProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //[Column("SportKindId")]
        //public int? SportKindId { get; set; }
        ////[ForeignKey("SportKindId")]
        //public SportKind SportKind { get; set; }
        public IEnumerable<User> Users { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<TrainingProgramSportKind> TrainingProgramSportKinds { get; set; }

    }
}
