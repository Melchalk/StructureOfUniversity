using System.Net;

namespace StructureOfUniversity.Models.Exceptions;

public class UnauthorizedException(string message) : StatusCodeException(message, statusCode)
{
    private const HttpStatusCode statusCode = HttpStatusCode.Unauthorized;
}
