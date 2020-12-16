using System;
using Adv.Data.Entities;
using Adv.Web.Dtos;
using AutoMapper;

namespace Adv.Web.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Iteration, IterationDto>();
            CreateMap<IterationCreationUpdateDto, Iteration>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}