using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MoviePlex.Core.Areas.CinemaHalls.Dtos;
using MoviePlex.Core.Exceptions;
using MoviePlex.Infrastructure;
using MoviePlex.Infrastructure.Entities;

namespace MoviePlex.Core.Areas.CinemaHalls;

public class CinemaHallService : ICinemaHallService
{
    private readonly MovieContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CinemaHallInputDto> _validator;
    
    public CinemaHallService(MovieContext context, IMapper mapper, IValidator<CinemaHallInputDto> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<List<CinemaHallDto>> GetListAsync()
    {
        var halls = await _context.CinemaHalls
            .Select(hall => _mapper.Map<CinemaHallDto>(hall))
            .ToListAsync();

        return halls;
    }

    public async Task<CinemaHallDto> GetByIdAsync(int id)
    {
        var hall = await _context.CinemaHalls.FirstOrDefaultAsync(hall => hall.Id == id) 
                   ?? throw new NotFoundException($"Cinema hall with Id: {id} not found");

        var hallDto = _mapper.Map<CinemaHallDto>(hall);

        return hallDto;
    }

    public async Task<List<CinemaHallDto>> GetByNameAsync(string name)
    {
        var halls = await _context.CinemaHalls
            .Where(hall => hall.Name.Contains(name) || name.Contains(hall.Name))
            .Select(hall => _mapper.Map<CinemaHallDto>(hall))
            .ToListAsync();

        return halls;
    }

    public async Task<CinemaHallDto> CreateAsync(CinemaHallInputDto cinemaHallInput)
    {
        await ThrowIfNotValidCinemaHallInput(cinemaHallInput);

        await ThrowIfNameExistAsync(cinemaHallInput.Name);
        
        var hall = _mapper.Map<CinemaHall>(cinemaHallInput);

        await _context.CinemaHalls.AddAsync(hall);
        await _context.SaveChangesAsync();

        var hallDto = _mapper.Map<CinemaHallDto>(hall);
        
        return hallDto;
    }

    public async Task<CinemaHallDto> UpdateByIdAsync(int id, CinemaHallInputDto cinemaHallInput)
    {
        var hall = await _context.CinemaHalls.FirstOrDefaultAsync(hall => hall.Id == id) 
                   ?? throw new NotFoundException($"Cinema hall with Id: {id} not found");

        await ThrowIfNotValidCinemaHallInput(cinemaHallInput);

        await ThrowIfNameExistAsync(cinemaHallInput.Name, hall.Id);
        
        _mapper.Map(cinemaHallInput, hall);

        _context.CinemaHalls.Update(hall);
        await _context.SaveChangesAsync();
        
        var hallDto = _mapper.Map<CinemaHallDto>(hall);

        return hallDto;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var hall = await _context.CinemaHalls.FirstOrDefaultAsync(film => film.Id == id) 
                   ?? throw new NotFoundException($"Cinema hall with Id: {id} not found");
        
        _context.CinemaHalls.Remove(hall);
        await _context.SaveChangesAsync();
    }
    
    
    private async Task ThrowIfNameExistAsync(string name, int id = -1)
    {
        if (await _context.CinemaHalls.AnyAsync(hall => hall.Name == name && hall.Id != id))
        {
            throw new BadRequestException($"Cinema hall with Name: {name} exists");
        }
    }

    private async Task ThrowIfNotValidCinemaHallInput(CinemaHallInputDto hallInput)
    {
        ValidationResult result = await _validator.ValidateAsync(hallInput);

        if (!result.IsValid)
        {
            var errors = new Dictionary<string, string>();
            foreach (var failure in result.Errors)
            {
                if (errors.ContainsKey(failure.PropertyName))
                {
                    errors[failure.PropertyName] += "; " + failure.ErrorMessage;
                }
                else
                {
                    errors[failure.PropertyName] = failure.ErrorMessage;
                }
            }
            throw new ValidationErrorException(errors);
        }
    }
}