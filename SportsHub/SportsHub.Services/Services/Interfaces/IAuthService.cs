using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface IAuthService
    {
        Task<SignInResult> SignInAsync(LoginDto loginDto);
        Task SignOutAsync();
        Task<IdentityResult> CreateUserAsync(RegisterDto registerDto);
        Task AddRoleAsync(RegisterDto registerDto);
        Task<UserDto> FindByEmailAsync(string email);

    }
}
