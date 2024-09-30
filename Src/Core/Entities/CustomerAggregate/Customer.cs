namespace Core.Entities.CustomerAggregate;

public class Customer
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
}
