namespace MoviePlex.Core.Areas.Films.Dtos;

public class FilmDto
{
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public string Publisher { get; set; }

    public string Genre { get; set; }
    public string Trailer { get; set; }
}