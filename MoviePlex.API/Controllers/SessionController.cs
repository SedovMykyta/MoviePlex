using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using MoviePlex.Core.Areas.Sessions;
using MoviePlex.Core.Areas.Sessions.Dtos;

namespace MoviePlex.API.Controllers;

[ApiController]
[Route("api/session")]
[Produces(MediaTypeNames.Application.Json)]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    /// <summary>
    /// Get list of Session
    /// </summary>
    /// <returns>Returns SessionDto list</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<SessionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var sessions = await _sessionService.GetListAsync();

        return Ok(sessions);
    }


    /// <summary>
    /// Get list SessionDto by cinemaHallId
    /// </summary>
    /// <param name="cinemaHallId">Cinema Hall Id</param>
    /// <returns>Returns SessionDto list</returns>
    /// <response code="200">Success</response>
    [HttpGet("cinemaHallId/{cinemaHallId}")]
    [ProducesResponseType(typeof(List<SessionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByCinemaHallIdAsync([FromRoute] int cinemaHallId)
    {
        var sessions = await _sessionService.GetListByCinemaHallIdAsync(cinemaHallId);

        return Ok(sessions);
    }

    /// <summary>
    /// Get list SessionDto by id
    /// </summary>
    /// <param name="filmId">Film id</param>
    /// <returns>Returns SessionDto list</returns>
    /// <response code="200">Success</response>
    [HttpGet("id/{id}")]
    [ProducesResponseType(typeof(List<SessionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByFilmIdAsync([FromRoute] int filmId)
    {
        var sessions = await _sessionService.GetListByFilmIdAsync(filmId);

        return Ok(sessions);
    }

    /// <summary>
    /// Get Session by id
    /// </summary>
    /// <param name="id">Session Id</param>
    /// <returns>Returns SessionDto</returns>
    /// <response code="200">Success</response>
    /// <response code="404">SessionNotFound</response>
    [HttpGet("{cinemaHallId:int}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var session = await _sessionService.GetByIdAsync(id);

        return Ok(session);
    }

    /// <summary>
    /// Create Session
    /// </summary>
    /// <param name="sessionInput">SessionInputDto object</param>
    /// <returns>Created SessionDto object</returns>
    /// <response code="200">Success</response>
    /// <response code="400">SessionExists, BadRequest</response>
    [HttpPost]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromForm] SessionInputDto sessionInput)
    {
        var session = await _sessionService.CreateAsync(sessionInput);

        return Ok(session);
    }

    /// <summary>
    /// Update Session by id
    /// </summary>
    /// <param name="id">Session id</param>
    /// <param name="sessionInput">SessionInputDto object</param>
    /// <returns>Updated SessionDto object</returns>
    /// <response code="200">Success</response>
    /// <response code="400">SessionExists, BadRequest</response>
    /// <response code="404">SessionNotFound</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromForm] SessionInputDto sessionInput)
    {
        var session = await _sessionService.UpdateByIdAsync(id, sessionInput);

        return Ok(session);
    }

    /// <summary>
    /// Delete Session by id
    /// </summary>
    /// <param name="id">Session id</param>
    /// <response code="200">Success</response>
    /// <response code="404">SessionNotFound</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        await _sessionService.DeleteByIdAsync(id);

        return Ok();
    }
}