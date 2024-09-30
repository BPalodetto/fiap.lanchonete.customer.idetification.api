using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerAuthenticationService _customerService;
    public CustomerController(ICustomerAuthenticationService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate(CustomerAuthenticateRequest request, CancellationToken cancellationToken)
    {
        var response = await _customerService.GetCustomerTokenAsync(request.Cpf, cancellationToken);
        if (string.IsNullOrEmpty(response))
        {
            return NotFound();
        }
        return Ok(response);
    }

}
