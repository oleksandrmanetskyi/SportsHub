using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsHub.DataAccess.Entities;

namespace SportsHub.DataAccess
{
    public class SportsHubDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Nutrition> Nutritions { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<SportKind> SportKinds { get; set; }
        public DbSet<SportPlace> SportPlaces { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public SportsHubDbContext(DbContextOptions<SportsHubDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //var types = modelBuilder.Model.GetEntityTypes().Where(x => !x.Name.Contains("Identity")).ToList();
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(x => !x.Name.Contains("Identity")).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //modelBuilder.Entity<User>()
            //  .HasMany(p => p.)
            //  .WithMany(t => t.Users)
            //  .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Nutrition>().HasOne<TrainingProgram>().WithOne(x => x.Nutrition).HasForeignKey<TrainingProgram>(x=>x.NutritionId).IsRequired(false);
            //modelBuilder.Entity<User>().HasOne<TrainingProgram>().WithOne(x => x.User).HasForeignKey<User>(x=>x.TrainingProgramId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            //////////modelBuilder.Entity<TrainingProgram>().HasOne<SportKind>().WithMany(x => x.TrainingPrograms).HasForeignKey(x=>x.SportKindId).OnDelete(DeleteBehavior.Restrict);
            ////////////modelBuilder.Entity<User>().HasOne<SportKind>().WithMany(x => x.Users).HasForeignKey(x=>x.SportKindId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            //////////modelBuilder.Entity<SportKind>().HasMany<TrainingProgram>().WithOne(x=>x.SportKind).HasForeignKey(x=>x.SportKindId).OnDelete(DeleteBehavior.Restrict);
            //////////modelBuilder.Entity<SportKind>().HasMany<User>().WithOne(x => x.SportKind).HasForeignKey(x=>x.SportKindId).OnDelete(DeleteBehavior.Restrict);
            ////////////modelBuilder.Entity<SportKind>().Property(x=>x.Id).
            //modelBuilder.Entity<User>().HasOne(e => e.SportKind).WithMany(y => y.Users);
            //modelBuilder.Entity<User>().HasOne(e => e.TrainingProgram).WithMany(y => y.Users);
            modelBuilder.Entity<User>().Property(x => x.SportKindId).IsRequired(false);
            modelBuilder.Entity<User>().Property(x => x.TrainingProgramId).IsRequired(false);
            //modelBuilder.Entity<TrainingProgram>().Property(x => x.SportKindId).IsRequired(false);
            modelBuilder.Entity<Recommendation>().HasKey(nameof(Recommendation.UserId), nameof(Recommendation.TrainingProgramId));
            //    .HasForeignKey(x => x.SportKindId).IsRequired(false);
            modelBuilder.Seed();
        }

    }
}
