using System.Net;
using System.Net.Mime;
using System.Text.Json;
using MoviePlex.Core.Exceptions;
using MoviePlex.UI.Middlewares;
using MoviePlex.UI.Middlewares.ExceptionHandler;

namespace MoviePlex.API.Middlewares;

public class ExceptionHandlingMiddlewareApi
{
    private const HttpStatusCode InternalServerErrorMessage = HttpStatusCode.InternalServerError;
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddlewareMvc> _logger;

    public ExceptionHandlingMiddlewareApi(RequestDelegate next, ILogger<ExceptionHandlingMiddlewareMvc> logger)
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

        await response.WriteAsJsonAsync(errorDto, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}