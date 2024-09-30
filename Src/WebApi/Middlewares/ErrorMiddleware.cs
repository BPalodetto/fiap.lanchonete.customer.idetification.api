using Core.Services.Exceptions;
using Infrastructure.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using ProblemDetailsFactory = WebApi.Factories.ProblemDetailsFactory;

namespace WebApi.Middlewares;

[ExcludeFromCodeCoverage]
public class ErrorMiddleware
{
    private static readonly JsonSerializerOptions _serializerOptions = new();
    private readonly ILogger<ErrorMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpException exception)
        {
            _logger.LogWarning(exception, exception.Message);

            var statusCodeResponse = exception.StatusCode;
            var problemDetails = ProblemDetailsFactory.BuildProblemDetails(context.Request.Path, exception.Message, statusCodeResponse);
            await HandleResponseAsync(context, (int)statusCodeResponse, problemDetails);
        }
        catch (InvalidCustomerJwtException exception)
        {
            _logger.LogWarning(exception, exception.Message);

            var statusCodeResponse = HttpStatusCode.Forbidden;
            var problemDetails = ProblemDetailsFactory.BuildProblemDetails(context.Request.Path, exception.Message, statusCodeResponse);
            await HandleResponseAsync(context, (int)statusCodeResponse, problemDetails);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            var statusCodeResponse = HttpStatusCode.BadRequest;
            var problemDetails = ProblemDetailsFactory.BuildProblemDetails(context.Request.Path, statusCodeResponse);
            await HandleResponseAsync(context, (int)statusCodeResponse, problemDetails);
        }
    }

    private static async Task HandleResponseAsync<T>(HttpContext context, int statusCodeResponse, T response)
    {
        context.Response.StatusCode = statusCodeResponse;
        await context.Response.WriteAsJsonAsync(response, _serializerOptions);
    }

}