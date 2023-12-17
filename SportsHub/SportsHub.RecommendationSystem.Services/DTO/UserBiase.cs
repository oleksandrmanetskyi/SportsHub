using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DTO
{
    public class UserBiase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Biase { get; set; }
        public Guid SvdResultId { get; set; }
        public SvdResult SvdResult { get; set; }
    }
}
