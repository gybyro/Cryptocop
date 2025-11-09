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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ShoppingCart -> ShoppingCartItem
        modelBuilder.Entity<ShoppingCartItem>()
        .HasOne<ShoppingCart>()
        .WithMany()
        .HasForeignKey(a => a.ShoppingCartId);


        // Order -> OrderItem
        modelBuilder.Entity<OrderItem>()
        .HasOne<Order>()
        .WithMany()
        .HasForeignKey(a => a.OrderId);


        // User -> ShoppingCart (1:1)
        modelBuilder.Entity<ShoppingCart>()
            .HasIndex(a => a.UserId)
            .IsUnique();

        // modelBuilder.Entity<ShoppingCart>()
        // .HasOne(s => s.User)
        // .WithOne(u => u.ShoppingCart)
        // .HasForeignKey<ShoppingCart>(s => s.UserId);


        // User -> Address
        modelBuilder.Entity<Address>()
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<Address>()
        .HasOne(a => a.User)
        .WithMany(u => u.Addresses)
        .HasForeignKey(a => a.UserId);


        // User -> PaymentCard
        modelBuilder.Entity<PaymentCard>()
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(a => a.UserId);
    }
}
