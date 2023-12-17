using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DTO
{
    public class UserFeature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserIndex { get; set; }
        public int FeatureIndex { get; set; }
        public float Value { get; set; }
        public Guid SvdResultId { get; set; }
        public SvdResult SvdResult { get; set; }
    }
}
