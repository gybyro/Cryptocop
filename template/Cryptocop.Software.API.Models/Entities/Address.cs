namespace Cryptocop.Software.API.Models.Entities;

public class Address
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string StreetName { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;    
}