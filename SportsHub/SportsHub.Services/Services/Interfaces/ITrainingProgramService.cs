using System.Collections.Generic;
using System.Threading.Tasks;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface ITrainingProgramService
    {
        Task<IEnumerable<TrainingProgramDto>> GetAllPrograms();
        Task<IEnumerable<TrainingProgramDto>> GetProgramsByUserSportKind(string userId);
        Task<TrainingProgramDto> Get(string userId);
        Task<TrainingProgramDto> Get(int id);
        Task SetTrainingProgramForUser(string userId, int trainingProgramId);
        Task DeleteTrainingProgramForUser(string userId);
        void EditTrainingProgram(TrainingProgramDto trainingProgram);
        Task AddTrainingProgram(TrainingProgramDto trainingProgram);
        void DeleteTrainingProgram(int trainingProgramId);
        Task<List<TrainingProgramDto>> GetRecommendations(string userId, int count);
        Task<List<TrainingProgramDto>> GetNewRecommendations(string userId, int count);
        Task<bool> LikeTrainingProgram(string userId, int trainingProgramId, int rating);
        Task<int?> GetRatingAsync(string userId, int trainingProgramId);
    }
}
