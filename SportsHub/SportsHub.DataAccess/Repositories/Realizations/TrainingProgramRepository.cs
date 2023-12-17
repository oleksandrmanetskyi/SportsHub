using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories.Interfaces;

namespace SportsHub.DataAccess.Repositories.Realizations
{
    public class TrainingProgramRepository: RepositoryBase<TrainingProgram>, ITrainingProgramRepository
    {
        public TrainingProgramRepository(SportsHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}
