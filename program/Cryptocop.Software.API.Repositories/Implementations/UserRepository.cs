using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Models.Entities;


namespace Cryptocop.Software.API.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly CryptocopDbContext _context;
    public UserRepository(CryptocopDbContext context) => _context = context;


    public async Task<UserDto> CreateUserAsync(RegisterInputModel inputModel)
    {
        var userC = await _context.Users.FirstOrDefaultAsync(u => u.Email == inputModel.Email);
        if (userC != null) throw new ArgumentException($"Email already in use");

        var hashPass = HashingHelper.HashPassword(inputModel.Password);

        var newUser = new User
        {
            FullName = inputModel.FullName,
            Email = inputModel.Email,
            HashedPassword = hashPass
        };
    }

    public Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel)
    {
        throw new NotImplementedException();
    }
}