using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Helpers;


namespace Cryptocop.Software.API.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly CryptocopDbContext _context;
    private readonly ITokenRepository _tokenRepo;
    public UserRepository(CryptocopDbContext context, ITokenRepository tokenRepo) 
    {
        _context = context;
        _tokenRepo = tokenRepo;
    }


    // Create
    public async Task<UserDto> CreateUserAsync(RegisterInputModel inputModel)
    {
        var userC = await _context.Users.FirstOrDefaultAsync(u => u.Email == inputModel.Email);
        if (userC != null) throw new ArgumentException($"Email already in use");

        var hashPass = HashingHelper.HashPassword(inputModel.Password);

        var token = await _tokenRepo.CreateNewTokenAsync();

        var newUser = new User
        {
            FullName = inputModel.FullName,
            Email = inputModel.Email,
            HashedPassword = hashPass
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        var ret = newUser.ToDto(token.Id);
        return ret;
    }

    // 
    public async Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel)
    {
        var userC = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginInputModel.Email);
        if (userC == null) throw new ArgumentException($"Email does not exist");

        var newHashedPass = HashingHelper.HashPassword(loginInputModel.Password);

        if (userC.HashedPassword != newHashedPass) throw new ArgumentException($"Wrong Password");


        var token = await _tokenRepo.CreateNewTokenAsync();

        var ret = userC.ToDto(token.Id);
        return ret;
    }
}