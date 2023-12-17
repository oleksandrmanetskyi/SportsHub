using CommandLine;
using Microsoft.EntityFrameworkCore;
using SportsHub.RecommendationSystem.DatabaseSeeding;
using SportsHub.RecommendationSystem.RecommendationModule.Recommenders;
using SportsHub.RecommendationSystem.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem
{
    [Verb("recommend", HelpText = "Gives the recommendation for your user.")]
    public class RecommendCommand : ICommand
    {
        [Option('u', "userId", Required = true, HelpText = "Set user which will receive a recommendation.")]
        public string UserId { get; set; }

        [Option('s', "suggestions", Required = true, HelpText = "Set count of suggestions for recommendation.")]
        public int Suggestions { get; set; }

        [Option('l', "large", Required = false, HelpText = "Set the large dataset.")]
        public bool IsLarge { get; set; } = false;

        public void Execute()
        {
            //Console.WriteLine($"Executing reccomendation for user with id: {UserId}, with {Suggestions} suggestions.");
            SportsHubDbContext database = null;
            if (IsLarge)
            {
                database = new SportsHubDbContext(@"Data Source=127.0.0.1;Initial Catalog=RecommendationDb;User ID=sa;Password=I8well4sure;Encrypt=false;TrustServerCertificate=true");
            }
            else
            {
                database = new SportsHubDbContext(@"Data Source=127.0.0.1;Initial Catalog=RecommendationDbMedium;User ID=sa;Password=I8well4sure;Encrypt=false;TrustServerCertificate=true");

            }

            var user = database.Users.FirstOrDefault(x => x.Id == UserId);
            if (user is null)
            {
                Console.WriteLine($"User with Id - {UserId} doesn't exist.");
                return;
            }


            Console.WriteLine($"Previous user {UserId} ratings are:");
            var ratings = database.Ratings.Where(u => u.UserId == UserId).Include(x => x.TrainingProgram.TrainingProgramSportKinds);
            var sportKinds = database.SportKinds.ToList();
            foreach (var item in ratings)
            {
                Console.WriteLine($"Training program - {item.TrainingProgramId} - {item.TrainingProgram.Name} - sport kinds - {string.Join(",", sportKinds.Where(z => item.TrainingProgram.TrainingProgramSportKinds.Select(x => x.SportKindId).Contains(z.Id)).Select(y => y.Name))}");
            }

            var data = new RatingsData();
            data.UserIds = database.Users.Select(x => x.Id).ToList();
            data.TrainingProgramIds = database.TrainingPrograms.Select(x => x.Id).ToList();
            data.Ratings = database.Ratings.Select(x => new Services.DTO.Rating
            {
                TrainingProgramId = x.TrainingProgramId,
                UserId = x.UserId,
                Score = x.Score
            }).ToList();
            var randomRecommender = new RandomRecommender(data);
            var matrixRecommender = new MatrixFactorizationRecommender();
            
            var hybridRecommender = new HybridRecommender(matrixRecommender, randomRecommender);

            string fileName = "LargeTrain";
            var files = Directory.GetFiles("../../../", $"{fileName}.*");

            if (!IsLarge || ! (files.Length > 0))
            {
                hybridRecommender.Train(data);
                if (!(files.Length > 0))
                {
                    hybridRecommender.Save(Path.Combine("../../../", fileName));
                }
            }
            else
            {
                hybridRecommender.Load(Path.Combine("../../../", fileName));
            }
            var suggestions = hybridRecommender.GetSuggestions(UserId, Suggestions);

            var programs = database.TrainingPrograms.Where(x => suggestions.Select(y => y.TrainingProgramId).Contains(x.Id)).Include(g => g.TrainingProgramSportKinds).ToList();
            Console.WriteLine("Recommendations: ");
            foreach (var suggestion in suggestions)
            {
                Console.WriteLine($"{suggestion.ToString()} - Sport kinds - {string.Join(",", sportKinds.Where(z => programs.First(o => o.Id ==suggestion.TrainingProgramId).TrainingProgramSportKinds.Select(x => x.SportKindId).Contains(z.Id)).Select(y => y.Name))}");
            }
        }
    }
}
