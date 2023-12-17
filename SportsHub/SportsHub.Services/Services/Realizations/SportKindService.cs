using System;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;

namespace SportsHub.Services.Services.Realizations
{
    public class SportKindService : ISportKindService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private const string InvalidUserMessage = "Specified user does not exist.";

        public SportKindService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SportKindDto>> GetAllSportKindsAsync()
        {
            var sportKinds = await _repository.SportKind.GetAll().ToListAsync();
            return _mapper.Map<List<SportKind>, List<SportKindDto>>(sportKinds);
        }

        public async Task<SportKindDto> GetSportKindAsync(string userId)
        {
            var user = await _repository.User.GetAll(include: source => source.Include(x => x.SportKind),
                filter: s => s.Id == userId).FirstAsync();
            if (user == null)
            {
                throw new ArgumentNullException(InvalidUserMessage);
            }

            return _mapper.Map<SportKind, SportKindDto>(user.SportKind);
        }

        public async Task SetUpUserKindOfSportAsync(string userId, int sportKindId)
        {
            var user = await _repository.User.GetAll(filter: x => x.Id == userId).FirstAsync();
            if (user == null)
            {
                throw new ArgumentNullException(InvalidUserMessage);
            }

            user.SportKindId = sportKindId;
            _repository.User.Update(user);
        }
        public void UpdateSportKind(SportKindDto sportKind)
        {
            var newSportKind = _mapper.Map<SportKindDto, SportKind>(sportKind);
            _repository.SportKind.Update(newSportKind);
        }

        public void DeleteSportKind(int sportKindId)
        {
            var sportKind = _repository.SportKind.GetAll(filter: x => x.Id == sportKindId).First();
            _repository.SportKind.Delete(sportKind);
        }
    }
}
