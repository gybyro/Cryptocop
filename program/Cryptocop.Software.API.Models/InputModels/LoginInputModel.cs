using System.ComponentModel.DataAnnotations;
namespace Cryptocop.Software.API.Models.InputModels;

public class LoginInputModel
{
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Must be a valid email address")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "A minimum length of 8 characters")]
    public required string Password { get; set; }
}