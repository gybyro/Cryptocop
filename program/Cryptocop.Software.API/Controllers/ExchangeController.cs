
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Cryptocop.Software.API.Extensions;
using Cryptocop.Software.API.Models;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/exchanges")]
[ApiController]
// [Authorize]
public class ExchangeController : ControllerBase
{
    private readonly IExchangeService _exchangeService;
    public ExchangeController(IExchangeService exchangeService) => _exchangeService = exchangeService;


    // GET /api/exchange?pageNumber=1

    // Gets all exchanges in a paginated envelope. 
    // This routes accepts a single query parameter called 
    // pageNumber which is used to paginate the results

    [HttpGet("")]
    // public async Task<ActionResult<IEnumerable<ExchangeDto>>> GetExchanges(int pagenum)
    public async Task<ActionResult<Envelope<ExchangeDto>>> GetExchanges([FromQuery] int pageNumber = 1)
    {
        var result = await _exchangeService.GetExchangesAsync(pageNumber);
        return Ok(result);
    }
}