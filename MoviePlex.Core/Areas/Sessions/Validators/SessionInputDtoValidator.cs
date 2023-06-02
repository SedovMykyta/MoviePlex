using FluentValidation;
using MoviePlex.Core.Areas.Sessions.Dtos;

namespace MoviePlex.Core.Areas.Sessions.Validators;

public class SessionInputDtoValidator : AbstractValidator<SessionInputDto>
{
    public SessionInputDtoValidator()
    {
        //TODO
        // RuleFor(session => session.Date)
        
        RuleFor(session => session.Time)
        .Must(BeValidTimeSpan).WithMessage("Incorrect date format")
            .Must(duration => TimeSpan.TryParse(duration, out var timeSpan) && timeSpan > TimeSpan.FromSeconds(30))
            .WithMessage("The duration should be at least 30 seconds")
            .Must(duration =>
                TimeSpan.TryParse(duration, out var timeSpan) && timeSpan <
                TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59) + TimeSpan.FromSeconds(59))
            .WithMessage("The duration must be no more than 23 hours 59 minutes 59 seconds");
        
        RuleFor(session => session.CinemaHallId)
            .NotEmpty().WithMessage("The cinema hall id must be specified")
            .GreaterThan(0)
            .WithMessage("Cinema hall id must be a positive integer.");

        RuleFor(session => session.FilmId)
            .NotEmpty().WithMessage("The film id must be specified")
            .GreaterThan(0)
            .WithMessage("Film id must be a positive integer.");
    }
    
    
    private bool BeValidTimeSpan(string timeSpan)
    {
        return TimeSpan.TryParse(timeSpan, out _);
    }
}