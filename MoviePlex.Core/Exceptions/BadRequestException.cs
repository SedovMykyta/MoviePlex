using System.Net;

namespace MoviePlex.Core.Exceptions;

public class BadRequestException : CustomException
{
    private new const HttpStatusCode StatusCode = HttpStatusCode.BadRequest;
    
    public BadRequestException(string message) : base(message, StatusCode)
    {
    }
}