using System.Net;

namespace Infrastructure.Exceptions;

public abstract class HttpException : Exception
{
    public HttpException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; set; }
}