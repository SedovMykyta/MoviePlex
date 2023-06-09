using System.Net;
using System.Net.Mime;
using System.Text.Json;
using MoviePlex.Core.Exceptions;
using MoviePlex.UI.Middlewares.ExceptionHandler;

namespace MoviePlex.UI.Middlewares;

public class ExceptionHandlingMiddlewareMvc
{
    private const HttpStatusCode InternalServerErrorMessage = HttpStatusCode.InternalServerError;
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddlewareMvc> _logger;

    public ExceptionHandlingMiddlewareMvc(RequestDelegate next, ILogger<ExceptionHandlingMiddlewareMvc> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, httpContext);
        }
    }
    
    private async Task HandleExceptionAsync(Exception ex, HttpContext context)
    {
        _logger.LogError(ex.Message);
    
        var response = context.Response;
        
        response.ContentType = MediaTypeNames.Application.Json;
    
        ErrorDto errorDto = new();
        
        if (ex is CustomException customException)
        {
            response.StatusCode = (int)customException.StatusCode;
            errorDto.ErrorMessage = customException.Message;
            errorDto.AdditionalInfo = customException.ResponseAdditionalInfo;
        }
        else
        {
            response.StatusCode = (int)InternalServerErrorMessage;
            errorDto.ErrorMessage = InternalServerErrorMessage.ToString();
        }
 
        var errorDtoJson = JsonSerializer.Serialize(errorDto);
        context.Session.SetString("ErrorMessage", errorDtoJson);
        
        context.Response.Redirect(context.Request.Headers["Referer"].ToString());
    }
}