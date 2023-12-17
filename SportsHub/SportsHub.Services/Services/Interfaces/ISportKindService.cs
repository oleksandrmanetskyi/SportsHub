using System.Collections.Generic;
using System.Threading.Tasks;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface ISportKindService
    {
        Task<IEnumerable<SportKindDto>> GetAllSportKindsAsync();
        Task SetUpUserKindOfSportAsync(string userId, int sportKindId);
        Task<SportKindDto> GetSportKindAsync(string userId);
        void UpdateSportKind(SportKindDto sportKind);
        void DeleteSportKind(int sportKindId);
    }
}
