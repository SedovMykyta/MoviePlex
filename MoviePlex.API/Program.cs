using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MoviePlex.API.Filters;
using MoviePlex.API.Middlewares;
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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDatabase")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IValidator<FilmInputDto>, FilmInputDtoValidator>();
builder.Services.AddScoped<IValidator<CinemaHallInputDto>, CinemaHallInputDtoValidator>();
builder.Services.AddScoped<IValidator<SessionInputDto>, SessionInputDtoValidator>();

builder.Services.AddTransient<IFilmService, FilmService>();
builder.Services.AddTransient<ICinemaHallService, CinemaHallService>();
builder.Services.AddTransient<ISessionService, SessionService>();

builder.Services.AddAutoMapper(typeof(FilmMappingProfile), typeof(CinemaHallMappingProfile));

builder.Services.AddSwaggerGen(config =>
{
    config.SchemaFilter<EnumSchemaFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddlewareApi>();

app.UseHttpsRedirection();

app.UseCors(config =>
{
    config.AllowAnyOrigin();
    config.AllowAnyHeader();
    config.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();