using Core.Entities.Customer;

namespace Core.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerByCpf(string cpf);
}
