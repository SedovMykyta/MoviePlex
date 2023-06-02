using FluentValidation;
using MoviePlex.Core.Areas.Films.Dtos;

namespace MoviePlex.Core.Areas.Films.Validators;

public class FilmInputDtoValidator : AbstractValidator<FilmInputDto>
{
    public FilmInputDtoValidator()
    {
        RuleFor(film => film.Name)
            .Length(2, 200).WithMessage("Length must be between 2 and 200 characters");

        RuleFor(film => film.Duration)
            .Must(BeValidTimeSpan).WithMessage("Incorrect date format")
            .Must(duration => TimeSpan.TryParse(duration, out var timeSpan) && timeSpan > TimeSpan.FromSeconds(30))
            .WithMessage("The duration should be at least 30 seconds")
            .Must(duration =>
                TimeSpan.TryParse(duration, out var timeSpan) && timeSpan <
                TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59))
            .WithMessage("The duration must be no more than 23 hours 59 minutes 59 seconds");

        //TODO
        // RuleFor(x => x.RentalStartDate)

        //TODO 
        //Make a check that the end date is later than the start date
        // RuleFor(film => film.RentalEndDate)

        RuleFor(film => film.Publisher)
            .Length(2, 200).WithMessage("Length must be between 2 and 200 characters");

        RuleFor(film => film.Genre)
            .NotEmpty().WithMessage("The Topic field is required");

        RuleFor(film => film.Trailer)
            .Matches(@"^(https?://)?(www\.)?youtube\.com/watch\?v=[\w-]{11}.*$")
            .WithMessage("The link should point to YouTube");
    }
    

    private bool BeValidTimeSpan(string timeSpan)
    {
        return TimeSpan.TryParse(timeSpan, out _);
    }
}