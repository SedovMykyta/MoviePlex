using Microsoft.EntityFrameworkCore;
using MoviePlex.Infrastructure.Entities;
using MoviePlex.Infrastructure.Entities.Enum;

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
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CinemaHall>().HasData(
            new CinemaHall { Id = 1, Name = "Blue", CountOfSeats = 100, Price = 10 },
            new CinemaHall { Id = 2, Name = "Purple", CountOfSeats = 150, Price = 12 },
            new CinemaHall { Id = 3, Name = "White", CountOfSeats = 120, Price = 11 }
        );
        
        modelBuilder.Entity<Film>().HasData(
            new Film { Id = 1, Name = "Avengers", Duration = TimeSpan.FromMinutes(120), RentalStartDate = DateTime.Now, RentalEndDate = DateTime.Now.AddDays(7), Publisher = "Publisher 1", Genre = GenreFilm.Comedy, Trailer = "Trailer 1" },
            new Film { Id = 2, Name = "Sniper Elite", Duration = TimeSpan.FromMinutes(90), RentalStartDate = DateTime.Now, RentalEndDate = DateTime.Now.AddDays(7), Publisher = "Publisher 2", Genre = GenreFilm.Drama, Trailer = "Trailer 2" },
            new Film { Id = 3, Name = "Spider Man", Duration = TimeSpan.FromMinutes(105), RentalStartDate = DateTime.Now, RentalEndDate = DateTime.Now.AddDays(7), Publisher = "Publisher 3", Genre = GenreFilm.Fantasy, Trailer = "Trailer 3" }
        );
        
        modelBuilder.Entity<Session>().HasData(
            new Session { Id = 1, Date = DateTime.Now, Time = TimeSpan.FromHours(18), CinemaHallId = 1, FilmId = 1 },
            new Session { Id = 2, Date = DateTime.Now, Time = TimeSpan.FromHours(20), CinemaHallId = 2, FilmId = 2 },
            new Session { Id = 3, Date = DateTime.Now, Time = TimeSpan.FromHours(22), CinemaHallId = 3, FilmId = 3 }
        );
    }
}