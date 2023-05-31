using MoviePlex.Infrastructure.Entities.Enum;

namespace MoviePlex.Core.Areas.Films.Dtos;

public class FilmInputDto
{
    public string Name { get; set; }
    public string Duration { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public string Publisher { get; set; }

    public GenreFilm Genre { get; set; }
    public string Trailer { get; set; }
}