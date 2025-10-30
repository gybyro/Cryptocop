namespace Cryptocop.Software.API.Models.Dtos;

public class PaymentCardDto
{
    public int Id { get; set; }
    public required string CardholderName { get; set; }
    public required string CardNumber { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}