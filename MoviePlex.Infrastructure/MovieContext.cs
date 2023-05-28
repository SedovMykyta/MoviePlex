using Microsoft.EntityFrameworkCore;
using MoviePlex.Infrastructure.Entities;

namespace MoviePlex.Infrastructure;

public class MovieContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<CinemaHall> CinemaHalls { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
    }
}