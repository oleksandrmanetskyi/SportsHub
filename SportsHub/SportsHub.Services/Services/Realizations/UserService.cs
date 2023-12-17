using System.Collections.Generic;
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
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IRepositoryWrapper repoWrapper,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserDto> GetUserAsync(string userId)
        {
            var user = await _repoWrapper.User.GetAll().FirstOrDefaultAsync(
                i => i.Id == userId);
            var model = _mapper.Map<User, UserDto>(user);

            return model;
        }

        public async Task<IEnumerable<string>> GetRolesAsync(UserDto user)
        {
            var result = await _userManager.GetRolesAsync(_mapper.Map<UserDto, User>(user));
            return result;
        }
    }
}
