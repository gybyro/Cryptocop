using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;


namespace Cryptocop.Software.API.Repositories.Implementations;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly CryptocopDbContext _context;
    public ShoppingCartRepository(CryptocopDbContext context) => _context = context;


    public Task<IEnumerable<ShoppingCartItemDto>> GetCartItemsAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task AddCartItemAsync(string email, ShoppingCartItemInputModel shoppingCartItemItem, float priceInUsd)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCartItemAsync(string email, int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCartItemQuantityAsync(string email, int id, float quantity)
    {
        throw new NotImplementedException();
    }

    public Task ClearCartAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCartAsync(string email)
    {
        throw new NotImplementedException();
    }
}