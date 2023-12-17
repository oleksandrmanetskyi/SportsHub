using AutoMapper;
using SportsHub.DataAccess.Entities;
using SportsHub.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsHub.Services.Mapping
{
    public class RecommendationProfile : Profile
    {
        public RecommendationProfile()
        {
            CreateMap<Recommendation, RecommendationDto>().ReverseMap();
        }
    }
}
