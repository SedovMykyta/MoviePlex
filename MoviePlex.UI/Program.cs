using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MoviePlex.Core.Areas.Films;
using MoviePlex.Core.Areas.Films.AutoMapper;
using MoviePlex.Core.Areas.Films.Dtos;
using MoviePlex.Core.Areas.Films.Validators;
using MoviePlex.Infrastructure;
using MoviePlex.UI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDatabase")));

builder.Services.AddTransient<IValidator<FilmInputDto>, FilmInputDtoValidator>();

builder.Services.AddTransient<IFilmService, FilmService>();

builder.Services.AddAutoMapper(typeof(FilmMappingProfile));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();