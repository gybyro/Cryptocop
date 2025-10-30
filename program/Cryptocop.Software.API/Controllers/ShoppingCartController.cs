using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.Software.API.Controllers;

[Route("api/cart")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    // GET /api/cart
    // Gets all items within the shopping cart

    // POST /api/cart 
    // Adds an item to the shopping cart

    // DELETE /api/cart/{id}
    // Deletes an item from the shopping cart

    // PATCH /api/cart/{id}
    // Updates the quantity for a shopping cart item

    // DELETE /api/cart
    // Clears the cart - all items within the cart should be deleted
}