using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Helpers;


namespace Cryptocop.Software.API.Repositories.Implementations;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly CryptocopDbContext _context;
    public ShoppingCartRepository(CryptocopDbContext context) => _context = context;


    // Get all
    public async Task<IEnumerable<ShoppingCartItemDto>> GetCartItemsAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        // var cart = await _context.ShoppingCarts
        // .Include(o => o.ShoppingCartItems)
        // .Where(o => o.UserId == user.Id);

        var cart = user.ShoppingCartt;

        cart.ShoppingCartItems.Select(item => item.ToDto());
        return (IEnumerable<ShoppingCartItemDto>)cart.ShoppingCartItems;
    }

    // Create Item
    public async Task AddCartItemAsync(string email, ShoppingCartItemInputModel shoppingCartItemItem, float priceInUsd)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var newItem = new ShoppingCartItem
        {
            ShoppingCartId = user.ShoppingCartt.Id,
            ProductIdentifier = shoppingCartItemItem.ProductIdentifier,
            Quantity = shoppingCartItemItem.Quantity,
            UnitPrice = priceInUsd,
        };

        user.ShoppingCartt.ShoppingCartItems.Add(newItem);
        _context.ShoppingCartItems.Add(newItem);

        _context.SaveChanges();
        return;
    }

    // Delete Item
    public async Task RemoveCartItemAsync(string email, int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var item = _context.ShoppingCartItems.FirstOrDefault(item => item.Id == id);
        if (item == null) throw new ArgumentException($"No shopingcart item found with ID {id}");

        user.ShoppingCartt.ShoppingCartItems.Remove(item);
        _context.ShoppingCartItems.Remove(item);

        _context.SaveChanges();
        return;
    }

    // Update
    public async Task UpdateCartItemQuantityAsync(string email, int id, float quantity)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var item = _context.ShoppingCartItems.FirstOrDefault(item => item.Id == id);
        if (item == null) throw new ArgumentException($"No shopingcart item found with ID {id}");

        item.Quantity = quantity;

        // user.ShoppingCartt.ShoppingCartItems.U(item);
        _context.ShoppingCartItems.Update(item);

        _context.SaveChanges();
        return;
    }

    // Delete - for Shopping Cart Service
    public async Task ClearCartAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        foreach (ShoppingCartItem item in user.ShoppingCartt.ShoppingCartItems)
        {
            _context.ShoppingCartItems.Remove(item);
        }
        user.ShoppingCartt.ShoppingCartItems = new List<ShoppingCartItem>();
        
        _context.SaveChanges();
        return;
    }

    // Delete - for Order Service
    public async Task DeleteCartAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        foreach (ShoppingCartItem item in user.ShoppingCartt.ShoppingCartItems)
        {
            _context.ShoppingCartItems.Remove(item);
        }
        user.ShoppingCartt.ShoppingCartItems = new List<ShoppingCartItem>();
        
        _context.SaveChanges();
        return;
    }
}