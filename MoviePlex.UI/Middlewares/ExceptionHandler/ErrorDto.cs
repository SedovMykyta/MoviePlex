namespace MoviePlex.UI.Middlewares.ExceptionHandler;

public class ErrorDto
{
    public string ErrorMessage { get; set; }
    
    public object? AdditionalInfo { get; set; }
}