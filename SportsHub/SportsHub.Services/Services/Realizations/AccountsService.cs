using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Services.Services.Realizations
{
    public class AccountsService : IAccountsService
    {
        //private readonly UserManager<User> _userManager;
        private IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public AccountsService(IMapper mapper, UserManager<User> userManager, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            //_userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repositoryWrapper.User.GetAll().ToListAsync();
            return _mapper.Map<List<User>, List<UserDto>>(users);
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            var user = await _repositoryWrapper.User.GetAll().FirstAsync(x=>x.Id==userId);
            return _mapper.Map<User, UserDto>(user);
        }

        public void UpdateUser(UserDto user)
        {
            var newUser = _mapper.Map<UserDto, User>(user);
            _repositoryWrapper.User.Update(newUser);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _repositoryWrapper.User.GetAll().FirstAsync(x => x.Id == userId);
            _repositoryWrapper.User.Delete(user);
        }
    }
}
