using MoviePlex.Infrastructure.Entities;

namespace MoviePlex.Core.Areas.Sessions.Dtos;

public class SessionDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }

    public int CinemaHallId { get; set; } 
    public int FilmId { get; set; }
    
    public List<Ticket> Tickets { get; set; }
}