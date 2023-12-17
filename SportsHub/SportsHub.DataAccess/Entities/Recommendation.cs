using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsHub.DataAccess.Entities
{
    public class Recommendation
    {
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        [Required]
        public float ScoreAssumption { get; set; }
        public bool IsActive { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; }
    }
}
