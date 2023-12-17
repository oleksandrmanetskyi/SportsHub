using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Services.Services.Realizations
{
    public class NutritionService : INutritionService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public NutritionService(IRepositoryWrapper repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<NutritionDto>> GetAllNutritions()
        {
            var nutritions = await _repository.Nutrition.GetAll().ToListAsync();
            return _mapper.Map<List<Nutrition>, List<NutritionDto>>(nutritions);
        }
        public async Task<NutritionDto> GetNutritionByTrainingProgramOfUserAsync(string userId)
        {
            var user = await _repository.User.GetAll(filter: x => x.Id == userId).FirstAsync();
            var program = await _repository.TrainingProgram.GetAll(filter: x => x.Id == user.TrainingProgramId).FirstOrDefaultAsync();
            var nutrition = await _repository.Nutrition.GetAll(filter: x => x.Id == program.NutritionId).FirstAsync();
            return _mapper.Map<Nutrition, NutritionDto>(nutrition);
        }
        public async Task<NutritionDto> GetNutritionAsync(int nutritionId)
        {
            var nutrition = await _repository.Nutrition.GetAll(filter: x => x.Id == nutritionId).FirstAsync();
            return _mapper.Map<Nutrition, NutritionDto>(nutrition);
        }

        public async Task AddNutritionAsync(NutritionDto nutrition)
        {
            var newShop = _mapper.Map<NutritionDto, Nutrition>(nutrition);
            await _repository.Nutrition.CreateAsync(newShop);
        }

        public void UpdateNutrition(NutritionDto nutrition)
        {
            var newShop = _mapper.Map<NutritionDto, Nutrition>(nutrition);
            _repository.Nutrition.Update(newShop);
        }

        public void DeleteNutrition(int nutritionId)
        {
            var nutritions = _repository.Nutrition.GetAll(filter: x => x.Id == nutritionId).First();
            _repository.Nutrition.Delete(nutritions);
        }
    }
}
