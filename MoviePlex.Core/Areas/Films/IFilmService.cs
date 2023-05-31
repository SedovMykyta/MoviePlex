using MoviePlex.Core.Areas.Films.Dtos;
using MoviePlex.Infrastructure.Entities.Enum;

namespace MoviePlex.Core.Areas.Films;

public interface IFilmService
{
    public Task<List<FilmDto>> GetListAsync();

    public Task<FilmDto> GetByIdAsync(int id);
    
    public Task<List<FilmDto>> GetByGenreAsync(GenreFilm genreFilm);

    public Task<List<FilmDto>> GetByNameAsync(string name);

    public Task<FilmDto> CreateAsync(FilmInputDto filmInput);
    
    public Task<FilmDto> UpdateByIdAsync(int id, FilmInputDto filmInput);

    public Task DeleteByIdAsync(int id);
}
