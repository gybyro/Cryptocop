using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Helpers;


namespace Cryptocop.Software.API.Repositories.Implementations;

public class PaymentRepository : IPaymentRepository
{
    private readonly CryptocopDbContext _context;
    public PaymentRepository(CryptocopDbContext context) => _context = context;


    // Create
    public async Task AddPaymentCardAsync(string email, PaymentCardInputModel paymentCard)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var card = new PaymentCard
        {
            UserId = user.Id,
            CardholderName = paymentCard.CardholderName,
            CardNumber = paymentCard.CardNumber,
            Month = paymentCard.Month,
            Year = paymentCard.Year
        };

        // user.PaymentCards.Add(card);
        // _context.PaymentCards.Add(card);

        // _context.SaveChanges();
        // return;
        
        await _context.PaymentCards.AddAsync(card);
        await _context.SaveChangesAsync();
    }

    // Get all
    public async Task<IEnumerable<PaymentCardDto>> GetStoredPaymentCardsAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var usersCardList = await _context.PaymentCards.Where(c => c.UserId == user.Id).ToListAsync();
        var ret = usersCardList.Select(c => c.ToDto());

        return ret;
    }
}