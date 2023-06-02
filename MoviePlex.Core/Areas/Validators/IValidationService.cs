namespace MoviePlex.Core.Areas.Validators;

public interface IValidationService
{
    public Task ValidateAndThrowAsync<T>(T dto);
}