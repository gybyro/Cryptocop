namespace Cryptocop.Software.API.Models.Entities;

public class Order
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string StreetName { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string CardholderName { get; set; } = null!;


    // (code-generated)
    public string MaskedCreditCard { get; set; } = null!;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public float TotalPrice { get; set; }

    // Foreign keys
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();  // (many-to-one)
}