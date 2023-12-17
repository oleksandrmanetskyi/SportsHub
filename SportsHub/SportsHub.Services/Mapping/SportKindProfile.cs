using AutoMapper;
using SportsHub.DataAccess.Entities;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Mapping
{
    public class SportKindProfile: Profile
    {
        public SportKindProfile() => CreateMap<SportKind, SportKindDto>().ReverseMap();
    }
}
