using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using MoviePlex.Core.Exceptions;

namespace MoviePlex.Core.Areas.Validators;

public class ValidationService : IValidationService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ValidateAndThrowAsync<T>(T dto)
    {
        var validator = _serviceProvider.GetService<IValidator<T>>();

        if (validator == null)
        {
            throw new InvalidOperationException($"Validator not found for type {typeof(T).Name}");
        }

        ValidationResult result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            var errors = new Dictionary<string, string>();
            foreach (var failure in result.Errors)
            {
                if (errors.ContainsKey(failure.PropertyName))
                {
                    errors[failure.PropertyName] += "; " + failure.ErrorMessage;
                }
                else
                {
                    errors[failure.PropertyName] = failure.ErrorMessage;
                }
            }
            throw new ValidationErrorException(errors);
        }
    }
}