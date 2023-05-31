namespace MoviePlex.Infrastructure.Entities;

public class Session
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }

    public int CinemaHallId { get; set; } 
    public int FilmId { get; set; }

    public Film Film { get; set; }
    public CinemaHall CinemaHall { get; set; }
    public List<Ticket> Tickets { get; set; }
}