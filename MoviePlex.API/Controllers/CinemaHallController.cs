using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using MoviePlex.Core.Areas.CinemaHalls;
using MoviePlex.Core.Areas.CinemaHalls.Dtos;

namespace MoviePlex.API.Controllers;

[ApiController]
[Route("api/cinemaHall")]
[Produces(MediaTypeNames.Application.Json)]
public class CinemaHallController : ControllerBase
{
    private readonly ICinemaHallService _hallService;

    public CinemaHallController(ICinemaHallService hallService)
    {
        _hallService = hallService;
    }
    
    
    /// <summary>
    /// Get list of Cinema halls
    /// </summary>
    /// <returns>Returns CinemaHallDto list</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<CinemaHallDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var halls = await _hallService.GetListAsync();
        
        return Ok(halls);
    }
    
    
    /// <summary>
    /// Get Cinema hall by id
    /// </summary>
    /// <param name="id">Cinema hall id</param>
    /// <returns>Returns CinemaHallDto</returns>
    /// <response code="200">Success</response>
    /// <response code="404">CinemaHallNotFound</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var hall = await _hallService.GetByIdAsync(id);

        return Ok(hall);
    }
    
    /// <summary>
    /// Get Cinema halls by name
    /// </summary>
    /// <param name="name">Name cinema hall</param>
    /// <returns>Returns CinemaHallDto</returns>
    /// <response code="200">Success</response>
    [HttpGet("name/{name}")]
    [ProducesResponseType(typeof(List<CinemaHallDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByNameAsync([FromRoute] string name)
    {
        var halls = await _hallService.GetByNameAsync(name);
        
        return Ok(halls);
    }

    /// <summary>
    /// Create Cinema hall
    /// </summary>
    /// <param name="cinemaHallInput">FilmInputDto object</param>
    /// <returns>Created CinemaHallDto object</returns>
    /// <response code="200">Success</response>
    /// <response code="400">NameExists, BadRequest</response>
    [HttpPost]
    [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromForm] CinemaHallInputDto cinemaHallInput)
    {
        var hall = await _hallService.CreateAsync(cinemaHallInput);

        return Ok(hall);
    }

    /// <summary>
    /// Update Cinema hall by id
    /// </summary>
    /// <param name="id">Cinema hall id</param>
    /// <param name="cinemaHallInput">CinemaHallInputDto object</param>
    /// <returns>Updated CinemaHallDto object</returns>
    /// <response code="200">Success</response>
    /// <response code="400">NameExists, BadRequest</response>
    /// <response code="404">CinemaHallNotFound</response>
    [HttpPut ("{id:int}")]
    [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromForm] CinemaHallInputDto cinemaHallInput)
    {
        var hall = await _hallService.UpdateByIdAsync(id, cinemaHallInput);

        return Ok(hall);
    }

    /// <summary>
    /// Delete Cinema hall by id
    /// </summary>
    /// <param name="id">Cinema hall id</param>
    /// <response code="200">Success</response>
    /// <response code="404">ArticleNotFound</response>
    [HttpDelete ("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _hallService.DeleteByIdAsync(id);

        return Ok();
    }
}