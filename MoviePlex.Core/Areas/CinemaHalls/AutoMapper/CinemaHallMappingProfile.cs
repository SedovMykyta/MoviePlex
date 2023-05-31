using AutoMapper;
using MoviePlex.Core.Areas.CinemaHalls.Dtos;
using MoviePlex.Infrastructure.Entities;

namespace MoviePlex.Core.Areas.CinemaHalls.AutoMapper;

public class CinemaHallMappingProfile: Profile
{
    public CinemaHallMappingProfile()
    {
        CreateMap<CinemaHall, CinemaHallDto>();
        CreateMap<CinemaHallInputDto, CinemaHall>();
    }
}