using Core.Entities.CustomerAggregate;

namespace Core.Services.Interfaces;

public interface ICustomerAuthenticationService
{
    Task<string> GetCustomerTokenAsync(string Cpf, CancellationToken cancellationToken);
    Task<Customer> ValidateCustomerCpf(string Cpf, CancellationToken cancellationToken);
}
