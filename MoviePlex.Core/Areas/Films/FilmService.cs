using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MoviePlex.Core.Areas.Films.Dtos;
using MoviePlex.Core.Exceptions;
using MoviePlex.Infrastructure;
using MoviePlex.Infrastructure.Entities;
using MoviePlex.Infrastructure.Entities.Enum;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace MoviePlex.Core.Areas.Films;

public class FilmService : IFilmService
{
    private readonly MovieContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<FilmInputDto> _validator;
    
    public FilmService(MovieContext context, IMapper mapper, IValidator<FilmInputDto> validator)
    {
        _context = context;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<List<FilmDto>> GetListAsync()
    {
        var films = await _context.Films
            .Select(film => _mapper.Map<FilmDto>(film))
            .ToListAsync();

        return films;
    }

    public async Task<FilmDto> GetByIdAsync(int id)
    {
        var film = await _context.Films.FirstOrDefaultAsync(film => film.Id == id) 
                   ?? throw new NotFoundException($"Film with Id: {id} not found");

        var filmDto = _mapper.Map<FilmDto>(film);

        return filmDto;
    }

    public async Task<List<FilmDto>> GetByGenreAsync(GenreFilm genreFilm)
    {
        var films = await _context.Films
            .Where(film => film.Genre == genreFilm)
            .Select(film => _mapper.Map<FilmDto>(film))
            .ToListAsync();

        return films;
    }

    public async Task<List<FilmDto>> GetByNameAsync(string name)
    {
        var films = await _context.Films
            .Where(film => film.Name.Contains(name) || name.Contains(film.Name))
            .Select(film => _mapper.Map<FilmDto>(film))
            .ToListAsync();

        return films;
    }

    public async Task<FilmDto> CreateAsync(FilmInputDto filmInput)
    {
        await ThrowIfNotValidFilmInput(filmInput);

        await ThrowIfNameExistAsync(filmInput.Name);
        
        var film = _mapper.Map<Film>(filmInput);

        await _context.Films.AddAsync(film);
        await _context.SaveChangesAsync();

        var filmDto = _mapper.Map<FilmDto>(film);
        
        return filmDto;
    }

    public async Task<FilmDto> UpdateByIdAsync(int id, FilmInputDto filmInput)
    {
        var film = await _context.Films.FirstOrDefaultAsync(film => film.Id == id) 
                   ?? throw new NotFoundException($"Film with Id: {id} not found");

        await ThrowIfNotValidFilmInput(filmInput);

        await ThrowIfNameExistAsync(filmInput.Name, film.Id);
        
        _mapper.Map(filmInput, film);

        _context.Films.Update(film);
        await _context.SaveChangesAsync();
        
        var filmDto = _mapper.Map<FilmDto>(film);

        return filmDto;
    }
    
    public async Task DeleteByIdAsync(int id)
    {
        var film = await _context.Films.FirstOrDefaultAsync(film => film.Id == id) 
                   ?? throw new NotFoundException($"Film with Id: {id} not found");
        
        _context.Films.Remove(film);
        await _context.SaveChangesAsync();
    }

    
    private async Task ThrowIfNameExistAsync(string name, int id = -1)
    {
        if (await _context.Films.AnyAsync(film => film.Name == name && film.Id != id))
        {
            throw new BadRequestException($"Film with Name: {name} exists");
        }
    }

    private async Task ThrowIfNotValidFilmInput(FilmInputDto filmInput)
    {
        ValidationResult result = await _validator.ValidateAsync(filmInput);

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