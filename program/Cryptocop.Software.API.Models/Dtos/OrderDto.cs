namespace Cryptocop.Software.API.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public required string StreetName { get; set; }
    public required string HouseNumber { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string CardholderName { get; set; }
    public required string CreditCard { get; set; } // MaskedCreditCard string
    public required string OrderDate { get; set; } // Represented as 01.01.2020
    public float TotalPrice { get; set; }
    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();  // (many-to-one)
}

// OrderItemDto is the same as ShoppingCartItemDto but the assignment description lists them separately...
public class OrderItemDto
{
    public int Id { get; set; }
    public required string ProductIdentifier { get; set; }
    public float Quantity { get; set; }
    public float UnitPrice { get; set; }
    public float TotalPrice { get; set; }
}