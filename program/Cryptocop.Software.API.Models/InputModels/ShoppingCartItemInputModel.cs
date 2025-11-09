using System.ComponentModel.DataAnnotations;
namespace Cryptocop.Software.API.Models.InputModels;

public class ShoppingCartItemInputModel
{
    [Required(ErrorMessage = "Product ID is required")]
    public required string ProductIdentifier { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(0.01f, float.MaxValue, ErrorMessage = "Quantity must be 0.01 and above")]
    public required float Quantity { get; set; }
}