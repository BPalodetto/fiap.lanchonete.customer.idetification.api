using Core.Entities.CustomerAggregate;
using Core.Repositories;
using Infrastructure.Repositories.Exceptions;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly HttpClient _httpClient;
    public static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public CustomerRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Customer?> GetCustomerByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/customer/cpf/{cpf}");
        var httpResponse = await _httpClient.SendAsync(request, cancellationToken);

        if (httpResponse.StatusCode == HttpStatusCode.NotFound) {
            return null;
        }

        GetCustomerByCpfHttpException.ThrowIfNotSuccessStatusCode(httpResponse);

        var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
        var customer = JsonSerializer.Deserialize<Customer>(jsonResponse, JsonSerializerOptions);
        return customer;
    }
}
