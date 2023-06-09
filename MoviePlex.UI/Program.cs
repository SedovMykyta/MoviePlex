using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MoviePlex.Core.Areas.CinemaHalls;
using MoviePlex.Core.Areas.CinemaHalls.AutoMapper;
using MoviePlex.Core.Areas.CinemaHalls.Dtos;
using MoviePlex.Core.Areas.CinemaHalls.Validators;
using MoviePlex.Core.Areas.Films;
using MoviePlex.Core.Areas.Films.AutoMapper;
using MoviePlex.Core.Areas.Films.Dtos;
using MoviePlex.Core.Areas.Films.Validators;
using MoviePlex.Core.Areas.Sessions;
using MoviePlex.Core.Areas.Sessions.Dtos;
using MoviePlex.Core.Areas.Sessions.Validators;
using MoviePlex.Core.Areas.Validators;
using MoviePlex.Infrastructure;
using MoviePlex.UI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDatabase")));

builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IValidator<FilmInputDto>, FilmInputDtoValidator>();
builder.Services.AddScoped<IValidator<CinemaHallInputDto>, CinemaHallInputDtoValidator>();
builder.Services.AddScoped<IValidator<SessionInputDto>, SessionInputDtoValidator>();

builder.Services.AddTransient<IFilmService, FilmService>();
builder.Services.AddTransient<ICinemaHallService, CinemaHallService>();
builder.Services.AddTransient<ISessionService, SessionService>();

builder.Services.AddAutoMapper(typeof(FilmMappingProfile), typeof(CinemaHallMappingProfile));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddlewareMvc>();


app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();