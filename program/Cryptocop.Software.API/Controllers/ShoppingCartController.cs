using Microsoft.AspNetCore.Mvc;

using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers;

[Route("api/cart")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _cartService;
    public ShoppingCartController(IShoppingCartService cartService) => _cartService = cartService;


    // GET /api/cart
    // Gets all items within the shopping cart
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ShoppingCartItemDto>>> GetCartItems()
    {
        var mal = ""; // get email from somewhere else plz

        return Ok(await _cartService.GetCartItemsAsync(mal));
    }

    // POST /api/cart 
    // Adds an item to the shopping cart
    [HttpPost("")]
    public async Task<ActionResult<ShoppingCartItemDto>> AddPaymentCard(ShoppingCartItemInputModel input)
    {
        var mal = ""; // get email from somewhere else plz

        try
        {
            await _cartService.AddCartItemAsync(mal, input);
            return Ok("Item added to cart");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE /api/cart/{id}
    // Deletes an item from the shopping cart
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveCartItem(int id)
    {
        var mal = ""; // get email from somewhere else plz

        try
        {
            await _cartService.RemoveCartItemAsync(mal, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PATCH /api/cart/{id}
    // Updates the quantity for a shopping cart item
    [HttpPatch("{id}")]
    public async Task<ActionResult<ShoppingCartItemDto>> UpdateCartItemQuantity(int id, float quantity)
    {
        var mal = ""; // get email from somewhere else plz

        try
        {
            await _cartService.UpdateCartItemQuantityAsync(mal, id, quantity);
            return Ok("Item quantity updated");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE /api/cart
    // Clears the cart - all items within the cart should be deleted
    [HttpDelete("")]
    public async Task<IActionResult> ClearCart()
    {
        var mal = ""; // get email from somewhere else plz

        try
        {
            await _cartService.ClearCartAsync(mal);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}