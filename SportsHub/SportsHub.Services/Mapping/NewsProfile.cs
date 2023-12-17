using AutoMapper;
using SportsHub.DataAccess.Entities;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Mapping
{
    public class NewsProfile: Profile
    {
        public NewsProfile() => CreateMap<News, NewsDto>().ReverseMap();
    }
}
