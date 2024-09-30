using Core.Entities.CustomerAggregate;
using Core.Repositories;
using Core.Services.Exceptions;
using Core.Services.Interfaces;
using Core.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services;

public class CustomerAuthenticationService : ICustomerAuthenticationService
{
    public const string CustomerRole = "Customer";

    private readonly ICustomerRepository _customerRepository;

    public CustomerAuthenticationService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> ValidateCustomerCpf(string Cpf, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByCpfAsync(Cpf, cancellationToken);

        if(customer == null)
        {
            throw new InvalidCustomerJwtException();
        }
        return customer!;
    }

    public async Task<string> GetCustomerTokenAsync(string Cpf, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByCpfAsync(Cpf, cancellationToken);

        if(customer == null)
        {
            return string.Empty;
        }
        

        return GenerateToken(customer);
    }
    private static string GenerateToken(Customer user)
    {
        

        var authKey = CoreEnviromentVariables.AuthKey;
        var key = Encoding.ASCII.GetBytes(authKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Cpf),
                    new Claim(ClaimTypes.Role, CustomerRole)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

