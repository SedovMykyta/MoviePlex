using MoviePlex.Core.Areas.CinemaHalls.Dtos;

namespace MoviePlex.Core.Areas.CinemaHalls;

public interface ICinemaHallService
{
    public Task<List<CinemaHallDto>> GetListAsync();

    public Task<CinemaHallDto> GetByIdAsync(int id);

    public Task<List<CinemaHallDto>> GetByNameAsync(string name);

    public Task<CinemaHallDto> CreateAsync(CinemaHallInputDto cinemaHallInput);
    
    public Task<CinemaHallDto> UpdateByIdAsync(int id, CinemaHallInputDto cinemaHallInput);

    public Task DeleteByIdAsync(int id);
}