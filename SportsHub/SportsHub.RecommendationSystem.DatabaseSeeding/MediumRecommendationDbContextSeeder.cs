using SportsHub.RecommendationSystem.DatabaseSeeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportsHub.RecommendationSystem.DatabaseSeeding
{
    public class MediumRecommendationDbContextSeeder : IDbSeeder
    {
        private readonly SportsHubDbContext _dbContext;

        public MediumRecommendationDbContextSeeder(SportsHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            _dbContext.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[SportKinds] ON");

            // Create SportKinds
            var kindNames = new string[]
                {
                    "Football", "Workout", "Running", "Boxing", "Basketball", "Archery","Athletics","Billiards","Bobsleigh","Bowling", "Canoe", "Chess","Climbing", "Cycling",
                    "Dancing", "Darts", "Discus", "Fencing", "Figure", "Skating", "Fishing","Gliding", "Gymnastics", "Hammer Throwing",
                    "Hang-gliding", "Diving", "High jump", "Hiking", "Horse", "Racing", "Hunting", "Ice Hockey", "Pentathlon", "Motorsports",
                    "Mountaineering", "Paintball", "Parachuting", "Race", "Gymnastics", "Riding", "Skipping", "Rowing", "Sailing",
                    "Shooting", "Shot put", "Skateboarding", "Skating", "Jumping", "Skiing", "Snowboarding", "Surfing", "Swimming",
                    "Tennis", "Walking", "Water Polo", "Waterski", "Lifting", "Windsurfing", "Wrestling"
                }
            ;
            var sportKinds = kindNames.Distinct().Select((x, y) => new SportKind() { Id = y + 1, Name = x }).ToList();
            _dbContext.SportKinds.AddRange(sportKinds);

            _dbContext.SaveChanges();
            _dbContext.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[SportKinds] OFF");

            _dbContext.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[TrainingPrograms] ON");

            // Create TrainingPrograms
            var trainingPrograms = new List<TrainingProgram>
            {
                new TrainingProgram { Id = 1, Name = "Beginner Football Training", Description = "Introduction to football for beginners"},
                new TrainingProgram { Id = 2, Name = "Advanced Basketball Training", Description = "Intensive basketball training for advanced players"},
                new TrainingProgram { Id = 3, Name = "Tennis Basics", Description = "Learn the basics of tennis"},
                new TrainingProgram { Id = 4, Name = "Swimming Basics", Description = "Learn the basics of swimming"},
                new TrainingProgram { Id = 5, Name = "Running for Beginners", Description = "Get started with running"},
                new TrainingProgram { Id = 6, Name = "Medium Football Training", Description = "Intensive football training" },
                new TrainingProgram { Id = 7, Name = "Advanced Football Training", Description = "Intensive football training" },
                new TrainingProgram { Id = 8, Name = "Shevchenko's Football Training", Description = "Intensive football training"}
            };
            _dbContext.TrainingPrograms.AddRange(trainingPrograms);

            var trainingProgramSportKindsRelation = new List<TrainingProgramSportKind>
            {
                new TrainingProgramSportKind
                {
                    SportKindId = sportKinds.First(x => x.Name == "Football").Id,
                    TrainingProgramId = 1
                },
                new TrainingProgramSportKind
                {
                    SportKindId = sportKinds.First(x => x.Name == "Basketball").Id,
                    TrainingProgramId = 2
                },
                new TrainingProgramSportKind
                {
                    SportKindId = sportKinds.First(x => x.Name == "Tennis").Id,
                    TrainingProgramId = 3
                },
                new TrainingProgramSportKind
                {
                    SportKindId = sportKinds.First(x => x.Name == "Swimming").Id,
                    TrainingProgramId = 4
                },
                new TrainingProgramSportKind
                {
                    SportKindId = sportKinds.First(x => x.Name == "Running").Id,
                    TrainingProgramId = 5
                },
                new TrainingProgramSportKind
                {
                    SportKindId = sportKinds.First(x => x.Name == "Football").Id,
                    TrainingProgramId = 6
                },
                new TrainingProgramSportKind
                {
                    SportKindId = sportKinds.First(x => x.Name == "Football").Id,
                    TrainingProgramId = 7
                },
                new TrainingProgramSportKind
                {
                    SportKindId = sportKinds.First(x => x.Name == "Football").Id,
                    TrainingProgramId = 8
                }
            };

            _dbContext.TrainingProgramSportKinds.AddRange(trainingProgramSportKindsRelation);
            _dbContext.SaveChanges();

            _dbContext.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[TrainingPrograms] OFF");

            _dbContext.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Users] ON");

            // Create Users
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid().ToString(), Name = "John", SurName = "Doe", Location = "New York", Age = 25, Weight = 180, Sex = "Male", SportKindId = 1 },
                new User { Id = Guid.NewGuid().ToString(), Name = "Emma", SurName = "Smith", Location = "London", Age = 30, Weight = 150, Sex = "Female", SportKindId = 3 },
                new User { Id = Guid.NewGuid().ToString(), Name = "Michael", SurName = "Johnson", Location = "Los Angeles", Age = 35, Weight = 200, Sex = "Male", SportKindId = 2 },
                new User { Id = Guid.NewGuid().ToString(), Name = "Sophia", SurName = "Wilson", Location = "Sydney", Age = 28, Weight = 160, Sex = "Female", SportKindId = 4 },
                new User { Id = Guid.NewGuid().ToString(), Name = "Daniel", SurName = "Brown", Location = "Berlin", Age = 32, Weight = 175, Sex = "Male", SportKindId = 5 },
                new User { Id = Guid.NewGuid().ToString(), Name = "Maxym", SurName = "Lanchevych", Location = "Lviv", Age = 21, Weight = 74, Sex = "Male", SportKindId = 1 }
            };

            _dbContext.Users.AddRange(users);

            // Create Ratings
            var ratings = new List<Rating>
            {
                new Rating { UserId = users[0].Id, TrainingProgramId = trainingPrograms[0].Id, Score = 4 },
                new Rating { UserId = users[1].Id, TrainingProgramId = trainingPrograms[1].Id, Score = 5 },
                new Rating { UserId = users[2].Id, TrainingProgramId = trainingPrograms[2].Id, Score = 3 },
                new Rating { UserId = users[3].Id, TrainingProgramId = trainingPrograms[3].Id, Score = 4 },
                new Rating { UserId = users[4].Id, TrainingProgramId = trainingPrograms[4].Id, Score = 5 },
                new Rating { UserId = users[5].Id, TrainingProgramId = trainingPrograms[0].Id, Score = 5 }
            };

            _dbContext.Ratings.AddRange(ratings);

            _dbContext.SaveChanges();

            _dbContext.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Users] OFF");
            transaction.Commit();
        }
    }
}
