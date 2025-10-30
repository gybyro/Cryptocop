using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.Software.API.Controllers;

[Route("api/exchanges")]
[ApiController]
public class ExchangeController : ControllerBase
{
    // GET /api/exchanges

    // Gets all exchanges in a paginated envelope. 
    // This routes accepts a single query parameter called 
    // pageNumber which is used to paginate the results

}