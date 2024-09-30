using Core.Entities.OrderAggregate;
using Core.Repositories;
using Infrastructure.Dtos;
using Infrastructure.Repositories.Exceptions;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly HttpClient _httpClient;
    public static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public OrderRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(order);

        var request = new HttpRequestMessage(HttpMethod.Post, $"api/Order");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var httpResponse = await _httpClient.SendAsync(request, cancellationToken);

        CreateOrderHttpException.ThrowIfNotSuccessStatusCode(httpResponse);

        var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<CreateOrderApiResponse>(jsonResponse, JsonSerializerOptions);
        return response!.OrderId;
    }
}
