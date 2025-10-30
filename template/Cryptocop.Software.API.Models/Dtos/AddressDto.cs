namespace Cryptocop.Software.API.Models.Dtos;

public class AddressDto
{
    public int Id { get; set; }
    public required string StreetName { get; set; }
    public required string HouseNumber { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
}