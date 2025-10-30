namespace Cryptocop.Software.API.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public int TokenId { get; set; }
}