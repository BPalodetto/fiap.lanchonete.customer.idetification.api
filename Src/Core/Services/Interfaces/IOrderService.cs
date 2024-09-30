using Core.Dtos;
using Core.Entities.CustomerAggregate;

namespace Core.Services.Interfaces;

public interface IOrderService
{
    Task<CreateOrderResponse> CreateAsync(CreateOrderRequet request, Customer? userId, CancellationToken cancellationToken);
}
