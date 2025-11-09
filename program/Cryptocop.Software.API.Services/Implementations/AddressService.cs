using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _repo;
    public AddressService(IAddressRepository repo) => _repo = repo;


    // Create
    public Task AddAddressAsync(string email, AddressInputModel address)
    {
        return _repo.AddAddressAsync(email, address);
    }

    // Get All
    public Task<IEnumerable<AddressDto>> GetAllAddressesAsync(string email)
    {
        return _repo.GetAllAddressesAsync(email);
    }

    // Delete
    public Task DeleteAddressAsync(string email, int addressId)
    {
        return _repo.DeleteAddressAsync(email, addressId);
    }
}