namespace Cryptocop.Software.API.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;

    // (code-generated)
    public string HashedPassword { get; set; } = "";

    // Foreign keys
    public ICollection<PaymentCard> PaymentCards { get; set; } = new List<PaymentCard>();  // (many-to-one)
    public ICollection<Order> Orders { get; set; } = new List<Order>();  // (many-to-one)
    public ICollection<Address> Addresses { get; set; } = new List<Address>();  // (many-to-one)
    public ShoppingCart ShoppingCarts { get; set; } = null!; // (one-to-one)
}