using SportsHub.RecommendationSystem.Services;
using SportsHub.RecommendationSystem.RecommendationModule.Abstractions;
using SportsHub.RecommendationSystem.RecommendationModule.Mathematics;
using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using SportsHub.RecommendationSystem.RecommendationModule.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsHub.RecommendationSystem.Services.Database;
using SportsHub.RecommendationSystem.DTO;

namespace SportsHub.RecommendationSystem.RecommendationModule.Recommenders
{
    public class MatrixFactorizationRecommender : IRecommender
    {
        private UserTrainingProgramRatingsTable ratings;
        private Mathematics.SvdResult svd;
        //private IRater rater;

        private int numUsers;
        private int numTrainingPrograms;

        private int numFeatures;
        private int learningIterations;
        private DateTime StartedTime;
        private DateTime EndedTime;
        public MatrixFactorizationRecommender()
            : this(20)
        {
        }

        public MatrixFactorizationRecommender(int features)
        {
            numFeatures = features;
            learningIterations = 100;
        }

        public void Train(RatingsData db)
        {
            StartedTime = DateTime.UtcNow;
            UserBehaviorTransformer ubt = new UserBehaviorTransformer(db);
            ratings = ubt.GetUserTrainingProgramRatingsTable();

            SingularValueDecomposition factorizer = new SingularValueDecomposition(numFeatures, learningIterations);
            svd = factorizer.FactorizeMatrix(ratings);

            numUsers = ratings.UserIndexToID.Count;
            numTrainingPrograms = ratings.TrainingProgramIndexToID.Count;
            EndedTime = DateTime.UtcNow;
        }

        public double GetRating(string userId, int trainingProgramId)
        {
            int userIndex = ratings.UserIndexToID.IndexOf(userId);
            int trainingProgramIndex = ratings.TrainingProgramIndexToID.IndexOf(trainingProgramId);

            return GetRatingForIndex(userIndex, trainingProgramIndex);
        }

        private double GetRatingForIndex(int userIndex, int trainingProgramIndex)
        {
            return svd.AverageGlobalRating + svd.UserBiases[userIndex] + svd.TrainingProgramBiases[trainingProgramIndex] + Matrix.GetDotProduct(svd.UserFeatures[userIndex], svd.TrainingProgramFeatures[trainingProgramIndex]);
        }

        public List<Suggestion> GetSuggestions(string userId, int numSuggestions, int persentsOfSuggestions = 50, IEnumerable<Suggestion> excludeSuggestions = null)
        {
            numSuggestions = (numSuggestions * persentsOfSuggestions) / 100;
            int userIndex = ratings.UserIndexToID.IndexOf(userId);
            UserTrainingProgramRatings user = ratings.Users[userIndex];
            List<Suggestion> suggestions = new List<Suggestion>();

            for (int trainingProgramIndex = 0; trainingProgramIndex < ratings.TrainingProgramIndexToID.Count; trainingProgramIndex++)
            {
                // If the user in question hasn't rated the given trainingProgram yet
                if (user.TrainingProgramRatings[trainingProgramIndex] == 0)
                {
                    double rating = GetRatingForIndex(userIndex, trainingProgramIndex);

                    suggestions.Add(new Suggestion(userId, ratings.TrainingProgramIndexToID[trainingProgramIndex], rating));
                }
            }

            suggestions.Sort((c, n) => n.Rating.CompareTo(c.Rating));
            excludeSuggestions = suggestions.Take(numSuggestions).ToList();
            return suggestions.Take(numSuggestions).ToList();
        }

        public void Save(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Create))
            using (GZipStream zip = new GZipStream(fs, CompressionMode.Compress))
            using (StreamWriter w = new StreamWriter(zip))
            {
                w.WriteLine(numUsers);
                w.WriteLine(numTrainingPrograms);
                w.WriteLine(numFeatures);

                w.WriteLine(svd.AverageGlobalRating);

                for (int userIndex = 0; userIndex < numUsers; userIndex++)
                {
                    w.WriteLine(svd.UserBiases[userIndex]);
                }

                for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
                {
                    w.WriteLine(svd.TrainingProgramBiases[trainingProgramIndex]);
                }

                for (int userIndex = 0; userIndex < numUsers; userIndex++)
                {
                    for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                    {
                        w.WriteLine(svd.UserFeatures[userIndex][featureIndex]);
                    }
                }

                for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
                {
                    for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                    {
                        w.WriteLine(svd.TrainingProgramFeatures[trainingProgramIndex][featureIndex]);
                    }
                }

                foreach (UserTrainingProgramRatings t in ratings.Users)
                {
                    w.WriteLine(t.UserID);

                    foreach (double v in t.TrainingProgramRatings)
                    {
                        w.WriteLine(v);
                    }
                }

                foreach (string i in ratings.UserIndexToID)
                {
                    w.WriteLine(i);
                }

                foreach (int i in ratings.TrainingProgramIndexToID)
                {
                    w.WriteLine(i);
                }
            }
        }

        public void Load(string file)
        {
            ratings = new UserTrainingProgramRatingsTable();

            using (FileStream fs = new FileStream(file, FileMode.Open))
            using (GZipStream zip = new GZipStream(fs, CompressionMode.Decompress))
            using (StreamReader r = new StreamReader(zip))
            {
                numUsers = int.Parse(r.ReadLine());
                numTrainingPrograms = int.Parse(r.ReadLine());
                numFeatures = int.Parse(r.ReadLine());

                double averageGlobalRating = double.Parse(r.ReadLine());

                double[] userBiases = new double[numUsers];
                for (int userIndex = 0; userIndex < numUsers; userIndex++)
                {
                    userBiases[userIndex] = double.Parse(r.ReadLine());
                }

                double[] trainingProgramBiases = new double[numTrainingPrograms];
                for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
                {
                    trainingProgramBiases[trainingProgramIndex] = double.Parse(r.ReadLine());
                }

                double[][] userFeatures = new double[numUsers][];
                for (int userIndex = 0; userIndex < numUsers; userIndex++)
                {
                    userFeatures[userIndex] = new double[numFeatures];

                    for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                    {
                        userFeatures[userIndex][featureIndex] = double.Parse(r.ReadLine());
                    }
                }

                double[][] trainingProgramFeatures = new double[numTrainingPrograms][];
                for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
                {
                    trainingProgramFeatures[trainingProgramIndex] = new double[numFeatures];

                    for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                    {
                        trainingProgramFeatures[trainingProgramIndex][featureIndex] = double.Parse(r.ReadLine());
                    }
                }

                svd = new Mathematics.SvdResult(averageGlobalRating, userBiases, trainingProgramBiases, userFeatures, trainingProgramFeatures);

                for (int i = 0; i < numUsers; i++)
                {
                    string userId = r.ReadLine();
                    UserTrainingProgramRatings uat = new UserTrainingProgramRatings(userId, numTrainingPrograms);

                    for (int x = 0; x < numTrainingPrograms; x++)
                    {
                        uat.TrainingProgramRatings[x] = double.Parse(r.ReadLine());
                    }

                    ratings.Users.Add(uat);
                }

                for (int i = 0; i < numUsers; i++)
                {
                    ratings.UserIndexToID.Add(r.ReadLine());
                }

                for (int i = 0; i < numTrainingPrograms; i++)
                {
                    ratings.TrainingProgramIndexToID.Add(int.Parse(r.ReadLine()));
                }
            }
        }

        public void SaveToDatabase(RecommendationsDbContext database)
        {
            var svdId = Guid.NewGuid();
            var svdResult = new DTO.SvdResult()
            {
                Id = svdId,
                IsSuccessful = true,
                StartedOn = StartedTime,
                EndedOn = EndedTime
            };

            var userBiasesRes = new List<UserBiase>();
            var trainingProgramBiasesRes = new List<TrainingProgramBiase>();
            var userFeaturesRes = new List<UserFeature>();
            var trainingProgramFeaturesRes = new List<TrainingProgramFeature>();

            svdResult.AverageGlobalRating = (float)svd.AverageGlobalRating;

            for (int userIndex = 0; userIndex < numUsers; userIndex++)
            {
                userBiasesRes.Add(new UserBiase
                {
                    Biase = (float)svd.UserBiases[userIndex],
                    SvdResultId = svdId,
                });
            }

            for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
            {
                trainingProgramBiasesRes.Add(new TrainingProgramBiase
                {
                    Biase = (float)svd.TrainingProgramBiases[trainingProgramIndex],
                    SvdResultId = svdId
                });
            }

            for (int userIndex = 0; userIndex < numUsers; userIndex++)
            {
                for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                {
                    userFeaturesRes.Add(new UserFeature
                    {
                        SvdResultId = svdId,
                        FeatureIndex = featureIndex,
                        UserIndex = userIndex,
                        Value = (float)svd.UserFeatures[userIndex][featureIndex]
                    });
                }
            }

            for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
            {
                for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                {
                    trainingProgramFeaturesRes.Add(new TrainingProgramFeature
                    {
                        FeatureIndex = featureIndex,
                        TrainingProgramIndex = trainingProgramIndex,
                        SvdResultId = svdId,
                        Value = (float)svd.TrainingProgramFeatures[trainingProgramIndex][featureIndex]
                    });
                }
            }
            var trainedRatings = database.Ratings.Where(x => ratings.TrainingProgramIndexToID.Contains(x.TrainingProgramId) && ratings.UserIndexToID.Contains(x.UserId)).ToList();
            for (int i = 0; i < trainedRatings.Count; i++)
            {
                trainedRatings[i].IsTrained = true;
                database.Ratings.Update(trainedRatings[i]);
            }
            database.SvdResults.Add(svdResult);
            database.UserBiases.AddRange(userBiasesRes);
            database.TrainingProgramBiases.AddRange(trainingProgramBiasesRes);
            database.UserFeatures.AddRange(userFeaturesRes);
            database.TrainingProgramFeatures.AddRange(trainingProgramFeaturesRes);
            database.SaveChanges();
        }

        public void LoadFromDatabase(RecommendationsDbContext database, RatingsData db)
        {
            //UserBehaviorTransformer ubt = new UserBehaviorTransformer(db);
            //ratings = ubt.GetUserTrainingProgramRatingsTable();
            //var svdRes = database.SvdResults.OrderByDescending(x => x.EndedOn).First();

            //var userBiasesRes = database.UserBiases.Where(x => x.SvdResultId == svdRes.Id).OrderBy(x => x.Id).ToList();
            //var trainingProgramBiasesRes = database.TrainingProgramBiases.Where(x => x.SvdResultId == svdRes.Id).OrderBy(x => x.Id).ToList();
            //var userFeaturesRes = database.UserFeatures.Where(x => x.SvdResultId == svdRes.Id).OrderBy(x => x.Id).ToList();
            //var trainingProgramFeaturesRes = database.TrainingProgramFeatures.Where(x => x.SvdResultId == svdRes.Id).OrderBy(x => x.Id).ToList();

            //svd = new Mathematics.SvdResult(svdRes.AverageGlobalRating)

            var svdRes = database.SvdResults.OrderByDescending(x => x.EndedOn).First();
            var userBiasesRes = database.UserBiases.Where(x => x.SvdResultId == svdRes.Id).OrderBy(x => x.Id).ToList();
            var trainingProgramBiasesRes = database.TrainingProgramBiases.Where(x => x.SvdResultId == svdRes.Id).OrderBy(x => x.Id).ToList();
            var userFeaturesRes = database.UserFeatures.Where(x => x.SvdResultId == svdRes.Id).OrderBy(x => x.Id).ToList();
            var trainingProgramFeaturesRes = database.TrainingProgramFeatures.Where(x => x.SvdResultId == svdRes.Id).OrderBy(x => x.Id).ToList();

            ratings = new UserTrainingProgramRatingsTable();

            numUsers = userBiasesRes.Count();
            numTrainingPrograms = trainingProgramBiasesRes.Count();
            numFeatures = userFeaturesRes.Count() / numUsers;

            double averageGlobalRating = svdRes.AverageGlobalRating;

            double[] userBiases = new double[numUsers];
            for (int userIndex = 0; userIndex < numUsers; userIndex++)
            {
                userBiases[userIndex] = userBiasesRes[userIndex].Biase;
            }

            double[] trainingProgramBiases = new double[numTrainingPrograms];
            for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
            {
                trainingProgramBiases[trainingProgramIndex] = trainingProgramBiasesRes[trainingProgramIndex].Biase;
            }

            double[][] userFeatures = new double[numUsers][];
            for (int userIndex = 0; userIndex < numUsers; userIndex++)
            {
                userFeatures[userIndex] = new double[numFeatures];

                for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                {
                    userFeatures[userIndex][featureIndex] = userFeaturesRes.First(x => x.UserIndex == userIndex && x.FeatureIndex == featureIndex).Value;
                }
            }

            double[][] trainingProgramFeatures = new double[numTrainingPrograms][];
            for (int trainingProgramIndex = 0; trainingProgramIndex < numTrainingPrograms; trainingProgramIndex++)
            {
                trainingProgramFeatures[trainingProgramIndex] = new double[numFeatures];

                for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
                {
                    trainingProgramFeatures[trainingProgramIndex][featureIndex] = trainingProgramFeaturesRes.First(x => x.TrainingProgramIndex == trainingProgramIndex && x.FeatureIndex == featureIndex).Value;
                }
            }

            svd = new Mathematics.SvdResult(averageGlobalRating, userBiases, trainingProgramBiases, userFeatures, trainingProgramFeatures);

            //for (int i = 0; i < numUsers; i++)
            //{
            //    string userId = r.ReadLine();
            //    UserTrainingProgramRatings uat = new UserTrainingProgramRatings(userId, numTrainingPrograms);

            //    for (int x = 0; x < numTrainingPrograms; x++)
            //    {
            //        uat.TrainingProgramRatings[x] = double.Parse(r.ReadLine());
            //    }

            //    ratings.Users.Add(uat);
            //}

            //for (int i = 0; i < numUsers; i++)
            //{
            //    ratings.UserIndexToID.Add(r.ReadLine());
            //}

            //for (int i = 0; i < numTrainingPrograms; i++)
            //{
            //    ratings.TrainingProgramIndexToID.Add(int.Parse(r.ReadLine()));
            //}
            db.Ratings = db.Ratings.Where(x => x.CreatedOn <= svdRes.StartedOn).ToList(); // потенційно тут бага
            //db.TrainingProgramIds = db.TrainingProgramIds.Where
            UserBehaviorTransformer ubt = new UserBehaviorTransformer(db);
            ratings = ubt.GetUserTrainingProgramRatingsTable();
        }
    }
}
