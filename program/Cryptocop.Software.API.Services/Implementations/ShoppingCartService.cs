using System.Text.Json;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Services.Helpers;
using Cryptocop.Software.API.Repositories.Interfaces;


namespace Cryptocop.Software.API.Services.Implementations;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _cartRepo;
    private readonly ICryptoCurrencyService _cryptoService;
    public ShoppingCartService(IShoppingCartRepository cartRepo, ICryptoCurrencyService cryptoService)
    {
        _cartRepo = cartRepo;
        _cryptoService = cryptoService;
    }


    // Get All
    public Task<IEnumerable<ShoppingCartItemDto>> GetCartItemsAsync(string email)
    {
        return _cartRepo.GetCartItemsAsync(email);
    }

    // Add Cart Item
    public Task AddCartItemAsync(string email, ShoppingCartItemInputModel shoppingCartItemItem)
    {
        // TODO:
        // Call the external API using the product identifier as an URL parameter to receive the
        // current price in USD for this particular cryptocurrency.
        var TEMP;
        
        // Deserialize the response to a CryptoCurrencyDto model
        var response = HttpResponseMessageExtensions.DeserializeJsonToObject<CryptoCurrencyDto>(TEMP);

        // Add it to the database using the appropriate repository class
        _cartRepo.AddCartItemAsync(email, shoppingCartItemItem, response.PriceInUsd);

        return;
    }

    // Delete Item
    public Task RemoveCartItemAsync(string email, int id)
    {
        return _cartRepo.RemoveCartItemAsync(email, id);
    }

    // Update Item
    public Task UpdateCartItemQuantityAsync(string email, int id, float quantity)
    {
        return _cartRepo.UpdateCartItemQuantityAsync(email, id, quantity);
    }

    // Clear Cart
    public Task ClearCartAsync(string email)
    {
        return _cartRepo.ClearCartAsync(email);
    }
}