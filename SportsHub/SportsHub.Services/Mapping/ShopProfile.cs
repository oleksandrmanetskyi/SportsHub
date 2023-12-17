using AutoMapper;
using SportsHub.DataAccess.Entities;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Mapping
{
    public class ShopProfile: Profile
    {
        public ShopProfile()
        {
            CreateMap<Shop, ShopDto>().ReverseMap();
        }
    }
}
