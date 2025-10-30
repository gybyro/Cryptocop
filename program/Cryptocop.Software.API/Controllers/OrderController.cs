using Microsoft.AspNetCore.Mvc;

using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _orderService = orderService;
    }
    // public OrderController(IOrderService orderService) => _orderService = orderService;


    // GET /api/orders
    // Gets all orders associated with the authenticated user
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        var mal = ""; // get email from somewhere else plz

        return Ok(await _orderService.GetOrdersAsync(mal));
    }


    // POST /api/orders
    // Adds a new order associated with the authenticated user
    [HttpPost("")]
    public async Task<ActionResult<OrderDto>> CreateNewOrder(OrderInputModel input)
    {
        var mal = ""; // get email from somewhere else plz
        
        try
        {
            await _orderService.CreateNewOrderAsync(mal, input);
            return Ok("Order created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}