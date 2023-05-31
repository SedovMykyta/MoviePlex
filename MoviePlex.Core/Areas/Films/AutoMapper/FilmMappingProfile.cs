using AutoMapper;
using MoviePlex.Core.Areas.Films.Dtos;
using MoviePlex.Infrastructure.Entities;

namespace MoviePlex.Core.Areas.Films.AutoMapper;

public class FilmMappingProfile : Profile
{
    public FilmMappingProfile()
    {
        CreateMap<Film, FilmDto>();
        CreateMap<FilmInputDto, Film>()
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.Parse(src.Duration)));
    }
}