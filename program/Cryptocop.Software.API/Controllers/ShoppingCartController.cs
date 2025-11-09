
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Cryptocop.Software.API.Extensions;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/cart")]
[ApiController]
[Authorize]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _cartService;
    public ShoppingCartController(IShoppingCartService cartService) => _cartService = cartService;


    // GET /api/cart
    // Gets all items within the shopping cart
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ShoppingCartItemDto>>> GetCartItems()
    {
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        return Ok(await _cartService.GetCartItemsAsync(email));
    }

    // POST /api/cart 
    // Adds an item to the shopping cart
    [HttpPost("")]
    public async Task<ActionResult> AddPaymentCard(ShoppingCartItemInputModel input)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        try
        {
            await _cartService.AddCartItemAsync(email, input);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE /api/cart/{id}
    // Deletes an item from the shopping cart
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveCartItem(int id)
    {
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        try
        {
            await _cartService.RemoveCartItemAsync(email, id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PATCH /api/cart/{id}
    // Updates the quantity for a shopping cart item
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateCartItemQuantity(int id, float quantity)
    {
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        try
        {
            await _cartService.UpdateCartItemQuantityAsync(email, id, quantity);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE /api/cart
    // Clears the cart - all items within the cart should be deleted
    [HttpDelete("")]
    public async Task<IActionResult> ClearCart()
    {
        var email = User.GetUserEmail();
        if (email is null) return Unauthorized();

        try
        {
            await _cartService.ClearCartAsync(email);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}