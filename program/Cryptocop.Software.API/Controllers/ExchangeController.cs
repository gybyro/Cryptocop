
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/exchanges")]
[ApiController]
[Authorize]
public class ExchangeController : ControllerBase
{
    private readonly IExchangeService _exchangeService;
    public ExchangeController(IExchangeService exchangeService) => _exchangeService = exchangeService;


    // GET /api/exchanges

    // Gets all exchanges in a paginated envelope. 
    // This routes accepts a single query parameter called 
    // pageNumber which is used to paginate the results

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ExchangeDto>>> GetExchanges(int pagenum)
    {
        // string idString = HttpContext.Request.Query["id"];
        // int pagenum = int.Parse(idString);
        if (pagenum == 0) return Ok(await _exchangeService.GetExchangesAsync());
        else



        return Ok(await _exchangeService.GetExchangesAsync(pagenum));
    }
}