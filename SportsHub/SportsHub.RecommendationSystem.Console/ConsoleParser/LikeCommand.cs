using CommandLine;
using SportsHub.RecommendationSystem.DatabaseSeeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.ConsoleParser
{
    [Verb("like", HelpText = "Likes the training program for specific user.")]
    public class LikeCommand : ICommand
    {
        [Option('u', "userId", Required = true, HelpText = "Set user id which will like a program.")]
        public string UserId { get; set; }

        [Option('p', "programId", Required = true, HelpText = "Set the training program id that will be liked by user.")]
        public int TrainingProgramId { get; set; }

        [Option('r', "rating", Required = true, HelpText = "Set the rating for training program.")]
        public int Rating { get; set; }

        [Option('l', "large", Required = false, HelpText = "Set the large dataset.")]
        public bool IsLarge { get; set; }

        public void Execute()
        {
            Console.WriteLine($"Executing like with {Rating} rating for program with id: {TrainingProgramId}, with {UserId} user id.");
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
                Console.WriteLine($"If you want to create him enter Y: ");
                var ch = Console.ReadKey().ToString();
                if (ch.ToLower() == "y")
                {
                    var newUser = new User
                    {
                        Age = 18,
                        Id = UserId,
                        Location = "Lviv",
                        Name = "Created from Test",
                        Sex = "Male",
                        SportKindId = 1,
                        SurName = "Test",
                        Weight = 60
                    };
                    database.Add(user);
                    database.SaveChanges();
                }
                else
                {
                    return;
                }
            }
            var program = database.TrainingPrograms.FirstOrDefault(x => x.Id == TrainingProgramId);
            if (program is null)
            {
                Console.WriteLine($"TrainingProgram with Id - {TrainingProgramId} doesn't exist.");
                return;
            }
            database.Ratings.Add(
                new DatabaseSeeding.Rating
                {
                    TrainingProgramId = TrainingProgramId,
                    UserId = UserId,
                    Score = Rating
                });
            database.SaveChanges();
        }
    }
}
