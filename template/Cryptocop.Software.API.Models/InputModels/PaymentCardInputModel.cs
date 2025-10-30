using System.ComponentModel.DataAnnotations;
namespace Cryptocop.Software.API.Models.InputModels;

public class PaymentCardInputModel
{
    [Required(ErrorMessage = "Cardholder name is required")]
    [MinLength(3, ErrorMessage = "A minimum length of 3 characters")]
    public required string CardholderName { get; set; }

    [CreditCard]
    [Required(ErrorMessage = "Credit card number is required")]
    public required string CardNumber { get; set; }

    [Range(1, 12, ErrorMessage = "Month must be from 1 to 12")] // By default, both boundaries are inclusive (I think)
    public int Month { get; set; }

    [Range(0, 99, ErrorMessage = "Month must be from 1 to 12")]
    public int Year { get; set; }
}