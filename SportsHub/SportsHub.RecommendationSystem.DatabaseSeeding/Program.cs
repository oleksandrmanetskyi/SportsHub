using SportsHub.RecommendationSystem.DatabaseSeeding;
using System;

namespace SportsHub.RecommendationSystem.DatabaseSeeding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var database = new RecommendationDbContext(@"Data Source=127.0.0.1;Initial Catalog=RecommendationDbMedium;User ID=sa;Password=I8well4sure;Encrypt=false;TrustServerCertificate=true");
            //database.Database.EnsureDeleted();
            //database.Database.EnsureCreated();
            //var seeder = new MediumRecommendationDbContextSeeder(database);
            //seeder.Seed();
            CreateTestDbs();
        }

        static void CreateTestDbs()
        {
            var database = new SportsHubDbContext(@"Data Source=127.0.0.1;Initial Catalog=RecommendationDbTestTrain;User ID=sa;Password=I8well4sure;Encrypt=false;TrustServerCertificate=true");
            database.Database.EnsureDeleted();
            database.Database.EnsureCreated();
            var seeder = new RecommendationDbContextSeederForTests(database);
            seeder.Seed(70, true);

            var database2 = new SportsHubDbContext(@"Data Source=127.0.0.1;Initial Catalog=RecommendationDbTestForTest;User ID=sa;Password=I8well4sure;Encrypt=false;TrustServerCertificate=true");
            database2.Database.EnsureDeleted();
            database2.Database.EnsureCreated();
            var seeder2 = new RecommendationDbContextSeederForTests(database2);
            seeder2.Seed(70, false);
        }
    }
}
