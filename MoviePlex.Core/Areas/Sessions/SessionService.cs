using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviePlex.Core.Areas.Sessions.Dtos;
using MoviePlex.Core.Areas.Validators;
using MoviePlex.Core.Exceptions;
using MoviePlex.Infrastructure;
using MoviePlex.Infrastructure.Entities;

namespace MoviePlex.Core.Areas.Sessions;

public class SessionService : ISessionService
{
    private readonly MovieContext _context;
    private readonly IMapper _mapper;
    private readonly IValidationService _validationService;

    public SessionService(MovieContext context, IMapper mapper, IValidationService validationService)
    {
        _context = context;
        _mapper = mapper;
        _validationService = validationService;
    }

    public async Task<List<SessionDto>> GetListAsync()
    {
        var sessions = await _context.Sessions
            .Select(session => _mapper.Map<SessionDto>(session))
            .ToListAsync();

        return sessions;
    }

    public async Task<List<SessionDto>> GetListByCinemaHallIdAsync(int cinemaHallId)
    {
        var sessions = await _context.Sessions
            .Where(session => session.CinemaHallId == cinemaHallId)
            .Select(session => _mapper.Map<SessionDto>(session))
            .ToListAsync();

        return sessions;
    }

    public async Task<List<SessionDto>> GetListByFilmIdAsync(int filmId)
    {
        var sessions = await _context.Sessions
            .Where(session => session.FilmId == filmId)
            .Select(session => _mapper.Map<SessionDto>(session))
            .ToListAsync();

        return sessions;
    }

    public async Task<SessionDto> GetByIdAsync(int id)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(session => session.Id == id)
                      ?? throw new NotFoundException($"Session with Id: {id} not found");

        var sessionDto = _mapper.Map<SessionDto>(session);

        return sessionDto;
    }

    public async Task<SessionDto> CreateAsync(SessionInputDto sessionInput)
    {
        await _validationService.ValidateAndThrowAsync(sessionInput);

        await ThrowIfSameSessionExistAsync(sessionInput);
        await ThrowIfFilmIdOrCinemaHallIdNotFoundAsync(sessionInput);

        var session = _mapper.Map<Session>(sessionInput);

        await _context.Sessions.AddAsync(session);
        await _context.SaveChangesAsync();

        var sessionDto = _mapper.Map<SessionDto>(session);

        return sessionDto;
    }

    public async Task<SessionDto> UpdateByIdAsync(int id, SessionInputDto sessionInput)
    {
        await _validationService.ValidateAndThrowAsync(sessionInput);

        var session = await _context.Sessions.FirstOrDefaultAsync(film => film.Id == id)
                      ?? throw new NotFoundException($"Session with Id: {id} not found");

        await ThrowIfSameSessionExistAsync(sessionInput, id);
        await ThrowIfFilmIdOrCinemaHallIdNotFoundAsync(sessionInput);
        
        _mapper.Map(sessionInput, session);

        _context.Sessions.Update(session);
        await _context.SaveChangesAsync();

        var sessionDto = _mapper.Map<SessionDto>(session);

        return sessionDto;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(film => film.Id == id)
                      ?? throw new NotFoundException($"Session with Id: {id} not found");

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();
    }

    private async Task ThrowIfSameSessionExistAsync(SessionInputDto sessionInput, int id = -1)
    {
        if (await _context.Sessions.AnyAsync(session =>
                session.Date == sessionInput.Date &&
                session.CinemaHallId == sessionInput.CinemaHallId &&
                session.FilmId == sessionInput.FilmId &&
                session.Time == TimeSpan.Parse(sessionInput.Time) &&
                session.Id != id))
        {
            throw new BadRequestException("Same session existing");
        }
    }

    private async Task ThrowIfFilmIdOrCinemaHallIdNotFoundAsync(SessionInputDto sessionInput)
    {
        if (! await _context.CinemaHalls.AnyAsync(cinemaHall => cinemaHall.Id == sessionInput.CinemaHallId))
        {
            throw new NotFoundException($"Cinema hall with Id: {sessionInput.CinemaHallId} not found");
        }
        if (! await _context.Films.AnyAsync(film => film.Id == sessionInput.FilmId))
        {
            throw new NotFoundException($"Film with Id: {sessionInput.FilmId} not found");
        }
    }
}