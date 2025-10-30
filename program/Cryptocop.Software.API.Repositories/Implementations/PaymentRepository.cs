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

        _context.PaymentCards.Add(card);
        _context.SaveChanges();
        return;
    }

    public async Task<IEnumerable<PaymentCardDto>> GetStoredPaymentCardsAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        

        var usersCardList = await _context.PaymentCards.Where(c => c.UserId == user.Id).ToListAsync();
        var ret = usersCardList.Select(c => new PaymentCardDto
        {
            
            
        })

    }

    // helper func, by me :P
    // public async Task<PaymentCardDto> GetPaymentCardAsync(string email, int id)
    // {
    //     var cards = await GetStoredPaymentCardsAsync(email);
    //     if (cards == null) throw new ArgumentException("User has not saved any cards");

    //     var card = cards.FirstOrDefault(a => a.Id == id);
    //     if (card == null) throw new ArgumentException($"No card found with ID {id}");

    //     return card;
    // }
}

public int Id { get; set; }
    public int UserId { get; set; }
    public string CardholderName { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public int Month { get; set; }
    public int Year { get; set; }