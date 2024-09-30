namespace Core.Entities.OrderAggregate;

public class Order
{
    public int? CustomerId { get; set; }
    public OrderProduct Product { get; set; }
}
