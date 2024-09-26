using Core.Entities.Customer;
using Core.Repositories;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly HttpClient _httpClient;
    public CustomerRepository(HttpClient httpClient)
    {
        _httpClient = httpClient; 
    }

    public async Task<Customer> GetCustomerByCpf(string cpf)
    {
        throw new NotImplementedException();
    }
}
