using MoviePlex.Infrastructure.Entities.Enum;

namespace MoviePlex.Infrastructure.Entities;

public class Film
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTimeOffset RentalStartDate { get; set; }
    public DateTimeOffset RentalEndDate { get; set; }
    public string Publisher { get; set; }

    public GenreFilm Genre { get; set; }
    public string Trailer { get; set; }
    // public List<string> ImagesBase64 { get; set; }
    
    public IList<Session> Sessions { get; set; }
}      