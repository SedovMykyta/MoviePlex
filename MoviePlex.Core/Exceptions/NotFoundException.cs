using System.Net;

namespace MoviePlex.Core.Exceptions;

public class NotFoundException : CustomException
{
    private new const HttpStatusCode StatusCode = HttpStatusCode.NotFound;
    
    public NotFoundException(string message) : base(message, StatusCode)
    {
    }
}