using AutoMapper;
using SportsHub.DataAccess.Entities;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Mapping
{
    public class UserProfile: Profile
    {
        public UserProfile() => CreateMap<User, UserDto>().ReverseMap();
    }
}
