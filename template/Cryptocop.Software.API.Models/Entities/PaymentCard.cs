namespace Cryptocop.Software.API.Models.Entities;

public class PaymentCard
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CardholderName { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public int Month { get; set; }
    public int Year { get; set; }
}