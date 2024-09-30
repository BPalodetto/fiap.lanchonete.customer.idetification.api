using Infrastructure.Exceptions;
using System.Net;

namespace Infrastructure.Repositories.Exceptions;

internal class GetCustomerByCpfHttpException : HttpException
{
    private GetCustomerByCpfHttpException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }

    public static void ThrowIfNotSuccessStatusCode(HttpResponseMessage httpResponse)
    {
        if (httpResponse.IsSuccessStatusCode)
        {
            return;
        }

        var message = httpResponse.Content.ReadAsStringAsync().Result;

        throw new GetCustomerByCpfHttpException(message, httpResponse.StatusCode);
    }
}
