using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsHub.DataAccess.Entities;
using SportsHub.DataAccess.Repositories;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;

namespace SportsHub.Services.Services.Realizations
{
    public class ShopService : IShopService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ShopService(IRepositoryWrapper repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<ShopDto>> GetAllShops()
        {
            var shops = await _repository.Shop.GetAll().ToListAsync();
            return _mapper.Map<List<Shop>, List<ShopDto>>(shops);
        }

        public async Task<ShopDto> GetShopAsync(int shopId)
        {
            var shop = await _repository.Shop.GetAll(filter: x => x.Id == shopId).FirstAsync();
            return _mapper.Map<Shop, ShopDto>(shop);
        }

        public async Task AddShopAsync(ShopDto shop)
        {
            var newShop = _mapper.Map<ShopDto, Shop>(shop);
            await _repository.Shop.CreateAsync(newShop);
        }

        public void UpdateShop(ShopDto shop)
        {
            var newShop = _mapper.Map<ShopDto, Shop>(shop);
            _repository.Shop.Update(newShop);
        }

        public void DeleteShop(int shopId)
        {
            var news = _repository.Shop.GetAll(filter: x => x.Id == shopId).First();
            _repository.Shop.Delete(news);
        }
    }
}
