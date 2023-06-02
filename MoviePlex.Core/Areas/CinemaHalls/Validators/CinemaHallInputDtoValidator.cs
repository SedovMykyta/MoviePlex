using FluentValidation;
using MoviePlex.Core.Areas.CinemaHalls.Dtos;

namespace MoviePlex.Core.Areas.CinemaHalls.Validators;

public class CinemaHallInputDtoValidator: AbstractValidator<CinemaHallInputDto>
{
    public CinemaHallInputDtoValidator()
    {
        RuleFor(hall => hall.Name)
            .Length(1,200).WithMessage("Length must be between 1 and 200 characters");
        
        RuleFor(hall => hall.CountOfSeats)
            .NotEmpty().WithMessage("The count of seats must be specified")
            .GreaterThan(0)
            .WithMessage("CountOfSeats must be a positive integer.");

        RuleFor(hall => hall.Price)
            .NotEmpty().WithMessage("The price must be specified")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a non-negative decimal.");
    }
}