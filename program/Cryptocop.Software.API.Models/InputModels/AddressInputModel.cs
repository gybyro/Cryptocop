using System.ComponentModel.DataAnnotations;
namespace Cryptocop.Software.API.Models.InputModels;

public class AddressInputModel
{
    [Required(ErrorMessage = "Street name is required")]
    public required string StreetName { get; set; }

    [Required(ErrorMessage = "House number is required")]
    public required string HouseNumber { get; set; }

    [Required(ErrorMessage = "Zip code is required")]
    public required string ZipCode { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public required string Country { get; set; }
    
    [Required(ErrorMessage = "City is required")]
    public required string City { get; set; }
}