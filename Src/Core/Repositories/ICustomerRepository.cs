using Core.Entities.CustomerAggregate;

namespace Core.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerByCpfAsync(string cpf, CancellationToken cancellationToken);
}
