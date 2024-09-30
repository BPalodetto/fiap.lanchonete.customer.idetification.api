using Core.Dtos;
using Core.Entities.CustomerAggregate;
using Core.Entities.OrderAggregate;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Core.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<CreateOrderResponse> CreateAsync(CreateOrderRequet request, Customer? customer, CancellationToken cancellationToken)
    {
        var orderProduct = new OrderProduct();
        {
            orderProduct.ProductId = request.ProductId;
            orderProduct.Quantity = request.Quantity;
        }
        var order = new Order()
        {
            CustomerId = customer?.Id,
            Product = orderProduct
        };

        var orderId = await _orderRepository.CreateAsync(order, cancellationToken);

        return
            new CreateOrderResponse() 
            { 
                OrderId = orderId 
            };
    }
}
