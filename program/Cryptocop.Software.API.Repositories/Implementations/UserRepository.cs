using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;


namespace Cryptocop.Software.API.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly CryptocopDbContext _context;
    public UserRepository(CryptocopDbContext context) => _context = context;


    public Task<UserDto> CreateUserAsync(RegisterInputModel inputModel)
    {
        var userC = _context.Users.FirstOrDefault(u => u.Email == inputModel.Email);
        if (userC != null) throw new ArgumentException($"Email already in use");
    }

    public Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel)
    {
        throw new NotImplementedException();
    }
}