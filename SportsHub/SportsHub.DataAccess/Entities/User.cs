using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SportsHub.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string ImagePath { get; set; }
        public string Location { get; set; }
        [Column("SportKindId")]
        public int? SportKindId { get; set; }
        //[ForeignKey("SportKindId")]
        public SportKind SportKind { get; set; }
        public int? TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public IEnumerable<Recommendation> Recommendations { get; set; }
    }
}
