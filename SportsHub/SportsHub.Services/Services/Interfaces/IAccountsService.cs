using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface IAccountsService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetUserAsync(string userId);
        void UpdateUser(UserDto user);
        Task DeleteUserAsync(string userId);
    }
}
