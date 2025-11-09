using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;


namespace Cryptocop.Software.API.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenRepository _tokenRepo;
    public AccountService(IUserRepository repo, ITokenRepository tokenRepo)
    {
        _userRepo = repo;
        _tokenRepo = tokenRepo;
    }


    public Task<UserDto> CreateUserAsync(RegisterInputModel inputModel)
    {
        var newUser = _userRepo.CreateUserAsync(inputModel);
        return newUser;
    }

    public Task<UserDto> AuthenticateUserAsync(LoginInputModel loginInputModel)
    {
        var user = _userRepo.AuthenticateUserAsync(loginInputModel);
        return user;
    }

    public async Task LogoutAsync(int tokenId)
    {
        var isVoid = await _tokenRepo.IsTokenBlacklistedAsync(tokenId);
        if (!isVoid) await _tokenRepo.VoidTokenAsync(tokenId);

        return;
    }
}