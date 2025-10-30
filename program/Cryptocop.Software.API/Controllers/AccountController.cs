using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.Software.API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    // POST /api/account/register
    // Registers a user within the application

    // POST /api/account/signin
    // Signs the user in by checking the credentials 
    // provided and issuing a JWT token in return

    // GET /api/account/signout
    // Logs the user out by voiding the provided 
    // JWT token using the id found within the claim
}