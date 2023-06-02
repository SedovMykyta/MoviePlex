using AutoMapper;
using MoviePlex.Core.Areas.Sessions.Dtos;
using MoviePlex.Infrastructure.Entities;

namespace MoviePlex.Core.Areas.Sessions.AutoMapper;

public class SessionMappingProfile : Profile
{
    public SessionMappingProfile()
    {
        CreateMap<Session, SessionDto>();
        CreateMap<SessionInputDto, Session>()
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => TimeSpan.Parse(src.Time)));
    }
}