namespace MoviePlex.Infrastructure.Entities;

public class Ticket
{
    public int Id { get; set; }
    public DateTime DateOfIssue { get; set; }
    public int NumberOfSeats { get; set; }

    public int SessionId { get; set; }

    public Session Session { get; set; }
}