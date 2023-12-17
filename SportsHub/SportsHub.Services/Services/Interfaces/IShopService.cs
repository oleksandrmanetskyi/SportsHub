using System.Collections.Generic;
using System.Threading.Tasks;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Services.Interfaces
{
    public interface IShopService
    {
        Task<IEnumerable<ShopDto>> GetAllShops();
        Task<ShopDto> GetShopAsync(int shopId);
        Task AddShopAsync(ShopDto shop);
        void UpdateShop(ShopDto shop);
        void DeleteShop(int shopId);
    }
}
