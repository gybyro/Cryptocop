using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Repositories.Interfaces;

public interface IAddressRepository
{
    Task AddAddressAsync(string email, AddressInputModel address);
    Task<IEnumerable<AddressDto>> GetAllAddressesAsync(string email);
    // Task<AddressDto> GetAddressAsync(string email, int id); // helper func
    Task DeleteAddressAsync(string email, int addressId);
}