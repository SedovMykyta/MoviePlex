using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using MoviePlex.Core.Areas.Films;
using MoviePlex.Core.Areas.Films.Dtos;
using MoviePlex.Infrastructure.Entities.Enum;

namespace MoviePlex.API.Controllers;

[ApiController]
[Route("api/film")]
[Produces(MediaTypeNames.Application.Json)]
public class FilmController : ControllerBase
{
    private readonly IFilmService _filmService;

    public FilmController(IFilmService filmService)
    {
        _filmService = filmService;
    }

    
    /// <summary>
    /// Get list of Films
    /// </summary>
    /// <returns>Returns FilmDto list</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<FilmDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var films = await _filmService.GetListAsync();
        
        return Ok(films);
    }
    
    
    /// <summary>
    /// Get Film by id
    /// </summary>
    /// <param name="id">Film id</param>
    /// <returns>Returns FilmDto</returns>
    /// <response code="200">Success</response>
    /// <response code="404">FilmNotFound</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FilmDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var film = await _filmService.GetByIdAsync(id);

        return Ok(film);
    }
    

    /// <summary>
    /// Get Films by genre
    /// </summary>
    /// <param name="genreFilm">Genre film</param>
    /// <returns>Returns FilmsDto</returns>
    /// <response code="200">Success</response>
    [HttpGet("genre/{genreFilm}")]
    [ProducesResponseType(typeof(FilmDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByGenreAsync([FromRoute] GenreFilm genreFilm)
    {
        var films = await _filmService.GetByGenreAsync(genreFilm);
        
        return Ok(films);
    }

    /// <summary>
    /// Get Films by name
    /// </summary>
    /// <param name="name">Name film</param>
    /// <returns>Returns FilmsDto</returns>
    /// <response code="200">Success</response>
    [HttpGet("name/{name}")]
    [ProducesResponseType(typeof(List<FilmDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByNameAsync([FromRoute] string name)
    {
        var films = await _filmService.GetByNameAsync(name);
        
        return Ok(films);
    }

    /// <summary>
    /// Create Film
    /// </summary>
    /// <param name="filmInput">FilmInputDto object</param>
    /// <returns>Created FilmDto object</returns>
    /// <response code="200">Success</response>
    /// <response code="400">NameExists, BadRequest</response>
    [HttpPost]
    [ProducesResponseType(typeof(FilmDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] FilmInputDto filmInput)
    {
        var film = await _filmService.CreateAsync(filmInput);

        return Ok(film);
    }

    /// <summary>
    /// Update Film by id
    /// </summary>
    /// <param name="id">Film id</param>
    /// <param name="filmInput">FilmInputDto object</param>
    /// <returns>Updated FilmDto object</returns>
    /// <response code="200">Success</response>
    /// <response code="400">NameExists, BadRequest</response>
    /// <response code="404">FilmNotFound</response>
    [HttpPut ("{id:int}")]
    [ProducesResponseType(typeof(FilmDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromForm] FilmInputDto filmInput)
    {
        var film = await _filmService.UpdateByIdAsync(id, filmInput);

        return Ok(film);
    }

    /// <summary>
    /// Delete Film by id
    /// </summary>
    /// <param name="id">Film id</param>
    /// <response code="200">Success</response>
    /// <response code="404">ArticleNotFound</response>
    [HttpDelete ("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _filmService.DeleteByIdAsync(id);

        return Ok();
    }
}