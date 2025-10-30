using Microsoft.AspNetCore.Mvc;

using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/addresses")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService) => _addressService = addressService;


    // GET /api/addresses
    // Gets all addresses associated with authenticated user
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllAddresses()
    {
        var mal = ""; // get email from somewhere else plz

        return Ok(await _addressService.GetAllAddressesAsync(mal));
    }

    // POST /api/addresses
    // Adds a new address associated with authenticated user
    [HttpPost("")]
    public async Task<ActionResult<AddressDto>> AddAddress(AddressInputModel input)
    {
        var mal = ""; // get email from somewhere else plz

        try
        {
            await _addressService.AddAddressAsync(mal, input);
            return Ok("New Address has been added");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE /api/cart/{id}
    // Deletes an address by id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var mal = ""; // get email from somewhere else plz

        try
        {
            await _addressService.DeleteAddressAsync(mal, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}