using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportsHub.Services.Services.Realizations
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        private readonly IRecommendationsRestClient _restClient;
        private const string InvalidUserMessage = "Specified user does not exist.";

        public TrainingProgramService(IRepositoryWrapper repository, IMapper mapper, UserManager<User> userManager, IRecommendationsRestClient restClient)
        {
            this._repository = repository;
            this._mapper = mapper;
            _userManager = userManager;
            this._restClient = restClient;
        }

        public async Task<IEnumerable<TrainingProgramDto>> GetAllPrograms()
        {
            var programs = await _repository.TrainingProgram.GetAll().ToListAsync();
            return _mapper.Map<List<TrainingProgram>, List<TrainingProgramDto>>(programs);
        }
        public async Task<IEnumerable<TrainingProgramDto>> GetProgramsByUserSportKind(string userId)
        {
            var sportKindId = (await _userManager.FindByIdAsync(userId)).SportKindId;
            var programs = await _repository.TrainingProgram.GetAll(filter:x=>x.SportKindId==sportKindId).ToListAsync();
            return _mapper.Map<List<TrainingProgram>, List<TrainingProgramDto>>(programs);
        }
        public async Task<TrainingProgramDto> Get(string userId)
        {
            var user = await _repository.User.GetAll(filter: x => x.Id == userId, include:x=>x.Include(t=>t.TrainingProgram)).FirstOrDefaultAsync();
            return _mapper.Map<TrainingProgram, TrainingProgramDto>(user.TrainingProgram);
        }
        public async Task<TrainingProgramDto> Get(int id)
        {
            var program = await _repository.TrainingProgram.GetAll(filter: x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<TrainingProgram, TrainingProgramDto>(program);
        }
        public async Task SetTrainingProgramForUser(string userId, int trainingProgramId)
        {
            var user = await _repository.User.GetAll(filter: x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null) 
            {
                throw new ArgumentNullException(InvalidUserMessage);
            }
            user.TrainingProgramId = trainingProgramId;
            _repository.User.Update(user);
        }

        public async Task DeleteTrainingProgramForUser(string userId)
        {
            var user = await _repository.User.GetAll(filter: x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentNullException(InvalidUserMessage);
            }
            user.TrainingProgramId = null;
            _repository.User.Update(user);
        }

        public void EditTrainingProgram(TrainingProgramDto trainingProgram)
        {
            var program = _mapper.Map<TrainingProgramDto, TrainingProgram>(trainingProgram);
            _repository.TrainingProgram.Update(program);
        }

        public async Task AddTrainingProgram(TrainingProgramDto trainingProgram)
        {
            var program = _mapper.Map<TrainingProgramDto, TrainingProgram>(trainingProgram);
            await _repository.TrainingProgram.CreateAsync(program);
        }
        public void DeleteTrainingProgram(int trainingProgramId)
        {
            var trainingProgram = _repository.TrainingProgram.GetAll(filter: x => x.Id == trainingProgramId).First();
            var followedUsers = _repository.User.GetAll(filter: x => x.TrainingProgramId == trainingProgramId).ToList();
            foreach(var user in followedUsers)
            {
                user.TrainingProgramId = null;
                _repository.User.Update(user);
            }
            _repository.TrainingProgram.Delete(trainingProgram);
        }

        public async Task<List<TrainingProgramDto>> GetRecommendations(string userId, int count)
        {
            var recommendations = _repository.Recommendation.GetAll(filter: x => x.UserId == userId && x.IsActive==true).Select(x => x.TrainingProgramId).ToList();
            if (recommendations.Count() >= count)
            {
                var programs = _repository.TrainingProgram.GetAll(filter: x => recommendations.Contains(x.Id)).ToList();
                return _mapper.Map< List<TrainingProgram>, List<TrainingProgramDto>>(programs);
            }
            int neededCount = count - recommendations.Count();
            var suggestions = await _restClient.GetRecommendationsAsync(userId, neededCount);
            if (suggestions == null || !suggestions.Any())
            {
                if (recommendations.Count() != 0)
                {
                    var programs = _repository.TrainingProgram.GetAll(filter: x => recommendations.Contains(x.Id)).ToList();
                    return _mapper.Map<List<TrainingProgram>, List<TrainingProgramDto>>(programs);
                }
                return null;
            }
            if (suggestions.All(x => x.ScoreAssumption < 0))
            {
                //тут буде ще фільтрація по типу спорту юзера
                suggestions = await ApplyContentFiltering(suggestions, userId, count);
            }

            foreach (var item in suggestions)
            {
                await _repository.Recommendation.CreateAsync(new Recommendation
                {
                    UserId = item.UserId,
                    TrainingProgramId = item.TrainingProgramId,
                    ScoreAssumption = item.ScoreAssumption,
                    CreatedOn = DateTime.UtcNow,
                    IsActive = true
                });
            }

            await _repository.SaveAsync();

            var suggestionIds = suggestions.Select(x => x.TrainingProgramId).ToList();
            var programsResult = _repository.TrainingProgram.GetAll(filter: x => suggestionIds.Contains(x.Id)).ToList();
            return _mapper.Map<List<TrainingProgram>, List<TrainingProgramDto>>(programsResult);
        }

        private async Task<List<RecommendationDto>> ApplyContentFiltering(List<RecommendationDto> suggestions, string userId, int count)
        {
            int randomCount = suggestions.Where(x => x.ScoreAssumption < 0).Count();
            int filteringPercent = 60; // 60 відсотків буде по спорт типу і 40 рандомних
            var sportKindRecommendations = await this.GetProgramsByUserSportKind(userId);
            var result = sportKindRecommendations.Take((count / 100) * filteringPercent).Select(x => new RecommendationDto
            {
                TrainingProgramId = x.Id,
                ScoreAssumption = 0,
                UserId = userId
            }).ToList();

            for (int i = 0; i < suggestions.Count; i++)
            {
                suggestions[i].ScoreAssumption += 10;
            }

            result.AddRange(suggestions.Take(count - result.Count));
            return result;
        }

        public async Task<List<TrainingProgramDto>> GetNewRecommendations(string userId, int count)
        {
            var recommendations = _repository.Recommendation.GetAll(filter: x => x.UserId == userId).ToList();
            if (recommendations.Count() != 0)
            {
                for (int i = 0; i < recommendations.Count; i++)
                {
                    recommendations[i].IsActive = false;
                    _repository.Recommendation.Update(recommendations[i]);
                }
            }

            var suggestions = await _restClient.GetRecommendationsAsync(userId, count);//отут перевірити чи не треба брати нових=старі+нові каунт,бо воно може кожен раз вертати ті самі
            if (suggestions == null || !suggestions.Any())
            {
                return null;
            }
            var newSuggestionIds = suggestions.Select(x => x.TrainingProgramId).Except(recommendations.Select(y => y.TrainingProgramId)).ToList();
            var newSuggestions = suggestions.Where(x => newSuggestionIds.Contains(x.TrainingProgramId)).ToList();
            foreach (var item in newSuggestions)
            {
                await _repository.Recommendation.CreateAsync(new Recommendation
                {
                    UserId = item.UserId,
                    TrainingProgramId = item.TrainingProgramId,
                    ScoreAssumption = item.ScoreAssumption,
                    CreatedOn = DateTime.UtcNow,
                    IsActive = true
                });
            }
            await _repository.SaveAsync();

            var suggestionIds = newSuggestions.Select(x => x.TrainingProgramId).ToList();
            var programsResult = _repository.TrainingProgram.GetAll(filter: x => suggestionIds.Contains(x.Id)).ToList();
            return _mapper.Map<List<TrainingProgram>, List<TrainingProgramDto>>(programsResult);
        }

        public async Task<bool> LikeTrainingProgram(string userId, int trainingProgramId, int rating)
        {
           return await _restClient.LikeAsync(userId, trainingProgramId, rating);
        }

        public async Task<int?> GetRatingAsync(string userId, int trainingProgramId)
        {
            return await _restClient.GetRatingAsync(userId, trainingProgramId);
        }
    }
}
