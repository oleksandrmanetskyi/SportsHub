using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.RecommendationModule.Mathematics
{
    class SingularValueDecomposition
    {
        private double averageGlobalRating;

        private int learningIterations;
        private double learningRate;
        private double learningDescent = 0.99;
        private double regularizationTerm = 0.02;

        private int numUsers;
        private int numTrainingPrograms;
        private int numFeatures;

        private double[] userBiases;
        private double[] trainingProgramBiases;
        private double[][] userFeatures;
        private double[][] trainingProgramFeatures;

        public SingularValueDecomposition()
            : this(20, 100)
        {
        }

        public SingularValueDecomposition(int features, int iterations)
            : this(features, iterations, 0.005)
        {
        }

        public SingularValueDecomposition(int features, int iterations, double learningSpeed)
        {
            numFeatures = features;
            learningIterations = iterations;
            learningRate = learningSpeed;
        }

        private void Initialize(UserTrainingProgramRatingsTable ratings)
        {
            numUsers = ratings.Users.Count;
            numTrainingPrograms = ratings.Users[0].TrainingProgramRatings.Length;

            Random rand = new Random();

            userFeatures = new double[numUsers][];
            for (int userIndex = 0; userIndex < numUsers; userIndex++)
            {
                userFeatures[userIndex] = new double[numFeatures];

                for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                {
                    userFeatures[userIndex][featureIndex] = rand.NextDouble();
                }
            }

            trainingProgramFeatures = new double[numTrainingPrograms][];
            for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
            {
                trainingProgramFeatures[trainingProgramIndex] = new double[numFeatures];

                for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                {
                    trainingProgramFeatures[trainingProgramIndex][featureIndex] = rand.NextDouble();
                }
            }

            userBiases = new double[numUsers];
            trainingProgramBiases = new double[numTrainingPrograms];
        }

        public SvdResult FactorizeMatrix(UserTrainingProgramRatingsTable ratings)
        {
            Initialize(ratings);

            double squaredError;
            int count;
            List<double> rmseAll = new List<double>();

            averageGlobalRating = GetAverageRating(ratings);

            for (int i = 0; i < learningIterations; i++)
            {
                squaredError = 0.0;
                count = 0;

                for (int userIndex = 0; userIndex < numUsers; userIndex++)
                {
                    for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
                    {
                        if (ratings.Users[userIndex].TrainingProgramRatings[trainingProgramIndex] != 0)
                        {
                            double predictedRating = averageGlobalRating + userBiases[userIndex] + trainingProgramBiases[trainingProgramIndex] + Matrix.GetDotProduct(userFeatures[userIndex], trainingProgramFeatures[trainingProgramIndex]);

                            double error = ratings.Users[userIndex].TrainingProgramRatings[trainingProgramIndex] - predictedRating;

                            if (double.IsNaN(predictedRating))
                            {
                                throw new Exception("Encountered a non-number while factorizing a matrix! Try decreasing the learning rate.");
                            }

                            squaredError += Math.Pow(error, 2);
                            count++;

                            averageGlobalRating += learningRate * (error - regularizationTerm * averageGlobalRating);
                            userBiases[userIndex] += learningRate * (error - regularizationTerm * userBiases[userIndex]);
                            trainingProgramBiases[trainingProgramIndex] += learningRate * (error - regularizationTerm * trainingProgramBiases[trainingProgramIndex]);

                            for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                            {
                                userFeatures[userIndex][featureIndex] += learningRate * (error * trainingProgramFeatures[trainingProgramIndex][featureIndex] - regularizationTerm * userFeatures[userIndex][featureIndex]);
                                trainingProgramFeatures[trainingProgramIndex][featureIndex] += learningRate * (error * userFeatures[userIndex][featureIndex] - regularizationTerm * trainingProgramFeatures[trainingProgramIndex][featureIndex]);
                            }
                        }
                    }
                }

                squaredError = Math.Sqrt(squaredError / count);
                rmseAll.Add(squaredError);

                learningRate *= learningDescent;
            }

            //using (StreamWriter w = new StreamWriter("rmse.csv"))
            //{
            //    w.WriteLine("epoc,rmse");
            //    for (int i = 0; i < rmseAll.Count; i++)
            //    {
            //        w.WriteLine((i + 1) + "," + rmseAll[i]);
            //    }
            //}

            return new SvdResult(averageGlobalRating, userBiases, trainingProgramBiases, userFeatures, trainingProgramFeatures);
        }

        /// <summary>
        /// Get the average rating of non-zero values across the entire user-trainingProgram matrix
        /// </summary>
        private double GetAverageRating(UserTrainingProgramRatingsTable ratings)
        {
            double sum = 0.0;
            int count = 0;

            for (int userIndex = 0; userIndex < numUsers; userIndex++)
            {
                for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
                {
                    // If the given user rated the given item, add it to our average
                    if (ratings.Users[userIndex].TrainingProgramRatings[trainingProgramIndex] != 0)
                    {
                        sum += ratings.Users[userIndex].TrainingProgramRatings[trainingProgramIndex];
                        count++;
                    }
                }
            }

            return sum / count;
        }
    }
}
