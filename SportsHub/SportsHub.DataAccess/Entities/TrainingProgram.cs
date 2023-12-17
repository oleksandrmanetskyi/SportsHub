using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsHub.DataAccess.Entities
{
    public class TrainingProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int? NutritionId { get; set; }
        public Nutrition Nutrition { get; set; }
        [Column("SportKindId")]
        public int? SportKindId { get; set; }
        //[ForeignKey("SportKindId")]
        public SportKind SportKind { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Recommendation> Recommendations { get; set; }
    }
}
