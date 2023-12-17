using AutoMapper;
using SportsHub.DataAccess.Entities;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Mapping
{
    public class NutritionProfile : Profile
    {
        public NutritionProfile()
        {
            CreateMap<Nutrition, NutritionDto>().ReverseMap();
        }
    }
}
