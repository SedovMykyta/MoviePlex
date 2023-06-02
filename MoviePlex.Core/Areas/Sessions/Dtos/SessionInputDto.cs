namespace MoviePlex.Core.Areas.Sessions.Dtos;

public class SessionInputDto
{
    public DateTime Date { get; set; }
    public string Time { get; set; }

    public int CinemaHallId { get; set; } 
    public int FilmId { get; set; }
}