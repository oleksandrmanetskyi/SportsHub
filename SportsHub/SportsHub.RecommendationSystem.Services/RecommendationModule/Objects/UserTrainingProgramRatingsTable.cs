using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.RecommendationModule.Objects
{
    public class UserTrainingProgramRatingsTable
    {
        public List<UserTrainingProgramRatings> Users { get; set; }

        public List<string> UserIndexToID { get; set; }

        public List<int> TrainingProgramIndexToID { get; set; }

        public UserTrainingProgramRatingsTable()
        {
            Users = new List<UserTrainingProgramRatings>();
            UserIndexToID = new List<string>();
            TrainingProgramIndexToID = new List<int>();
        }
    }
}
