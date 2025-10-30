﻿using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task AddPaymentCardAsync(string email, PaymentCardInputModel paymentCard);
    Task<IEnumerable<PaymentCardDto>> GetStoredPaymentCardsAsync(string email);
    // Task<PaymentCardDto> GetPaymentCardAsync(string email, int id); // helper func, by me :P
}