using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.RecommendationModule.Objects
{
    public class UserTrainingProgramRatings
    {
        public string UserID { get; set; }

        public double[] TrainingProgramRatings { get; set; }

        public double Score { get; set; }

        public UserTrainingProgramRatings(string userId, int trainingProgramCount)
        {
            UserID = userId;
            TrainingProgramRatings = new double[trainingProgramCount];
        }

        public void AppendRatings(double[] ratings)
        {
            List<double> allRatings = new List<double>();

            allRatings.AddRange(TrainingProgramRatings);
            allRatings.AddRange(ratings);

            TrainingProgramRatings = allRatings.ToArray();
        }
    }
}
