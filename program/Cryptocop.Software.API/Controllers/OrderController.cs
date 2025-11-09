
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Cryptocop.Software.API.Extensions;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/orders")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService) => _orderService = orderService;


    // GET /api/orders
    // Gets all orders associated with the authenticated user
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        return Ok(await _orderService.GetOrdersAsync(email));
    }


    // POST /api/orders
    // Adds a new order associated with the authenticated user
    [HttpPost("")]
    public async Task<ActionResult> CreateNewOrder(OrderInputModel input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();
        
        try
        {
            await _orderService.CreateNewOrderAsync(email, input);
            return Accepted();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}