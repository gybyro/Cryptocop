using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Cryptocop.Software.API.Extensions;
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
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticationResultDto>> CreateUser(RegisterInputModel input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        try
        {
            var user = await _accountService.CreateUserAsync(input);
            var token = await _tokenService.GenerateJwtTokenAsync(user);
            

            var result = new AuthenticationResultDto
            {
                Token = token,
                User = user
            };

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST /api/account/signin
    // Signs the user in by checking the credentials 
    // provided and issuing a JWT token in return
    [HttpPost("signin")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticationResultDto>> AuthenticateUser(LoginInputModel input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        try
        {
            // await _accountService.AuthenticateUserAsync(input);
            var user = await _accountService.AuthenticateUserAsync(input);
            var token = await _tokenService.GenerateJwtTokenAsync(user);

            var result = new AuthenticationResultDto
            {
                Token = token,
                User = user
            };
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET /api/account/signout
    // Logs the user out by voiding the provided 
    // JWT token using the id found within the claim
    [HttpGet("signout")]
    [Authorize]
    public async Task<IActionResult> LogoutUser()
    {
        var tokenId = User.GetTokenId();
        if (tokenId is null)
        {
            return Unauthorized();
        }
        await _accountService.LogoutAsync(tokenId.Value);
        return NoContent();
    }
}