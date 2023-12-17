using System.Collections.Generic;
using System.Threading.Tasks;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(string userId);
        Task<IEnumerable<string>> GetRolesAsync(UserDto user);
    }
}
