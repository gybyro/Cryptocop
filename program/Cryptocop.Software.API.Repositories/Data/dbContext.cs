using Microsoft.EntityFrameworkCore;

using Cryptocop.Software.API.Models.Entities;

namespace Cryptocop.Software.API.Repositories.Data;

public class CryptocopDbContext : DbContext
{
    public CryptocopDbContext(DbContextOptions<CryptocopDbContext> options) : base(options) { }

    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<JwtToken> JwtTokens => Set<JwtToken>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<PaymentCard> PaymentCards => Set<PaymentCard>();
    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();
    public DbSet<User> Users => Set<User>();


    // relationships established on db creation:
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);


    //     // ShoppingCart -> ShoppingCartItem
    //     modelBuilder.Entity<ShoppingCart>()
    //         .HasMany(a => a.ShoppingCartItems)
    //         .WithOne(s => s.ShoppingCart)
    //         .HasForeignKey(s => s.AlbumId)
    //         .OnDelete(DeleteBehavior.Cascade);
        
    //     // Order -> OrderItem
    //     // User -> ShoppingCart
    //     // User -> Order
    //     // User -> Address
    //     // User -> PaymentCard

    // }
    
}
