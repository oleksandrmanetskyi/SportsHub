using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DatabaseSeeding
{
    public class SportKind
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
        public ICollection<TrainingProgramSportKind> TrainingProgramSportKinds { get; set; }
    }
}
