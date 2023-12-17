using Microsoft.EntityFrameworkCore;
using SportsHub.RecommendationSystem.DTO;
using SportsHub.RecommendationSystem.Services.DTO;

namespace SportsHub.RecommendationSystem.Services.Database
{
    public class RecommendationsDbContext : DbContext
    {
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<SvdResult> SvdResults { get; set; }
        public DbSet<TrainingProgramBiase> TrainingProgramBiases { get; set; }
        public DbSet<UserBiase> UserBiases { get; set; }
        public DbSet<UserFeature> UserFeatures { get; set; }
        public DbSet<TrainingProgramFeature> TrainingProgramFeatures { get; set; }

        public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
            //Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().HasKey(nameof(Rating.UserId), nameof(Rating.TrainingProgramId));
        }
    }
}
