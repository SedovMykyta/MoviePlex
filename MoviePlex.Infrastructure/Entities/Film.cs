﻿using MoviePlex.Infrastructure.Entities.Enum;

namespace MoviePlex.Infrastructure.Entities;

public class Film
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public string Publisher { get; set; }

    public GenreFilm Genre { get; set; }
    public string Trailer { get; set; }
    // public IList<string> ImagesBase64 { get; set; }
    
    public IList<Session> Sessions { get; set; }
}      