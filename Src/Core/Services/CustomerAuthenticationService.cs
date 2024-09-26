using Core.Entities.Customer;
using Core.Repositories;
using Core.Services.Interfaces;
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
    public async Task<string> GetCustomerToken(string Cpf, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByCpf(Cpf);

        return GenerateToken(customer);
    }
    private static string GenerateToken(Customer user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        //todo variavel de ambiente
        var key = Encoding.ASCII.GetBytes("819563c4-c1a2-48f5-a409-32565ae08405");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, CustomerRole)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

