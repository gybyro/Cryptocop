namespace Cryptocop.Software.API.Models.Dtos;

public class AuthenticationResultDto
{
    public required string Token { get; set; }
    public required UserDto User { get; set; }
}