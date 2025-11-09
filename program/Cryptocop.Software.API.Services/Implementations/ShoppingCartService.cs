using System.Threading.Tasks;
using System.Linq;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Services.Helpers;



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
    public async Task AddCartItemAsync(string email, ShoppingCartItemInputModel shoppingCartItemItem)
    {

        // Get all available cryptos (BTC, ETH, USDT, LINK)
        var availableCryptos = await _cryptoService.GetAvailableCryptocurrenciesAsync();

        // Find the requested crypto by symbol
        var crypto = availableCryptos
                .FirstOrDefault(c => c.Symbol.Equals(shoppingCartItemItem.ProductIdentifier, StringComparison.OrdinalIgnoreCase));
        if (crypto == null) throw new Exception($"Cryptocurrency '{shoppingCartItemItem.ProductIdentifier}' not found.");

        
        // Deserialize the response to a CryptoCurrencyDto model
        // var response = HttpResponseMessageExtensions.DeserializeJsonToObject<CryptoCurrencyDto>(TEMP);

        // Add it to the database using the appropriate repository class
        // _cartRepo.AddCartItemAsync(email, shoppingCartItemItem, response.PriceInUsd);

        await _cartRepo.AddCartItemAsync(email, shoppingCartItemItem, crypto.PriceInUsd);
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