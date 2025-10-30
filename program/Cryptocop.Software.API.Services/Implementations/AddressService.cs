using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _repo;
    public AddressService(IAddressRepository repo) => _repo = repo;


    public Task AddAddressAsync(string email, AddressInputModel address)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AddressDto>> GetAllAddressesAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAddressAsync(string email, int addressId)
    {
        throw new NotImplementedException();
    }
}