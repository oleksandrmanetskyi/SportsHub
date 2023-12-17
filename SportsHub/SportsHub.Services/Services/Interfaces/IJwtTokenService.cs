using System.Threading.Tasks;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateJwtTokenAsync(UserDto userDto);
    }
}
