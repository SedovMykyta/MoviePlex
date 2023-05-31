﻿using MoviePlex.Infrastructure.Entities;

namespace MoviePlex.Core.Areas.CinemaHalls.Dtos;

public class CinemaHallDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountOfSeats { get; set; }
    public decimal Price { get; set; }

    public List<Session> Sessions { get; set; }
}