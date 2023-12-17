using CommandLine;
using SportsHub.RecommendationSystem.DatabaseSeeding;
using SportsHub.RecommendationSystem.RecommendationModule.Objects;
using SportsHub.RecommendationSystem.RecommendationModule.Recommenders;
using SportsHub.RecommendationSystem.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.ConsoleParser
{
    [Verb("test", HelpText = "Runs tests.")]
    public class TestCommand : ICommand
    {
        public void Execute()
        {
            var trainingDb = new SportsHubDbContext(@"Data Source=127.0.0.1;Initial Catalog=RecommendationDbTestTrain;User ID=sa;Password=TBD;Encrypt=false;TrustServerCertificate=true");
            var testingDb = new SportsHubDbContext(@"Data Source=127.0.0.1;Initial Catalog=RecommendationDbTestForTest;User ID=sa;Password=TBD;Encrypt=false;TrustServerCertificate=true");
            var mfr = new MatrixFactorizationRecommender(10);

            var data = new RatingsData();
            data.UserIds = trainingDb.Users.Select(x => x.Id).ToList();
            data.TrainingProgramIds = trainingDb.TrainingPrograms.Select(x => x.Id).ToList();
            data.Ratings = trainingDb.Ratings.Select(x => new Services.DTO.Rating
            {
                TrainingProgramId = x.TrainingProgramId,
                UserId = x.UserId,
                Score = x.Score
            }).ToList();

            var testingData = new RatingsData();
            data.UserIds = testingDb.Users.Select(x => x.Id).ToList();
            data.TrainingProgramIds = testingDb.TrainingPrograms.Select(x => x.Id).ToList();
            data.Ratings = testingDb.Ratings.Select(x => new Services.DTO.Rating
            {
                TrainingProgramId = x.TrainingProgramId,
                UserId = x.UserId,
                Score = x.Score
            }).ToList();

            string fileName = "TestLargeData5";
            var files = Directory.GetFiles("../../../", $"{fileName}.*");

            if (!(files.Length > 0))
            {
                mfr.Train(data);
                if (!(files.Length > 0))
                {
                    mfr.Save(Path.Combine("../../../", fileName));
                }
            }
            else
            {
                mfr.Load(Path.Combine("../../../", fileName));
            }

            ScoreResults scores3 = mfr.Score(testingData);
            TestResults results3 = mfr.Test(testingData, 30);


            using (StreamWriter w = new StreamWriter("../../../results5.csv"))
            {
                w.WriteLine("model,rmse,users,user-solved,precision,recall");
                w.WriteLine("SVD," + scores3.RootMeanSquareDifference + "," + results3.TotalUsers + "," + results3.UsersSolved + "," + results3.AveragePrecision + "," + results3.AverageRecall);
            }
            Console.WriteLine("model,rmse,users,user-solved,precision,recall");
            Console.WriteLine("SVD," + scores3.RootMeanSquareDifference + "," + results3.TotalUsers + "," + results3.UsersSolved + "," + results3.AveragePrecision + "," + results3.AverageRecall);
        }
    }
}
