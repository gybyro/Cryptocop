using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cryptocop.Software.API.Repositories.Data
{
    public class CryptocopDbContextFactory : IDesignTimeDbContextFactory<CryptocopDbContext>
    {
        public CryptocopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CryptocopDbContext>();

            // Use your actual connection string here — same as in appsettings.json
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cryptocop_db;Username=postgres;Password=postgres");

            return new CryptocopDbContext(optionsBuilder.Options);
        }
    }
}