using Microsoft.EntityFrameworkCore;
using SportsHub.RecommendationSystem.DatabaseSeeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DatabaseSeeding
{
    public class SportsHubDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<SportKind> SportKinds { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<TrainingProgramSportKind> TrainingProgramSportKinds { get; set; }
        private string _connectionString;
        //public RecommendationDbContext(DbContextOptions<RecommendationDbContext> options) : base(options)
        //{
        //    Database.Migrate();
        //}
        public SportsHubDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(x => !x.Name.Contains("Identity")).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<User>().Property(x => x.SportKindId).IsRequired(false);
            modelBuilder.Entity<User>().Property(x => x.TrainingProgramId).IsRequired(false);
            modelBuilder.Entity<Recommendation>().HasKey(nameof(Recommendation.UserId), nameof(Recommendation.TrainingProgramId));
            modelBuilder.Entity<Rating>().HasKey(nameof(Rating.UserId), nameof(Rating.TrainingProgramId));

            modelBuilder.Entity<TrainingProgramSportKind>()
        .HasKey(bc => new { bc.SportKindId, bc.TrainingProgramId });
            modelBuilder.Entity<TrainingProgramSportKind>()
                .HasOne(bc => bc.TrainingProgram)
                .WithMany(b => b.TrainingProgramSportKinds)
                .HasForeignKey(bc => bc.TrainingProgramId);
            modelBuilder.Entity<TrainingProgramSportKind>()
                .HasOne(bc => bc.SportKind)
                .WithMany(c => c.TrainingProgramSportKinds)
                .HasForeignKey(bc => bc.SportKindId);
        }
    }
}
