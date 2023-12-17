using AutoMapper;
using SportsHub.DataAccess.Entities;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Mapping
{
    public class SportPlaceProfile : Profile
    {
        public SportPlaceProfile()
        {
            CreateMap<SportPlace, SportPlaceDto>().ReverseMap();
        }
    }
}
