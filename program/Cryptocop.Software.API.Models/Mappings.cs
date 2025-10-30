using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Repositories.Helpers;

namespace Cryptocop.Software.API.Models;

public static class Mappings
{
    public static AddressDto ToDto(this Address address) =>
        new AddressDto
        {
            Id = address.Id,
            StreetName = address.StreetName,
            HouseNumber = address.HouseNumber,
            ZipCode = address.ZipCode,
            Country = address.Country,
            City = address.City
        };

    public static JwtTokenDto ToDto(this JwtToken token) =>
        new JwtTokenDto
        {
            Id = token.Id,
            Blacklisted = token.Blacklisted
        };

    public static OrderDto ToDto(this Order order) =>
        new OrderDto
        {
            Id = order.Id,
            Email = order.Email,
            FullName = order.FullName,
            StreetName = order.StreetName,
            HouseNumber = order.HouseNumber,
            ZipCode = order.ZipCode,
            Country = order.Country,
            City = order.City,
            CardholderName = order.CardholderName,
            CreditCard = order.MaskedCreditCard,
            OrderDate = order.OrderDate.ToString("dd.mm.yyyy"),
            TotalPrice = order.TotalPrice,
            OrderItems = order.OrderItems.Select(i => i.ToDto()).ToList()
        };

    public static OrderItemDto ToDto(this OrderItem item) =>
        new OrderItemDto
        {
            Id = item.Id,
            ProductIdentifier = item.ProductIdentifier,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.TotalPrice
        };

    public static PaymentCardDto ToDto(this PaymentCard card) =>
        new PaymentCardDto
        {
            Id = card.Id,
            CardholderName = card.CardholderName,
            CardNumber = MaskPaymentCard(card.CardNumber),
            Month = card.Month,
            Year = card.Year
        };

    // public static ShoppingCartItemDto ToDto(this ShoppingCartItem item) =>
    //     new ShoppingCartItemDto
    //     {
    //         Id = item.Id,
    //         ProductIdentifier = item.ProductIdentifier,
    //         Quantity = item.Quantity,
    //         UnitPrice = item.UnitPrice,
    //         TotalPrice = item.TotalPrice
    //     };
}

