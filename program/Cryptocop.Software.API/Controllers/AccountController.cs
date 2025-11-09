using Microsoft.AspNetCore.Mvc;

using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ITokenService _tokenService;
    public AccountController(IAccountService accountService, ITokenService tokenService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
    }


    // POST /api/account/register
    // Registers a user within the application
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> CreateUser(RegisterInputModel input)
    {
        try
        {
            var user = await _accountService.CreateUserAsync(input);
            await _tokenService.GenerateJwtTokenAsync(user);
            
            return Ok("New user created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST /api/account/signin
    // Signs the user in by checking the credentials 
    // provided and issuing a JWT token in return
    [HttpPost("signin")]
    public async Task<ActionResult<UserDto>> AuthenticateUser(LoginInputModel input)
    {
        try
        {
            await _accountService.AuthenticateUserAsync(input);
            return Ok("Login succssess'd");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET /api/account/signout
    // Logs the user out by voiding the provided 
    // JWT token using the id found within the claim
    [HttpGet("signout")]
    public async Task<ActionResult<UserDto>> LogoutUser()
    {
        var token = 1; // get token from token smth

        try
        {
            await _accountService.LogoutAsync(token);
            return Ok("Logout succssess'd");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}