using Microsoft.AspNetCore.Mvc;

using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IPaymentService _payService;
    public PaymentController(IPaymentService payService, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _payService = payService;
    }


    // GET /api/payments
    // Gets all payment cards associated with the authenticated user
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<PaymentCardDto>>> GetStoredPaymentCards()
    {
        var mal = ""; // get email from somewhere else plz

        return Ok(await _payService.GetStoredPaymentCardsAsync(mal));
    }

    // POST /api/payments
    // Adds a new payment card associated with the authenticated user
    [HttpPost("")]
    public async Task<ActionResult<PaymentCardDto>> AddPaymentCard(PaymentCardInputModel input)
    {
        var mal = ""; // get email from somewhere else plz

        try
        {
            await _payService.AddPaymentCardAsync(mal, input);
            return Ok("Payment card created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}