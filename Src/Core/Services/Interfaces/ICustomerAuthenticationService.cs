namespace Core.Services.Interfaces;

public interface ICustomerAuthenticationService
{
    Task<string> GetCustomerToken(string Cpf, CancellationToken cancellationToken);
}
