using System.ComponentModel.DataAnnotations;
namespace Cryptocop.Software.API.Models.InputModels;

public class RegisterInputModel
{
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Must be a valid email address")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Full name is required")]
    [MinLength(3, ErrorMessage = "A minimum length of 3 characters")]
    public required string FullName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "A minimum length of 8 characters")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Password confirmation is required")]
    [MinLength(8, ErrorMessage = "A minimum length of 8 characters")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public required string PasswordConfirmation { get; set; }
}