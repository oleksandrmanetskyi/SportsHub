using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DatabaseSeeding
{
    public class TrainingProgramSportKind
    {
        [Column("SportKindId")]
        public int? SportKindId { get; set; }
        public SportKind SportKind { get; set; }

        [Column("TrainingProgramId")]
        public int? TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
    }
}
