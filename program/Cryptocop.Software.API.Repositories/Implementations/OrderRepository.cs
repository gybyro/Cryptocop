using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Helpers;

namespace Cryptocop.Software.API.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly CryptocopDbContext _context;
    public OrderRepository(CryptocopDbContext context) => _context = context;


    // Get all
    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(string email)
    {
        var orders = await _context.Orders
            .AsNoTracking()
            .Include(o => o.OrderItems)
            .Where(o => o.Email == email)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
        // .Include(o => o.OrderItems)
        // .Where(o => o.Email == email)
        // .ToListAsync();

        // orders.Select(order => order.ToDto());
        // return (IEnumerable<OrderDto>)orders;
        return orders.Select(order => order.ToDto()).ToList();
    }

    // Create
    public async Task<OrderDto> CreateNewOrderAsync(string email, OrderInputModel order)
    {
        var user = await _context.Users
            .Include(u => u.ShoppingCartt)
            .ThenInclude(cart => cart.ShoppingCartItems)
            .FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var address = await _context.Addresses.FirstOrDefaultAsync(u => u.Id == order.AddressId && u.UserId == user.Id);
        if (address == null) throw new ArgumentException($"No address found with ID {order.AddressId}");

        var card = await _context.PaymentCards.FirstOrDefaultAsync(u => u.Id == order.PaymentCardId && u.UserId == user.Id);
        if (card == null) throw new ArgumentException($"No card found with ID {order.PaymentCardId}");

        var cart = user.ShoppingCartt;
        if (cart == null || cart.ShoppingCartItems.Count == 0)
        {
            throw new ArgumentException("Shopping cart is empty.");
        }

        var cardMask = PaymentCardHelper.MaskPaymentCard(card.CardNumber);

        var orderItems = cart.ShoppingCartItems.Select(item => new OrderItem
            {
            ProductIdentifier = item.ProductIdentifier,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.UnitPrice * item.Quantity
        }).ToList();

        var totalPrice = orderItems.Sum(i => i.TotalPrice);

        var newOrder = new Order
        {
            Email = email,
            FullName = user.FullName,
            StreetName = address.StreetName,
            HouseNumber = address.HouseNumber,
            ZipCode = address.ZipCode,
            Country = address.Country,
            City = address.City,
            CardholderName = card.CardholderName,
            MaskedCreditCard = cardMask,
            OrderDate = DateTime.UtcNow,
            TotalPrice = totalPrice,
            OrderItems = orderItems
        };

        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();

        var ret = newOrder.ToDto();
        ret.CreditCard = card.CardNumber;
        return ret;
    }
}