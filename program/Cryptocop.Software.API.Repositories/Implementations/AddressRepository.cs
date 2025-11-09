using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Helpers;

namespace Cryptocop.Software.API.Repositories.Implementations;

public class AddressRepository : IAddressRepository
{
    private readonly CryptocopDbContext _context;
    public AddressRepository(CryptocopDbContext context) => _context = context;

    // Create
    public async Task AddAddressAsync(string email, AddressInputModel address)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var newAddress = new Address
        {
            UserId = user.Id,
            StreetName = address.StreetName,
            HouseNumber = address.HouseNumber,
            ZipCode = address.ZipCode,
            Country = address.Country,
            City = address.City,
        };

        user.Addresses.Add(newAddress);
        _context.Addresses.Add(newAddress);

        _context.SaveChanges();
        return;
    }

    // Get all
    public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var ret = await _context.Addresses.Where(o => o.UserId == user.Id).ToListAsync();

        return ret.Select(o => o.ToDto());
    }

    // Delete
    public async Task DeleteAddressAsync(string email, int addressId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");
        
        var addrs = _context.Addresses.FirstOrDefault(a => a.Id == addressId);
        if (addrs == null) throw new ArgumentException($"No address found with ID {addressId}");

        user.Addresses.Remove(addrs);
        _context.Addresses.Remove(addrs);
        return;
    }
}