using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.RecommendationModule.Mathematics
{
    class SvdResult
    {
        public double AverageGlobalRating { get; private set; }
        public double[] UserBiases { get; private set; }
        public double[] TrainingProgramBiases { get; private set; }
        public double[][] UserFeatures { get; private set; }
        public double[][] TrainingProgramFeatures { get; private set; }

        public SvdResult(double averageGlobalRating, double[] userBiases, double[] trainingProgramBiases, double[][] userFeatures, double[][] trainingProgramFeatures)
        {
            AverageGlobalRating = averageGlobalRating;
            UserBiases = userBiases;
            TrainingProgramBiases = trainingProgramBiases;
            UserFeatures = userFeatures;
            TrainingProgramFeatures = trainingProgramFeatures;
        }
    }
}
