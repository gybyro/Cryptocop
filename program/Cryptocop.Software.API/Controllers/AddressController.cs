
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Cryptocop.Software.API.Extensions;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/addresses")]
[ApiController]
[Authorize]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService) => _addressService = addressService;


    // GET /api/addresses
    // Gets all addresses associated with authenticated user
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllAddresses()
    {
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        return Ok(await _addressService.GetAllAddressesAsync(email));
    }

    // POST /api/addresses
    // Adds a new address associated with authenticated user
    [HttpPost("")]
    public async Task<ActionResult> AddAddress(AddressInputModel input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        try
        {
            await _addressService.AddAddressAsync(email, input);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE /api/cart/{id}
    // Deletes an address by id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        try
        {
            await _addressService.DeleteAddressAsync(email, id);
            return NoContent();
        }
        catch (ArgumentException  ex)
        {
            return BadRequest(ex.Message);
        }
    }
}