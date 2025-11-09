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
        var user = await _context.Users
            .Include(u => u.ShoppingCartt)
            .ThenInclude(cart => cart.ShoppingCartItems)
            .FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        return user.ShoppingCartt.ShoppingCartItems.Select(item => item.ToDto()).ToList();
    }

    // Create Item
    public async Task AddCartItemAsync(string email, ShoppingCartItemInputModel shoppingCartItemItem, float priceInUsd)
    {
        var user = await _context.Users
            .Include(u => u.ShoppingCartt)
            .ThenInclude(cart => cart.ShoppingCartItems)
            .FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var newItem = new ShoppingCartItem
        {
            ShoppingCartId = user.ShoppingCartt.Id,
            ProductIdentifier = shoppingCartItemItem.ProductIdentifier,
            Quantity = shoppingCartItemItem.Quantity,
            UnitPrice = priceInUsd
        };

        user.ShoppingCartt.ShoppingCartItems.Add(newItem);
        await _context.ShoppingCartItems.AddAsync(newItem);
        await _context.SaveChangesAsync();
    }

    // Delete Item
    public async Task RemoveCartItemAsync(string email, int id)
    {
        var user = await _context.Users
            .Include(u => u.ShoppingCartt)
            .ThenInclude(cart => cart.ShoppingCartItems)
            .FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var item = user.ShoppingCartt.ShoppingCartItems.FirstOrDefault(item => item.Id == id);
        if (item == null) throw new ArgumentException($"No shopping cart item found with ID {id}");

        _context.ShoppingCartItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    // Update
    public async Task UpdateCartItemQuantityAsync(string email, int id, float quantity)
    {
        var user = await _context.Users
            .Include(u => u.ShoppingCartt)
            .ThenInclude(cart => cart.ShoppingCartItems)
            .FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var item = user.ShoppingCartt.ShoppingCartItems.FirstOrDefault(item => item.Id == id);
        if (item == null) throw new ArgumentException($"No shopping cart item found with ID {id}");

        item.Quantity = quantity;

        _context.ShoppingCartItems.Update(item);
        await _context.SaveChangesAsync();
    }

    // Delete - for Shopping Cart Service
    public async Task ClearCartAsync(string email)
    {
        var user = await _context.Users
            .Include(u => u.ShoppingCartt)
            .ThenInclude(cart => cart.ShoppingCartItems)
            .FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        _context.ShoppingCartItems.RemoveRange(user.ShoppingCartt.ShoppingCartItems);
        user.ShoppingCartt.ShoppingCartItems.Clear();
        await _context.SaveChangesAsync();
    }

    // Delete - for Order Service
    public async Task DeleteCartAsync(string email)
    {
        var user = await _context.Users
            .Include(u => u.ShoppingCartt)
            .ThenInclude(cart => cart.ShoppingCartItems)
            .FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        _context.ShoppingCartItems.RemoveRange(user.ShoppingCartt.ShoppingCartItems);
        user.ShoppingCartt.ShoppingCartItems.Clear();
        await _context.SaveChangesAsync();
    }
}