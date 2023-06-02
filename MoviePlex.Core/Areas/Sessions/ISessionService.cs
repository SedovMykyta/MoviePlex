using MoviePlex.Core.Areas.Sessions.Dtos;

namespace MoviePlex.Core.Areas.Sessions;

public interface ISessionService
{
    public Task<List<SessionDto>> GetListAsync();

    public Task<List<SessionDto>> GetListByCinemaHallIdAsync(int cinemaHallId);
    
    public Task<List<SessionDto>> GetListByFilmIdAsync(int filmId);
    
    public Task<SessionDto> GetByIdAsync(int id);
    
    public Task<SessionDto> CreateAsync(SessionInputDto sessionInput);
    
    public Task<SessionDto> UpdateByIdAsync(int id, SessionInputDto sessionInput);

    public Task DeleteByIdAsync(int id);
}