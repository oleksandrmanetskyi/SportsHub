using System.Collections.Generic;
using System.Threading.Tasks;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface INutritionService
    {
        Task<IEnumerable<NutritionDto>> GetAllNutritions();
        Task<NutritionDto> GetNutritionByTrainingProgramOfUserAsync(string userId);
        Task<NutritionDto> GetNutritionAsync(int nutritionId);
        Task AddNutritionAsync(NutritionDto nutrition);
        void UpdateNutrition(NutritionDto nutrition);
        void DeleteNutrition(int nutritionId);
    }
}
