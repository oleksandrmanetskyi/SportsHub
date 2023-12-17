using AutoMapper;
using SportsHub.DataAccess.Entities;
using SportsHub.Services.DTO;

namespace SportsHub.Services.Mapping
{
    public class TrainingProgramProfile : Profile
    {
        public TrainingProgramProfile()
        {
            CreateMap<TrainingProgram, TrainingProgramDto>().ReverseMap();
        }
    }
}
