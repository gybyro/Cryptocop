
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Cryptocop.Software.API.Extensions;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/payments")]
[ApiController]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _payService;
    public PaymentController(IPaymentService payService) => _payService = payService;
    

    // GET /api/payments
    // Gets all payment cards associated with the authenticated user
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<PaymentCardDto>>> GetStoredPaymentCards()
    {
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        return Ok(await _payService.GetStoredPaymentCardsAsync(email));
    }

    // POST /api/payments
    // Adds a new payment card associated with the authenticated user
    [HttpPost("")]
    public async Task<ActionResult> AddPaymentCard(PaymentCardInputModel input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        try
        {
            await _payService.AddPaymentCardAsync(email, input);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}