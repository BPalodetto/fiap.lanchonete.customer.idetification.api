using Core.Dtos;
using Core.Entities.CustomerAggregate;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    public readonly IOrderService _orderService;
    public readonly ICustomerAuthenticationService _customerAuthenticationService;

    public OrderController(IOrderService orderService, ICustomerAuthenticationService customerAuthenticationService)
    {
        _orderService = orderService;
        _customerAuthenticationService = customerAuthenticationService;
    }

    [ProducesResponseType(typeof(IEnumerable<CreateOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> ListActiveAsync(CreateOrderRequet request, CancellationToken cancellationToken)
    {
        string? nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Customer? customer = null;
        if(nameIdentifier != null) { 
            customer = await _customerAuthenticationService.ValidateCustomerCpf(nameIdentifier, cancellationToken);
        }

        var response = await _orderService.CreateAsync(request, customer, cancellationToken);
        return Ok(response);
    }
}
