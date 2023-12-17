using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Services.Services.Realizations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repoWrapper;
        public AuthService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            IRepositoryWrapper repoWrapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _repoWrapper = repoWrapper;
        }


        public async Task<SignInResult> SignInAsync(LoginDto loginDto)
        {
            var user = _userManager.FindByEmailAsync(loginDto.Email);
            var result = await _signInManager.PasswordSignInAsync(user.Result, loginDto.Password, loginDto.RememberMe, true);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterDto registerDto)
        {
            var user = new User()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                SurName = registerDto.SurName,
                Name = registerDto.Name,
                SportKindId = int.Parse(registerDto.SportKindId),
                Location = registerDto.Location
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            return result;
        }

        public async Task AddRoleAsync(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            await _userManager.AddToRoleAsync(user, "User");
        }

        public async Task<UserDto> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<User, UserDto>(user);
        }

    }
}
