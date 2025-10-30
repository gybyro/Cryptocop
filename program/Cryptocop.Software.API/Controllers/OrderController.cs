using Microsoft.AspNetCore.Mvc;

using 

namespace Cryptocop.Software.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    // GET /api/orders
    // Gets all orders associated with the authenticated user
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        return Ok(await _service.GetAllAsync());
    }


    // POST /api/orders
    // Adds a new order associated with the authenticated user
    [HttpPost("")]
    // public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    // {
    //     return Ok(await _service.GetAllAsync());
    // }
}