using Microsoft.AspNetCore.Mvc;
using MoviePlex.Core.Areas.Films;
using MoviePlex.Core.Areas.Films.Dtos;
using MoviePlex.Core.Areas.Validators;
using MoviePlex.UI.Utilities;

namespace MoviePlex.UI.Controllers;

public class FilmController : Controller
{
    private readonly IFilmService _filmService;
    private readonly IValidationService _validationService;
    
    public FilmController(IFilmService filmService, IValidationService validationService)
    {
        _filmService = filmService;
        _validationService = validationService;
    }
    
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public ActionResult AdminPanel()
    {
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> GetListAdminAsync()
    {
        var films = await _filmService.GetListAsync();
        
        return View(films);
    }
    
    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync(FilmInputDto filmInput)
    {
        var result = await _validationService.ValidateAsync(filmInput);
        if (! result.IsValid)
        {
            result.AddToModelState(ModelState);
            
            return View(filmInput);
        }
        
        await _filmService.CreateAsync(filmInput);
        
        return RedirectToAction("GetListAdmin");
    }
    
    public async Task<ActionResult> EditAsync(int id)
    {
        var film = await _filmService.GetInputDtoByIdAsync(id);
        
        return View(film);
    }

    [HttpPost]
    public async Task<ActionResult> EditAsync(int id, FilmInputDto filmInput)
    {
        var result = await _validationService.ValidateAsync(filmInput);
        if (! result.IsValid)
        {
            result.AddToModelState(ModelState);
            
            return View(filmInput);
        }
        
        await _filmService.UpdateByIdAsync(id, filmInput);
        
        return RedirectToAction("GetListAdmin");
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _filmService.DeleteByIdAsync(id);
        
        return RedirectToAction("GetListAdmin");
    }
}