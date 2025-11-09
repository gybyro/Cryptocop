namespace Cryptocop.Software.API.Models.Entities;

public class ShoppingCart
{
    public int Id { get; set; }
    public int UserId { get; set; }

    // Foreign keys
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();  // (many-to-one)

    // // navigation
    // public User User { get; set; } = null!;
}
