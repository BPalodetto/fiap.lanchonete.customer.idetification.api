using Core.Entities.OrderAggregate;

namespace Core.Repositories;

public interface IOrderRepository
{
    Task<int> CreateAsync(Order order, CancellationToken cancellationToken);
}
