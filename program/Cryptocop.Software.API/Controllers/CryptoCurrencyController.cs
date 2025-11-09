
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Cryptocop.Software.API.Extensions;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[ApiController]
[Route("api/cryptocurrencies")]
[Authorize]
public class CryptoCurrencyController : ControllerBase
{
    private readonly ICryptoCurrencyService _cryptoService;
    public CryptoCurrencyController(ICryptoCurrencyService cryptoService) => _cryptoService = cryptoService;


    // GET /api/cryptocurrencies

    // Gets all available cryptocurrencies:
    // BitCoin (BTC)
    // Ethereum (ETH)
    // Tether (USDT)
    // Chainlink (LINK)

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<CryptoCurrencyDto>>> GetAvailableCryptocurrencies()
    {
        return Ok(await _cryptoService.GetAvailableCryptocurrenciesAsync());
    }

}