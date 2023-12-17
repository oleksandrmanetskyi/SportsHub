using Microsoft.EntityFrameworkCore;
using SportsHub.RecommendationSystem.DatabaseSeeding.FileParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DatabaseSeeding
{
    public class RecommendationDbContextSeederForTests
    {
        private readonly SportsHubDbContext _dbContext;

        public RecommendationDbContextSeederForTests(SportsHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed(int persents, bool toTake)
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

            // Parse data
            var movies = SCVParser.ParseMoviesCSV("../../../Datasets/imdb_top_1000.csv");
            var netflixMovies = SCVParser.ParseNetflixMoviesCSV("../../../Datasets/Netflix_Dataset_Movie.csv");
            var intersectedMovies = movies.Select(x => x.Key).Intersect(netflixMovies.Select(x => x.Key));
            var finalMovies = movies.Where(x => intersectedMovies.Contains(x.Key)).Select(x => new KeyValuePair<string, string[]>(x.Key, x.Value.Append(netflixMovies.First(y => y.Key == x.Key).Value).ToArray())).ToList();

            var trainingPrograms = MapMoviesToTrainingPrograms(finalMovies);
            _dbContext.TrainingPrograms.AddRange(trainingPrograms);

            var trainingProgramSportKinds = MapMoviesToTrainingProgramSportKinds(finalMovies);
            _dbContext.TrainingProgramSportKinds.AddRange(trainingProgramSportKinds);

            _dbContext.SaveChanges();

            _dbContext.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[TrainingPrograms] OFF");

            var hashset = finalMovies.Select(y => y.Value[2]).ToHashSet();
            var allRatings = SCVParser.ParseRatingsCSV("../../../Datasets/Netflix_Dataset_Rating.csv", x => hashset.Contains(x[2]));
            var ratings = allRatings.Select(z => new Rating
            {
                UserId = z[0], // here it's int but we have guid in db
                Score = int.Parse(z[1]),
                TrainingProgramId = int.Parse(z[2])
            }).ToList();

            _dbContext.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Users] ON");

            var users = ratings.Select(x => x.UserId).Distinct().Select(x => GenerateRandomUser(x)).ToList();

            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();

            _dbContext.Database.ExecuteSql($"SET IDENTITY_INSERT [dbo].[Users] OFF");

            if (toTake)
            {
                int ratingsToInsert = (ratings.Count() / 100) * persents;

                _dbContext.Ratings.AddRange(ratings.Take(ratingsToInsert));
            }
            else
            {
                int ratingsToSkip = (ratings.Count() / 100) * persents;

                _dbContext.Ratings.AddRange(ratings.Skip(ratingsToSkip));
            }


            _dbContext.SaveChanges();

            transaction.Commit();
        }

        User GenerateRandomUser(string userId)
        {
            Random random = new Random();

            string[] names = { "John", "Maxym", "Bohdan", "Nazar", "Oleh", "Jack", "Taras", "Oksana", "John", "Jane", "Michael", "Emily", "David", "Olivia", "Daniel", "Sophia", "Matthew", "Ava" };
            string[] surnames = { "Smith", "Lanchevych", "Klychko", "Ivanochko", "Levytskiy", "Shestopalov", "Kane", "Topaz", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Taylor", "Anderson" };
            string[] locations = { "Lviv", "Mykolaiv", "Zbarazh", "New York", "Los Angeles", "London", "Paris", "Tokyo", "Sydney", "Toronto", "Berlin", "Kyiv", "Dubai" };
            string[] sexes = { "Male", "Female" };


            User user = new User
            {
                Id = userId,
                Name = GetRandomItem(names, random),
                SurName = GetRandomItem(surnames, random),
                Location = GetRandomItem(locations, random),
                Age = random.Next(18, 65),
                Weight = random.Next(50, 100),
                Sex = GetRandomItem(sexes, random)
            };

            return user;
        }

        T GetRandomItem<T>(T[] array, Random random)
        {
            int index = random.Next(array.Length);
            return array[index];
        }

        private List<TrainingProgramSportKind> MapMoviesToTrainingProgramSportKinds(List<KeyValuePair<string, string[]>> finalMovies)
        {
            List<TrainingProgramSportKind> trainingProgramSportKinds = new List<TrainingProgramSportKind>();
            var genres = string.Join(",", finalMovies.Select(x => x.Value[0].Trim())).Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Distinct();

            foreach (var kvp in finalMovies)
            {
                var itemGenres = kvp.Value[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                foreach (var genre in itemGenres)
                {
                    var item = new TrainingProgramSportKind
                    {
                        TrainingProgramId = int.Parse(kvp.Value[2]),
                        SportKindId = MapSportKindToSportKind(genres.ToList(), genre),

                    };
                    trainingProgramSportKinds.Add(item);
                }

            }
            return trainingProgramSportKinds;
        }

        private int MapSportKindToSportKind(List<string> genres, string genre)
        {
            GenreMapper mapper = new GenreMapper(_dbContext, genres);
            return mapper.MapGenreToSportKind(genre);
        }

        private List<TrainingProgram> MapMoviesToTrainingPrograms(List<KeyValuePair<string, string[]>> finalMovies)
        {
            List<TrainingProgram> trainingPrograms = new List<TrainingProgram>();
            foreach (var kvp in finalMovies)
            {
                var program = new TrainingProgram
                {
                    Id = int.Parse(kvp.Value[2]),
                    Name = kvp.Key,
                    Description = kvp.Value[1],

                };
                trainingPrograms.Add(program);
            }
            return trainingPrograms;
        }
    }
}
