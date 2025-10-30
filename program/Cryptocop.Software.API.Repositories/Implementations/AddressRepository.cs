using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Helpers;

namespace Cryptocop.Software.API.Repositories.Implementations;

public class AddressRepository : IAddressRepository
{
    private readonly CryptocopDbContext _context;
    public AddressRepository(CryptocopDbContext context) => _context = context;


    public Task AddAddressAsync(string email, AddressInputModel address)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new ArgumentException($"User with email: {email} not found");

        var ret = await _context.Addresses.Where(o => o.UserId == user.Id).ToListAsync();

        return ret.Select(o => o.ToDto());
    }

    // helper func, takes in users list of addresses
    // public async Task<AddressDto> GetAddressAsync(string email, int id)
    // {
    //     var addresses = await GetAllAddressesAsync(email);
    //     if (addresses == null) throw new ArgumentException("User has no saved addresses");

    //     var addr = addresses.FirstOrDefault(a => a.Id == id);
    //     if (addr == null) throw new ArgumentException($"No address found with ID {id}");

    //     return addr;
    // }

    public Task DeleteAddressAsync(string email, int addressId)
    {
        var addrs = _context.Addresses.FirstOrDefault(a => a.Id == addressId);
        if (addrs == null) throw new ArgumentException($"No address found with ID {addressId}");

        _context.Addresses.Remove(addrs);
        return Task.CompletedTask;
    }
}